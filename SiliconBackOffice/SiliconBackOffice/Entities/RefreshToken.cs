using System;
using System.Collections.Generic;

namespace SiliconBackOffice.Entities;

public partial class RefreshToken
{
    public string RefreshToken1 { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime ExpiryDate { get; set; }
}
