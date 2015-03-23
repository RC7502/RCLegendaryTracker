using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LegendaryTracker.Helpers
{
    public class Helper
    {
        public static T GetRandomItem<T>(List<T> list) where T : new()
        {
            if (list.Any())
            {
                var rnd = new Random();
                var idx = rnd.Next(list.Count);
                var item = list[idx];
                list.RemoveAt(idx);
                return item;
            }
            return new T();
        }

        public static int VillainsGroupCount(int players)
        {
            switch (players)
            {
                case 1:
                    return 1;
                case 2:
                    return 2;
                case 3:
                case 4:
                    return 3;
                case 5:
                    return 4;
            }
            return 0;
        }

        public static int HenchmenGroupCount(int players)
        {
            switch (players)
            {
                case 1:
                case 2:
                case 3:
                    return 1;
                case 4:
                case 5:
                    return 2;
            }
            return 0;
        }

        public static List<string> RestrictedSchemeNames(int players)
        {
            var list = new List<string>();
            if (players == 1)
            {
                list.Add("Super Hero Civil War");
                list.Add("Negative Zone Prison Breakout");
            }

            return list;
        }
    }
}