using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace QKNWZ1.WpfApp
{
    public partial class MainWindowVM : ObservableRecipient
    {
        private Waypoint selectedWaypoint;
        private Waypoint selectedWaypointInHike;

        [ObservableProperty]
        private Hike selectedHike; // this can be auto-generated because it is not needed in any custom logic

        public MainWindowVM()
        {
            WaypointsForHike = new ObservableCollection<Waypoint>();
            Waypoints = new ObservableCollection<Waypoint>(Waypoint.GetWaypoints("WaypointList.txt"));
            selectedHike = new Hike();
            Hikes = new ObservableCollection<Hike>();

            // Use a dialog window to edit the first ListBox
            //ModifyWaypoint = new RelayCommand(ModifyWaypointMethod);

            MoveToHike = new RelayCommand(AddToHikeMethod, () => SelectedWaypoint is not null && IsHardLimitObeyed());

            RemoveFromHike = new RelayCommand(RemoveFromHikeMethod, () => SelectedWaypointInHike is not null);

            SaveHike = new RelayCommand(SaveHikeMethod, () => !string.IsNullOrEmpty(selectedHike?.DateOfHike));
        }

        public ObservableCollection<Waypoint> Waypoints { get; set; }
        public ObservableCollection<Waypoint> WaypointsForHike { get; set; }
        public ObservableCollection<Hike> Hikes { get; set; }

        public Waypoint SelectedWaypoint
        {
            get => selectedWaypoint;
            set
            {
                if (SetProperty(ref selectedWaypoint, value))
                    (MoveToHike as IRelayCommand).NotifyCanExecuteChanged();
            }
        }

        public Waypoint SelectedWaypointInHike
        {
            get => selectedWaypointInHike;
            set
            {
                if (SetProperty(ref selectedWaypointInHike, value))
                    (RemoveFromHike as IRelayCommand).NotifyCanExecuteChanged();
            }
        }

        public string UserDateTime
        {
            get => selectedHike.DateOfHike;
            set
            {
                if (SetProperty(selectedHike.DateOfHike, value, selectedHike, (hike, strDate) => hike.DateOfHike = strDate))
                    (SaveHike as IRelayCommand).NotifyCanExecuteChanged();
            }
        }

        public ICommand MoveToHike { get; private set; }
        public ICommand RemoveFromHike { get; private set; }
        public ICommand SaveHike { get; private set; }

        [ICommand]
        private void ModifyWaypoint() // no special/custom logic so this can be auto-generated.
        {
            if (SelectedWaypoint is null)
            {
                SelectedWaypoint = new Waypoint();
                Waypoints.Add(SelectedWaypoint);
            }
            _ = new WaypointWindow(SelectedWaypoint).ShowDialog();
        }

        private bool IsHardLimitObeyed()
        {
            if (WaypointsForHike.Count < 1) return true;
            if (SelectedWaypoint.Category != "Hard") return true;

            try
            {
                if (WaypointsForHike[^1].Category == "Hard") return false;
                if (WaypointsForHike[^2].Category == "Hard") return false;
                if (WaypointsForHike[^3].Category == "Hard") return false;
            }
            catch (System.ArgumentOutOfRangeException) { }

            return true;
        }

        private void SaveHikeMethod()
        {
            Hike temp = new(UserDateTime, WaypointsForHike);
            Hikes.Add(new Hike(temp));
            WaypointsForHike.Clear();
        }
        private void RemoveFromHikeMethod()
        {
            WaypointsForHike.Remove(SelectedWaypointInHike);
            SelectedWaypointInHike = null;
        }
        private void AddToHikeMethod()
        {
            WaypointsForHike.Add(SelectedWaypoint);
            SelectedWaypoint = null;
        }
    }

    /*
     * In the future: https://egvijayanand.in/2022/05/09/mvvm-made-easy-with-microsoft-mvvm-toolkit-part-2/
     * 
     * Alternative, not fully working below:

    public partial class MainWindowVM
    {
        private ObservableWaypoint selObsWaypoint;
        private ObservableWaypoint selObsWaypointForObsHike;

        public MainWindowVM(string pathToWaypointList)
        {
            ObsWaypoints = new ObservableCollection<ObservableWaypoint>(ObservableWaypoint.GetObservableWaypoints(pathToWaypointList));
            ObsWaypointsInHike = new ObservableCollection<ObservableWaypoint>();
            SelObsHike = new ObservableHike(new Hike());
            ObsHikes = new ObservableCollection<ObservableHike>();

            CreateWaypoint = new RelayCommand(() => new WaypointWindow(this).ShowDialog());

            EditWaypoint = new RelayCommand(
                () => new WaypointWindow(this).ShowDialog(),
                () => SelObsWaypoint is not null);

            MoveToHike = new RelayCommand(
                () => ObsWaypointsInHike.Add(SelObsWaypoint),
                () => SelObsWaypoint is not null);

            RemoveFromHike = new RelayCommand(
                () => ObsWaypointsInHike.Remove(SelObsWaypoint),
                () => SelObsWaypointForObsHike is not null);

            SaveHike = new RelayCommand(
                () =>
                {
                    foreach (ObservableWaypoint obsWaypoint in ObsWaypointsInHike)
                    {
                        WaypointsForHike.Add(obsWaypoint.Waypoint);
                    }
                    ObsHikes.Add(new ObservableHike(new Hike { DateOfHike = SelObsHike.DateOfHike, Waypoints = WaypointsForHike }));
                    ObsWaypointsInHike.Clear();
                    WaypointsForHike.Clear();
                },
                () => !string.IsNullOrEmpty(SelObsHike?.DateOfHike));
        }

        public ObservableWaypoint SelObsWaypoint
        {
            get => selObsWaypoint;
            set
            {
                selObsWaypoint = value;
                (EditWaypoint as IRelayCommand).NotifyCanExecuteChanged();
                (MoveToHike as IRelayCommand).NotifyCanExecuteChanged();
            }
        }
        public ObservableWaypoint SelObsWaypointForObsHike
        {
            get => selObsWaypointForObsHike;
            set
            {
                selObsWaypointForObsHike = value;
                (RemoveFromHike as IRelayCommand).NotifyCanExecuteChanged();
            }
        }
        public ObservableHike SelObsHike { get; set; }
        public ObservableCollection<ObservableWaypoint> ObsWaypoints { get; set; }
        public ObservableCollection<ObservableWaypoint> ObsWaypointsInHike { get; set; }
        public ObservableCollection<ObservableHike> ObsHikes { get; set; }
    }
    */
}
