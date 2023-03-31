using System;
using Microsoft.Data.Sqlite;
// Database class
public class Database
{
  private SqliteConnection _connection;
  private User _user;
  private string _databaseName = @"Database\database.db";

  public Database()
  {
    _connection = new SqliteConnection($"Data Source={_databaseName}");
    _connection.DefaultTimeout = 60;
    _connection.Open();
    CreateTables();
  }

  public Database(User user)
  {
    _connection = new SqliteConnection($"Data Source={_databaseName}");
    _connection.DefaultTimeout = 60;
    _connection.Open();
    CreateTables();

    _user = user;
  }

  
  private void CreateTables()
  {
    string createUserTableQuery = "CREATE TABLE IF NOT EXISTS Users (Id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT UNIQUE, type TEXT, password TEXT, data TEXT)";
    string createQuizTableQuery = "CREATE TABLE IF NOT EXISTS Quizzes(Id INTEGER PRIMARY KEY AUTOINCREMENT, title TEXT, description TEXT NULLABLE, duration INTEGER,  passMark INTEGER, userId INTEGER )";
    string createQuestionTableQuery = "CREATE TABLE IF NOT EXISTS Questions(Id INTEGER PRIMARY KEY AUTOINCREMENT, quizId INTEGER,type TEXT NULLABLE, text TEXT, answer TEXT, data TEXT NULLABLE, UNIQUE(quizId,text) )";
    string createResultTableQuery = "CREATE TABLE IF NOT EXISTS Results (Id INTEGER PRIMARY KEY AUTOINCREMENT, userId INTEGER, quizId INTEGER, correctAnswers INTEGER, totalQuestions INTEGER , score REAL, time INTEGER NULLABLE, quizData TEXT, data TEXT)";

    SqliteCommand createUserTableCommand = new SqliteCommand(createUserTableQuery, _connection);
    SqliteCommand createQuizTableCommand = new SqliteCommand(createQuizTableQuery, _connection);
    SqliteCommand createQuestionTableCommand = new SqliteCommand(createQuestionTableQuery, _connection);
    SqliteCommand createResultTableCommand = new SqliteCommand(createResultTableQuery, _connection);

    createUserTableCommand.ExecuteNonQuery();
    createQuizTableCommand.ExecuteNonQuery();
    createQuestionTableCommand.ExecuteNonQuery();
    createResultTableCommand.ExecuteNonQuery();
  }

  public bool VerifyUser(string username, string password)
  {
    var cmd = new SqliteCommand("SELECT password FROM users WHERE name = @username;", _connection);
    cmd.Parameters.AddWithValue("@username", username);
    var reader = cmd.ExecuteReader();
    if (!reader.Read())
    {
      reader.Close();
      return false;
    }
    string storedPassword = reader.GetString(0);
    reader.Close();
    return storedPassword == User.HashPassword(password);
  }

  public User GetUserByName(string username)
  {
    string selectQuery = "SELECT * FROM Users WHERE name = @username";
    SqliteCommand command = new SqliteCommand(selectQuery, _connection);
    command.Parameters.AddWithValue("@username", username);
    SqliteDataReader reader = command.ExecuteReader();

    User user = null ;
    if (reader.Read())
    {
      int id = reader.GetInt32(0);
      string name = reader.GetString(1);
      string type = reader.GetString(2);
      string password = reader.GetString(3);

      if(type == "Student"){
        user = new Student(id , name , password);
      }else if(type == "Teacher"){
        user = new Teacher(id , name , password);
      }else{
        user = new User(id , name , password);
      }
      reader.Close();
      return user;
    }
    reader.Close();
    return null ;
  }


  public User createUser(string type, string username, string password)
  {

    string insertQuery = "INSERT INTO Users (name, type, password) VALUES (@username, @type, @password)";

    SqliteCommand command = new SqliteCommand(insertQuery, _connection);
    command.Parameters.AddWithValue("@username", username);
    command.Parameters.AddWithValue("@type", type);
    command.Parameters.AddWithValue("@password", User.HashPassword(password));

    try
    {
      command.ExecuteNonQuery();
    }
    catch (System.Exception)
    {
      Console.WriteLine("User already Exist \n");
    }
    return GetUserByName(username);
  }

  public void AddQuiz(Quiz quiz)
  {
    SqliteCommand command = new SqliteCommand("INSERT INTO Quizzes (title,description,duration,passMark,userId) VALUES(@title,@description,@duration,@passMark,@userId)", _connection);
    command.Parameters.AddWithValue("@title", quiz.GetTitle());
    command.Parameters.AddWithValue("@description", quiz.GetDescription());
    command.Parameters.AddWithValue("@duration", quiz.GetDuration());
    command.Parameters.AddWithValue("@passMark", quiz.GetPassmark());
    command.Parameters.AddWithValue("@userId", _user.GetId());
    command.ExecuteNonQuery();
 
    // Get ID of inserted quiz
    command = new SqliteCommand("SELECT Id FROM Quizzes WHERE title=@title AND userId=@userId", _connection);
    command.Parameters.AddWithValue("@title", quiz.GetTitle());
    command.Parameters.AddWithValue("@userId", _user.GetId());
    int quizId = Convert.ToInt32(command.ExecuteScalar());
    // Add questions to database
    List<Question> questionList = quiz.GetQuestions();
    AddQuestionToQuiz(quizId,questionList);
    
  }

  public void AddQuestionToQuiz(int quizId,List<Question> questionList){
    if(questionList == null){ return ;}
    else if(quizId == 0){ return ;}
    else if(questionList.Count() == 0){ return ;}
    else{
      SqliteCommand command;
      foreach (Question question in questionList)
      {
        //(Id , quizId , text , answer , type , data )";
        if (question is MultipleChoiceQuestion)
        {
          MultipleChoiceQuestion mcq = (MultipleChoiceQuestion)question;
          command = new SqliteCommand("INSERT OR IGNORE INTO Questions(quizId, type, text, answer , data) VALUES(@quizId, @type, @text, @answer, @data)", _connection);
          command.Parameters.AddWithValue("@quizId", quizId);
          command.Parameters.AddWithValue("@type", typeof(MultipleChoiceQuestion).ToString());
          command.Parameters.AddWithValue("@text", mcq.GetText());
          command.Parameters.AddWithValue("@answer", mcq.GetCorrectAnswer());
          command.Parameters.AddWithValue("@data", mcq.GetStringOptions() );
          command.ExecuteNonQuery();
        }
        else if (question is TrueFalseQuestion)
        {
          TrueFalseQuestion tfq = (TrueFalseQuestion)question;
          command = new SqliteCommand("INSERT OR IGNORE INTO Questions(quizId, type, text, answer) VALUES(@quizId, @type, @text, @answer)", _connection);
          command.Parameters.AddWithValue("@quizId", quizId);
          command.Parameters.AddWithValue("@type", typeof(TrueFalseQuestion).ToString());
          command.Parameters.AddWithValue("@text", tfq.GetText());
          command.Parameters.AddWithValue("@answer", tfq.GetCorrectAnswer());
          command.ExecuteNonQuery();
        }
        else
        {
          Question q = (Question)question;
          command = new SqliteCommand("INSERT OR IGNORE INTO Questions(quizId, type, text, answer) VALUES(@quizId, @type, @text, @answer)", _connection);
          command.Parameters.AddWithValue("@quizId", quizId);
          command.Parameters.AddWithValue("@type", typeof(Question).ToString());
          command.Parameters.AddWithValue("@text", q.GetText());
          command.Parameters.AddWithValue("@answer", q.GetCorrectAnswer());
          command.ExecuteNonQuery();
        }
      }
    }
  

  }

  public List<Quiz> GetUserQuizzes(User user)
  {
    // Get quiz from database
    SqliteCommand command = new SqliteCommand("SELECT * FROM Quizzes WHERE userId=@userId", _connection);
    command.Parameters.AddWithValue("@userId", user.GetId());    
    SqliteDataReader reader = command.ExecuteReader();

    List<Quiz> quizzes = new List<Quiz>();
      while (reader.Read())
      {
        //"Id, title , description , duration ,  passMark , userId)";
        int id = reader.GetInt32(0);
        string title = reader.GetString(1);
        string description =  (!reader.IsDBNull(2))?reader.GetString(2):"";
        int duration = reader.GetInt32(3);
        int passMark = reader.GetInt32(4);
        int authorId = reader.GetInt32(5);
        Quiz quiz = new Quiz(id,title,description,duration,passMark,authorId);
        quizzes.Add(quiz);        
      }
      reader.Close();
    return quizzes;
  }

  public List<Quiz> GetQuizzes()
  {
    // Get quiz from database
    SqliteCommand command = new SqliteCommand("SELECT * FROM Quizzes", _connection);
    SqliteDataReader reader = command.ExecuteReader();
    List<Quiz> quizzes = new List<Quiz>();
      while (reader.Read())
      {
        //"Id, title , description , duration ,  passMark , userId)";
        int id = reader.GetInt32(0);
        string title = reader.GetString(1);
        string description =  (!reader.IsDBNull(2))?reader.GetString(2):"";
        int duration = reader.GetInt32(3);
        int passMark = reader.GetInt32(4);
        int authorId = reader.GetInt32(5);
        Quiz quiz = new Quiz(id,title,description,duration,passMark,authorId);
        quizzes.Add(quiz);        
      }
      reader.Close();
    return quizzes;
  }

  
  public List<Question> GetQuizQuestion(Quiz quiz)
  {
    SqliteCommand command = new SqliteCommand("SELECT * FROM Questions WHERE quizId = @quizId;", _connection);
    command.Parameters.AddWithValue("@quizId", quiz.GetId());
    SqliteDataReader reader = command.ExecuteReader();
    List<Question>  questions = new List<Question>();
    while (reader.Read())
    {
      //Id , quizId ,type , text , answer , data
      int id = reader.GetInt32(0);
      int quizID = reader.GetInt32(1);
      string type = (!reader.IsDBNull(2))?reader.GetString(2):typeof(Question).ToString();
      string text = reader.GetString(3);
      string answer = reader.GetString(4);
      Question question;

      switch (type)
      {
        case "MultipleChoiceQuestion":
          string data =  (!reader.IsDBNull(5))?reader.GetString(5):"";
          question = new MultipleChoiceQuestion(id,quizID,text,answer,data);
          questions.Add(question);
          break;
        case "TrueFalseQuestion":
          question = new TrueFalseQuestion(id,quizID,text,(answer == "True"));
          questions.Add(question);
          break;
        default:
          question = new Question(id,quizID,text,answer);
          questions.Add(question);
          break;
      }
    }
    reader.Close();
    return questions;
  }


  public void UpdateQuestion( Question question){
    if(question == null){ return ;}
    else if(question.GetId() == 0){ return ;}
    else if(question.GetQuizId() == 0){ return ;}
    else{
      SqliteCommand command;
        //(Id , quizId , text , answer , type , data )";
        if (question is MultipleChoiceQuestion)
        {
          MultipleChoiceQuestion mcq = (MultipleChoiceQuestion)question;
          command = new SqliteCommand("UPDATE Questions SET answer = @answer , data = @data WHERE Id = @Id", _connection);
          command.Parameters.AddWithValue("@answer", mcq.GetCorrectAnswer());
          command.Parameters.AddWithValue("@data", mcq.GetStringOptions() );
          command.Parameters.AddWithValue("@Id", mcq.GetId());
          command.ExecuteNonQuery();
        }
        else
        {
          command = new SqliteCommand("UPDATE Questions SET answer = @answer  WHERE Id = @Id", _connection);
          command.Parameters.AddWithValue("@answer", question.GetCorrectAnswer());
          command.Parameters.AddWithValue("@Id", question.GetId());
          command.ExecuteNonQuery();
        }
    }
  }



  public void DeleteQuiz(Quiz quiz){
    if(quiz.GetId() == 0){return ;}
    else{
      SqliteCommand command = new SqliteCommand("DELETE FROM Questions WHERE quizId=@quizId", _connection);
      command.Parameters.AddWithValue("@quizId", quiz.GetId());
      command.ExecuteNonQuery();

      command = new SqliteCommand("DELETE FROM Quizzes WHERE Id=@Id", _connection);
      command.Parameters.AddWithValue("@Id", quiz.GetId());
      command.ExecuteNonQuery();
    }
  }

  public void DeleteQuestion(Question question){
    if(question.GetId() == 0){return ;}
    else{
      SqliteCommand command = new SqliteCommand("DELETE FROM Questions WHERE Id=@Id", _connection);
      command.Parameters.AddWithValue("@Id", question.GetId());
      command.ExecuteNonQuery();
    }
  }



  public void AddQuizResult(QuizResult quizResult)
  {
    //userId, quizId, correctAnswers, totalQuestions , score, time, quizData, data
    SqliteCommand command = new SqliteCommand("INSERT INTO Results (userId, quizId, correctAnswers, totalQuestions , score, time, quizData, data) VALUES(@userId, @quizId, @correctAnswers, @totalQuestions , @score, @time, @quizData, @data)", _connection);
    
    command.Parameters.AddWithValue("@userId", _user.GetId());
    command.Parameters.AddWithValue("@quizId", quizResult.GetQuizId());
    command.Parameters.AddWithValue("@correctAnswers", quizResult.GetCorrectAnswers());
    command.Parameters.AddWithValue("@totalQuestions", quizResult.GetTotalQuestions());
    command.Parameters.AddWithValue("@score",quizResult.GetScore() );
    command.Parameters.AddWithValue("@time", quizResult.GetTime());
    command.Parameters.AddWithValue("@quizData", quizResult.GetQuizDataToString());
    command.Parameters.AddWithValue("@data", quizResult.GetResultDataToString());
    command.ExecuteNonQuery();
    
  }

  public List<QuizResult> GetStudentResults(Student user)
  {
    SqliteCommand command = new SqliteCommand("SELECT * FROM Results WHERE userId = @userId;", _connection);
    command.Parameters.AddWithValue("@userId", user.GetId());
    SqliteDataReader reader = command.ExecuteReader();
    List<QuizResult>  results = new List<QuizResult>();
    while (reader.Read())
    {
     //Id , userId, quizId, correctAnswers, totalQuestions , score, time (nullable), quizData, data
      int id = reader.GetInt32(0);
      int userId = reader.GetInt32(1);
      int quizID = reader.GetInt32(2);
      int correctAnswers = reader.GetInt32(3);
      int totalQuestions = reader.GetInt32(4);
      double score = reader.GetDouble(5);
      int time = (!reader.IsDBNull(6))?reader.GetInt32(6):0;
      string quizData = reader.GetString(7);
      string data = reader.GetString(8);
      QuizResult result = new QuizResult(id,userId,quizID,correctAnswers,totalQuestions,time,quizData,data);
      results.Add(result);
    }
    reader.Close();
    return results;
  }

  public void DeleteQuizResult(QuizResult result){
    if(result.GetId() == 0){return ;}
    else{
      SqliteCommand command = new SqliteCommand("DELETE FROM Results WHERE Id=@Id", _connection);
      command.Parameters.AddWithValue("@Id", result.GetId());
      command.ExecuteNonQuery();
    }
  }
}


