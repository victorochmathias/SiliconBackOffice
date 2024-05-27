using System;
using System.Collections.Generic;

namespace SiliconBackOffice.Entities;

public partial class Subscriber
{
    public string Email { get; set; } = null!;

    public bool AdvertisingUpdates { get; set; }

    public bool DailyNewletter { get; set; }

    public bool EventUpdates { get; set; }

    public bool Podcasts { get; set; }

    public bool StartupsWeekly { get; set; }

    public bool WeekInReview { get; set; }
}
