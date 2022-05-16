using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace QKNWZ1.WpfApp
{
    public class ObservableHike : ObservableObject
    {
        [PropToString]
        public Hike Hike { get; }

        public ObservableHike(Hike hike)
        {
            Hike = hike;

            // Does doing this (below) make sense [observable collection of observable type] ?
            ObservableWaypoints = new ObservableCollection<ObservableWaypoint>();
            foreach (Waypoint waypoint in hike.Waypoints)
            {
                ObservableWaypoints.Add(new ObservableWaypoint(waypoint));
            }
        }

        public string DateOfHike
        {
            get => Hike.DateOfHike;
            set => SetProperty(Hike.DateOfHike, value, Hike, (hike, dateOfHike) => hike.DateOfHike = dateOfHike);
        }
        public int Difficulty
        {
            get => Hike.Difficulty;
            set => SetProperty(Hike.Difficulty, value, Hike, (hike, difficulty) => hike.Difficulty = difficulty);
        }
        public ObservableCollection<ObservableWaypoint> ObservableWaypoints { get; set; }

        public override string ToString()
        {
            return this.PropsToString();
        }
    }
}
