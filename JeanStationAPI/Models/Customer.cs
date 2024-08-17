using System;
using System.Collections.Generic;

namespace JeanStationAPI.Models;

public partial class Customer
{
    public int CustId { get; set; }

    public string CustName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNo { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Pwd { get; set; } = null!;
}
