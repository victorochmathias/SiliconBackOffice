using System;
using System.Collections.Generic;

namespace SiliconBackOffice.Entities;

public partial class NotificationEntity
{
    public string Email { get; set; } = null!;

    public bool SubscribeStatus { get; set; }

    public bool UseDarkmode { get; set; }
}
