using ExpenseReport.DTO;

namespace ExpenseReport.Helpers
{
    public class UserManagement
    {
        public static User GetManager(int userId)
        {
            // Simulate getting users manager
            return new User() { Id = 2, Name = "Manager2" };
        }
    }
}
