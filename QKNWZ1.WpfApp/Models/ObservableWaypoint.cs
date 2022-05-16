using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;

namespace QKNWZ1.WpfApp
{
    public class ObservableWaypoint : ObservableObject
    {
        /// <summary>
        /// Gets the non-observable <see cref="WpfApp.Waypoint"/>.
        /// </summary>
        [PropToString]
        public Waypoint Waypoint { get; }

        public ObservableWaypoint(Waypoint waypoint)
        {
            Waypoint = waypoint;
        }

        public string PointCode
        {
            get => Waypoint.PointCode;
            set => SetProperty(Waypoint.PointCode, value, Waypoint, (waypoint, pointCode) => waypoint.PointCode = pointCode);
        }
        public int X
        {
            get => Waypoint.X;
            set => SetProperty(Waypoint.X, value, Waypoint, (waypoint, x) => waypoint.X = x);
        }
        public int Y
        {
            get => Waypoint.Y;
            set => SetProperty(Waypoint.Y, value, Waypoint, (waypoint, y) => waypoint.Y = y);
        }
        public int Height
        {
            get => Waypoint.Height;
            set => SetProperty(Waypoint.Height, value, Waypoint, (waypoint, height) => waypoint.Height = height);
        }
        public string Category
        {
            get => Waypoint.Category;
            set => SetProperty(Waypoint.Category, value, Waypoint, (waypoint, category) => waypoint.Category = category);
        }

        public static IEnumerable<ObservableWaypoint> GetObservableWaypoints(string path)
        {
            foreach (Waypoint waypoint in Waypoint.GetWaypoints(path))
            {
                yield return new ObservableWaypoint(waypoint);
            }
        }

        public override string ToString()
        {
            return this.PropsToString();
        }
    }
}
