using NZFTC_Portal.Interfaces;
using NZFTC_Portal.Models;

namespace NZFTC_Portal.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public Employee? GetEmployeeByUserId(int userId)
        {
            return _context.Employees.FirstOrDefault(e => e.UserId == userId);
        }

        public EmployeeRecord? GetEmployeeRecordByUserId(int userId)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.UserId == userId);

            if (employee == null)
                return null;

            return _context.EmployeeRecords.FirstOrDefault(r => r.EmployeeId == employee.UserId);
        }

        public bool UpdateOwnProfile(int userId, string phoneNumber, string address, string emergencyContact)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.UserId == userId);

            if (employee == null)
                return false;

            var record = _context.EmployeeRecords.FirstOrDefault(r => r.EmployeeId == employee.UserId);

            if (record == null)
                return false;

            record.PhoneNumber = phoneNumber;
            record.Address = address;
            record.EmergencyContact = emergencyContact;

            _context.SaveChanges();
            return true;
        }
    }
}