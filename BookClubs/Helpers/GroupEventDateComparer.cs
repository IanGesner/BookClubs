using BookClubs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookClubs.Helpers
{
    public class GroupEventDateComparer : IComparer<GroupEvent>
    {
        private static GroupEventDateComparer _instance = new GroupEventDateComparer();
        private GroupEventDateComparer() { }
        public static GroupEventDateComparer Instance { get { return _instance; } }

        public int Compare(GroupEvent x, GroupEvent y)
        {
            if (x == null && y == null)
                return 0;
            if (x == null)
                return -1;
            if (y == null)
                return 1;

            return DateTime.Compare(x.DateTime, y.DateTime);
        }
    }
}