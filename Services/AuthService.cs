using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NZFTC_Portal.Interfaces;
using NZFTC_Portal.Models;

namespace NZFTC_Portal.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _passwordHasher = new PasswordHasher<User>();
        }

        public User? Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
                return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (result == PasswordVerificationResult.Failed)
                return null;

            _httpContextAccessor.HttpContext?.Session.SetInt32("UserId", user.UserId);
            _httpContextAccessor.HttpContext?.Session.SetString("Role", user.Role);
            _httpContextAccessor.HttpContext?.Session.SetString("FullName", user.FullName);

            return user;
        }

        public void Logout()
        {
            _httpContextAccessor.HttpContext?.Session.Clear();
        }

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(new User(), password);
        }

        public bool VerifyPassword(User user, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result != PasswordVerificationResult.Failed;
        }
    }
}