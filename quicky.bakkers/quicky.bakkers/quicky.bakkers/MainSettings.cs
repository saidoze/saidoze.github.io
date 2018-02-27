using System;
using System.Collections.Generic;
using System.Text;

namespace quicky.bakkers
{
    public enum ContentPageEnum
    {
        None, Leaderboard, PlayerMatchesOverview
    }

    public static class MainSettings
    {
        public static ContentPageEnum LastVisitedContentPage { get; set; }
    }
}
