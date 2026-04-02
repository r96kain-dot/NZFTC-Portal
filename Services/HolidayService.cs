using NZFTC_Portal.Interfaces;
using NZFTC_Portal.Models;

namespace NZFTC_Portal.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly AppDbContext _context;

        public HolidayService(AppDbContext context)
        {
            _context = context;
        }

        public List<Holiday> GetAllHolidays()
        {
            return _context.Holidays
                .OrderBy(h => h.HolidayDate)
                .ToList();
        }
    }
}