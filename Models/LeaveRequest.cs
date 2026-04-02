using System;
using System.Collections.Generic;

namespace NZFTC_Portal.Models;

public partial class LeaveRequest
{
    public int LeaveRequestId { get; set; }

    public int EmployeeId { get; set; }

    public int? AdminId { get; set; }

    public string LeaveType { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string? Reason { get; set; }

    public string Status { get; set; } = null!;

    public virtual Admin? Admin { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
