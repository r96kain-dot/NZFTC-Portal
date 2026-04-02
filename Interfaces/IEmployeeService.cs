using NZFTC_Portal.Models;

namespace NZFTC_Portal.Interfaces
{
    public interface IEmployeeService
    {
        Employee? GetEmployeeByUserId(int userId);
        EmployeeRecord? GetEmployeeRecordByUserId(int userId);
        bool UpdateOwnProfile(int userId, string phoneNumber, string address, string emergencyContact);
    }
}