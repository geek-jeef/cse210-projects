using System;

public class Word
{
    private string _word;
    private bool _visibility;
    private Word()
    {
        _word = null;
        _visibility = true;
    }
    public Word(string word, bool visibility = true)
    {
        _word = word;
        _visibility = visibility;
    }
    public void Hide()
    {
        _visibility = false;
        string blanks = new string('_', _word.Length);
        _word = _word.EndsWith(".") ? blanks = blanks.Remove(0, 1) + ".":
                _word.EndsWith(",") ? blanks = blanks.Remove(0, 1) + ",":
                _word.EndsWith(":") ? blanks = blanks.Remove(0, 1) + ":":
                _word.EndsWith(";") ? blanks = blanks.Remove(0, 1) + ";":
                blanks;
    }
    public string GetWord()
    {
        return _word;
    }
    public bool GetVisibility()
    {
        return _visibility;
    }
    public void SetWord(string word)
    {
        _word = word;
    }
    public void SetVisibility(bool visibility)
    {
        _visibility = visibility;
    }
}