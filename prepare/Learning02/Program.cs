using System;

class Program
{
    static void Main(string[] args)
    {
        Job job1 = new Job();
        job1._jobTitle = "Web developer";
        job1._company = "Freelance";
        job1._startYear = 2020;
        job1._endYear = 2022;

        Job job2 = new Job();
        job2._jobTitle = "Outpatient Medical Coder";
        job2._company = "Skep Foundation";
        job2._startYear = 2022;
        job2._endYear = 2023;

        Resume myResume = new Resume();
        myResume._name = "Guy-Jeef TUMBA";

        myResume._jobs.Add(job1);
        myResume._jobs.Add(job2);
        myResume.Display();
    }
}