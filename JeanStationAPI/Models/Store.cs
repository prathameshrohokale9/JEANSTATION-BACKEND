using System;
using System.Collections.Generic;

namespace JeanStationAPI.Models;

public partial class Store
{
    public int StoreId { get; set; }

    public string StoreName { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string PhoneNo { get; set; } = null!;
}
