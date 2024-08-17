using System;
using System.Collections.Generic;

namespace JeanStationAPI.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? CustId { get; set; }

    public string? OrderStatus { get; set; }

    public DateTime? OrderDate { get; set; }

    public double? Amount { get; set; }

    public string ItemCode { get; set; } = null!;

    public int? Qty { get; set; }

    public double? Price { get; set; }

    public double? ItemValue { get; set; }
}
