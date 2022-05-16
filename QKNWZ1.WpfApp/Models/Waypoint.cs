using System.Collections.Generic;
using System.IO;

namespace QKNWZ1.WpfApp
{
    public class Waypoint
    {
        public Waypoint()
        {
        }

        public Waypoint(Waypoint other)
        {
            this.PointCode = other.PointCode;
            this.X = other.X;
            this.Y = other.Y;
            this.Height = other.Height;
            this.Category = other.Category;
        }

        [PropToString]
        public string PointCode { get; set; }
        [PropToString]
        public int X { get; set; }
        [PropToString]
        public int Y { get; set; }
        [PropToString]
        public int Height { get; set; }
        [PropToString]
        public string Category { get; set; }

        public static IEnumerable<Waypoint> GetWaypoints(string path)
        {
            List<Waypoint> waypoints = new();
            using StreamReader reader = new(path);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] cells = line.Split(',');
                Waypoint waypoint = new();
                waypoint.PointCode = cells[0];
                waypoint.X = int.Parse(cells[1]);
                waypoint.Y = int.Parse(cells[2]);
                waypoint.Height = int.Parse(cells[3]);
                waypoint.Category = cells[4];
                waypoints.Add(waypoint);
            }
            return waypoints;
        }

        public override string ToString()
        {
            return this.PropsToString();
        }
    }
}
