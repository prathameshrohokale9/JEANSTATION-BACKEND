using System;
using System.Collections.Generic;

namespace JeanStationAPI.Models;

public partial class Employee
{
    public int EmpId { get; set; }

    public string EmpName { get; set; } = null!;

    public string EmpEmail { get; set; } = null!;

    public string EmpPhoneNo { get; set; } = null!;

    public string EmpUserName { get; set; } = null!;

    public string EmpPwd { get; set; } = null!;

    public int? StoreId { get; set; }
}
