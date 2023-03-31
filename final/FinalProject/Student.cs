public class Student : User {
    public Student(string username, string password) : base(username, password) {
    }
    public Student(int userId, string username, string password) : base(userId,username, password) {
    }
    
}