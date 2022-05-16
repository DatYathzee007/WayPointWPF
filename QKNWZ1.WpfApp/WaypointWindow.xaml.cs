using System.Windows;
using System.Windows.Input;

namespace QKNWZ1.WpfApp
{
    /// <summary>
    /// Interaction logic for WaypointWindow.xaml
    /// </summary>
    public partial class WaypointWindow : Window
    {
        private WaypointVM waypointVM;
        private readonly System.Text.RegularExpressions.Regex regex = new("[^A-Z0-9]+");

        public WaypointWindow(MainWindowVM mainVM)
        {
            InitializeComponent();
            this.waypointVM = new WaypointVM(mainVM);
            DataContext = waypointVM;
        }

        /* If no items are selected in the ListBox,
         *  the window should open with empty values 
         *  and should add a new waypoint to the list
         *  when the dialog is closed
        */
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            /*waypointVM.ReturnWaypoint();*/
            // Use the Save button bound to SaveChangesCommand (auto gen.)
        }

        // The PointCode can only accept capital letters and numbers
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
