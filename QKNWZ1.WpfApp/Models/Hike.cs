using System.Collections.Generic;
using System.Linq;

namespace QKNWZ1.WpfApp
{
    public class Hike
    {
        public Hike()
        {
            Waypoints = new List<Waypoint>();
        }

        public Hike(Hike other)
        {
            this.DateOfHike = other.DateOfHike;
            this.Difficulty = other.Difficulty;
            List<Waypoint> newWaypoints = new();
            foreach (Waypoint otherWaypoint in other.Waypoints)
            {
                newWaypoints.Add(new Waypoint(otherWaypoint));
            }
            Waypoints = newWaypoints;

            // The comfortable way:
            //Difficulty = (newWaypoints.Max(wp => wp.Height) - newWaypoints.Min(wp => wp.Height)) * newWaypoints.Count;

            // A bit less taxing:
            int length = newWaypoints.Count;
            int minH = newWaypoints[0].Height;
            int maxH = newWaypoints[0].Height;
            // Start from 1, [0] is already used.
            for (int i = 1; i < length; i++)
            {
                int currentH = newWaypoints[i].Height;

                if (currentH < minH)
                    minH = currentH;

                else if (currentH > maxH)
                    maxH = currentH;
            }
            Difficulty = (maxH - minH) * length;
        }

        public Hike(string dateOfHike, IEnumerable<Waypoint> waypoints)
        {
            DateOfHike = dateOfHike;
            Waypoints = waypoints;
        }

        [PropToString]
        public string DateOfHike { get; set; }

        [PropToString]
        public int Difficulty { get; set; }

        public IEnumerable<Waypoint> Waypoints { get; set; }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new(this.PropsToString(false));
            foreach (Waypoint waypoint in Waypoints)
            {
                sb.Append(waypoint.PropsToString());
            }
            return sb.Append(" }").ToString();
        }
    }
}
