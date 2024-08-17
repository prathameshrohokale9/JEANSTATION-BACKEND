using System;
using System.Collections.Generic;

namespace JeanStationAPI.Models;

public partial class Item
{
    public string ItemCode { get; set; } = null!;

    public string ItemName { get; set; } = null!;

    public double Price { get; set; }

    public int? Quantity { get; set; }

    public int? StoreId { get; set; }

    public string? ItemImage { get; set; }
}
