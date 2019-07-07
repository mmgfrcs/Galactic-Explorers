using GalacticExplorers.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticExplorers.JSONUtilities
{
    public enum DangerLevel
    {
        Neutral, Friendly, Hostile = -1
    }
    public class Location
    {
        public DangerLevel dangerLevel;
        public Point position;
        public List<string> storyIds;

        public static List<Location> LoadAll(string filename)
        {
            
            List<Location> loclist = new List<Location>();
            string[] locs = File.ReadAllLines($"Locations/{filename}.txt");
            foreach(string loc in locs)
            {
                string[] locentry = loc.Split(' ');
                List<string> ids = new List<string>();
                if (locentry.Length > 3) ids = locentry.Skip(3).ToList();
                loclist.Add(new Location() {
                    dangerLevel = (DangerLevel)int.Parse(locentry[0]),
                    position = new Point(double.Parse(locentry[1], CultureInfo.CreateSpecificCulture("en-US")),
                        double.Parse(locentry[2], CultureInfo.CreateSpecificCulture("en-US"))),
                    storyIds = ids
                });

            }

            return loclist;
        }
    }
}
