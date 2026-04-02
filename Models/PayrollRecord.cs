using System;
using System.Collections.Generic;

namespace NZFTC_Portal.Models;

public partial class PayrollRecord
{
    public int PayrollId { get; set; }

    public int EmployeeId { get; set; }

    public int? AdminId { get; set; }

    public decimal BaseSalary { get; set; }

    public decimal TaxRate { get; set; }

    public decimal Deductions { get; set; }

    public decimal NetPay { get; set; }

    public DateOnly PayDate { get; set; }

    public virtual Admin? Admin { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
