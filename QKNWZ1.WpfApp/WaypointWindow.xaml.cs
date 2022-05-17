using System.Windows;
using System.Windows.Input;

namespace QKNWZ1.WpfApp
{
    /// <summary>
    /// Interaction logic for WaypointWindow.xaml
    /// </summary>
    public partial class WaypointWindow : Window
    {
        private readonly System.Text.RegularExpressions.Regex regex = new("[^A-Z0-9]+");

        /* If no items are selected in the ListBox,
         *  the window should open with empty values 
         *  and should add a new waypoint to the list
         *  when the dialog is closed
        */
        public WaypointWindow(Waypoint waypoint)
        {
            InitializeComponent();
            DataContext = new WaypointVM(waypoint);
        }

        // The PointCode can only accept capital letters and numbers
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
