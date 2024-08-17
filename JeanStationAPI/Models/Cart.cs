using System;
using System.Collections.Generic;

namespace JeanStationAPI.Models;

public partial class Cart
{
    public int CustId { get; set; }

    public string? ItemCode { get; set; }

    public double? Price { get; set; }

    public int? Qty { get; set; }

    public double? Value { get; set; }
}
