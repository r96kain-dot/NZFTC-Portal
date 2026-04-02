using NZFTC_Portal.Models;

namespace NZFTC_Portal.Interfaces
{
    public interface IHolidayService
    {
        List<Holiday> GetAllHolidays();
    }
}