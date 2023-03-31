public class Time
{
    private DateTime _startTime;
    private DateTime _endTime;
    private int _countdown;

    public void SetCountdown(int countdown)
    {
        _countdown = countdown;
    }
    public void Start()
    {
        _startTime = DateTime.Now;
    }
    public void Stop()
    {
        _endTime = DateTime.Now;
    }


    public bool CheckCountdown()
    {
        if(_countdown != 0){
            Stop();
            TimeSpan TimecountdownTime = new TimeSpan(0,_countdown,0);
            return (GetElapsed() >= TimecountdownTime );
        }else{
            return true;
        }
    }

    public TimeSpan GetElapsed()
    {
        return _endTime - _startTime;
    }

    public int GetMinutesElapsed(){
        return (int) GetElapsed().TotalMinutes;
    }

    public string GetTimeElapsed(){
        string hours = ((int)GetElapsed().Hours >= 1)?$"{(int)GetElapsed().Hours} hours ":"";
        string min = ((int)GetElapsed().Minutes >= 1)?$"{(int)GetElapsed().Minutes} minutes ":"";
        string sec = ((int)GetElapsed().Seconds >= 1)?$"{(int)GetElapsed().Seconds} seconds ":"";
        return $"{hours}{min}{sec}";
    }
}
