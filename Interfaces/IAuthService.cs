using NZFTC_Portal.Models;

namespace NZFTC_Portal.Interfaces
{
    public interface IAuthService
    {
        User? Login(string email, string password);
        void Logout();
        string HashPassword(string password);
        bool VerifyPassword(User user, string password);
    }
}

