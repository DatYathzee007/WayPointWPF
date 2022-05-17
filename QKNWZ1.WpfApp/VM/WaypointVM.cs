using CommunityToolkit.Mvvm.ComponentModel;

namespace QKNWZ1.WpfApp
{
    internal partial class WaypointVM : ObservableObject
    {
        private static readonly string[] categories = { "Easy", "Medium", "Hard" };

        [ObservableProperty]
        private Waypoint editedWaypoint;

        public WaypointVM(Waypoint waypoint)
        {
            /*  If an item is selected,
             * that item's values should be bound to the controls in the window 
             * and the item should be updated when the window is closed
             */
            editedWaypoint = waypoint;
            Categories = categories;
        }

        public static string[] Categories { get; private set; }
    }
}
