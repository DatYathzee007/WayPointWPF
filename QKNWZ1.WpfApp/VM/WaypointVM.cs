using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace QKNWZ1.WpfApp
{
    internal partial class WaypointVM : ObservableObject
    {
        [ObservableProperty]
        private Waypoint editedWaypoint;

        private bool isEditing;
        private int indexOfWaypoint;
        private MainWindowVM main;

        public WaypointVM(MainWindowVM mainVM)
        {
            this.main = mainVM;
            this.isEditing = mainVM.SelectedWaypoint is not null;
            Categories = mainVM.Categories;

            //SelectedWaypoint = mainVM.SelObsWaypoint;
            if (!isEditing) //(selectedWaypoint is null)
            {
                editedWaypoint = new Waypoint();
                /* ReturnWaypoint = () => mainVM.Waypoints.Add(editedWaypoint);*/
                //SelectedWaypoint = new ObservableWaypoint(new Waypoint());
                //ReturnWaypoint = () => mainVM.ObsWaypoints.Add(SelectedWaypoint);
            }
            else
            {
                this.editedWaypoint = new Waypoint(mainVM.SelectedWaypoint);
                this.indexOfWaypoint = main.Waypoints.IndexOf(main.SelectedWaypoint);
                /*  If an item is selected,
                 * that item's values should be bound to the controls in the window 
                 * and the item should be updated when the window is closed
                 */
                /*ReturnWaypoint = () => mainVM.SelectedWaypoint = editedWaypoint;*/
                //ReturnWaypoint = () => mainVM.SelObsWaypoint = SelectedWaypoint;
            }
        }

        //public ObservableWaypoint SelectedWaypoint { get; set; }
        //public Waypoint SelectedWaypoint { get => editedWaypoint; set => SetProperty(ref editedWaypoint, value); }

        /// <summary>
        /// The <see cref="QKNWZ1.WpfApp.Waypoint.Category"/> should be a drop-down list with the values - [easy, medium, hard]
        /// </summary>
        public static string[] Categories { get; private set; }

        /// <summary>
        /// If no items are selected, the window should open with empty values and should add a new waypoint to the list.<br/>
        /// If an item is selected, its values should be bound to the controls in the window and the item should be updated.
        /// </summary>
        [ICommand]
        public void SaveChanges()
        {
            if (!isEditing)
            {
                this.main.Waypoints.Add(new Waypoint(this.editedWaypoint));
            }
            else
            {
                this.main.Waypoints[this.indexOfWaypoint] = editedWaypoint;
            }
        }
    }
}
