using System;
using System.Collections.Generic;

namespace NZFTC_Portal.Models;

public partial class Holiday
{
    public int HolidayId { get; set; }

    public string HolidayName { get; set; } = null!;

    public DateOnly HolidayDate { get; set; }
}
