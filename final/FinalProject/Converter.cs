using System;
using System.IO;
using System.Text.Json;

/*=======================================================================
  This file is used to convert data from source (url: https://github.com/uberspot/OpenTriviaQA) to json format
=======================================================================*/
static class Converter
{
  public static string StripAndEncode(string str)
  {
    return str.Trim().TrimEnd('\r', '\n').Trim().Replace("\r\n", "\n");
  }
  private static readonly JsonSerializerOptions _options = new()
  {
      WriteIndented = true,
  };

  public static void Convert()
  {
    string[] dirs = Directory.GetFiles(@"Database\files\", "*").Where(name => !name.EndsWith(".json",StringComparison.OrdinalIgnoreCase)).ToArray();
    foreach (var file in dirs)
    {
      if (Directory.Exists(file))
      {
        Console.WriteLine($"{file} is a directory. Skipping.");
        continue;
      }

      List<Dictionary<string, string>> questions = new List<Dictionary<string, string>>();
      Dictionary<string, string> question = new Dictionary<string, string>();

      using (StreamReader reader = new StreamReader(file))
      {
        while (!reader.EndOfStream)
        {
          string line = StripAndEncode(reader.ReadLine());
          if (line.StartsWith("#Q "))
          {
            question["question"] = line.Substring(3);
            question["category"] = new FileInfo(file).Name;

            while (!reader.EndOfStream)
            {
              line = StripAndEncode(reader.ReadLine());

              if (line == null || line.StartsWith("^ "))
              {
                break;
              }

              question["question"] += "\n" + line;
            }
          }

          if (line.StartsWith("^ "))
          {
            question["answer"] = line.Substring(2);
          }
          else if (line.StartsWith("A ") || line.StartsWith("B ") || line.StartsWith("C ") || line.StartsWith("D "))
          {
            if (!question.ContainsKey("choices"))
            {
              question["choices"] = "";
            }
            else
            {
              question["choices"] += "\n";
            }
            question["choices"] += line.Substring(2);
          }
          else if (string.IsNullOrWhiteSpace(line))
          {
            if (question.Count > 0)
            {
              question["type"] = typeof(MultipleChoiceQuestion).ToString() ;
              if( (question["choices"] == "True\nFalse" ) || 
                  (question["choices"] == "False\nTrue" ) || 
                  (question["choices"] == "Yes\nNo"     ) || 
                  (question["choices"] == "No\nYes" )
              ){
                question["type"] = typeof(TrueFalseQuestion).ToString() ;
                question["answer"] = ((question["answer"] == "True") || (question["answer"] == "Yes") ).ToString() ;
                question["choices"] = "True\nFalse";
              }
              questions.Add(question);
            }

            question = new Dictionary<string, string>();
          }
        }
      }
      File.WriteAllText($"{file}.json", JsonSerializer.Serialize(questions, _options));
    }
  }
}

