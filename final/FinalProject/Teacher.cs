public class Teacher : User {
    
    public Teacher(string username, string password) : base(username, password) {
    }
    public Teacher(int userId, string username, string password) : base(userId,username, password) {
    }
}