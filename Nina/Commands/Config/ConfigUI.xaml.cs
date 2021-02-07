using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Logging.Core;

namespace Nina.Info
{
    /// <summary>
    /// Interaction logic for Config.xaml
    /// </summary>
    public partial class ConfigUI : Window
    {
        public ConfigUI(Document doc, List<WallType> types)
        {
            InitializeComponent();

            double wallTypeTolerance = Settings.Default.Tolerance;
            if (wallTypeTolerance == null || wallTypeTolerance < 0) wallTypeTolerance = 1;
            TextBox_tolerance.Text = wallTypeTolerance.ToString();

            WallType wallTypeSelected = null;
            List<string> typeNames = types.Select(type => type.Name).ToList();
            ComboBox_WallTypes.ItemsSource = typeNames;
            wallTypeSelected = types.Where(type => type.Name == Settings.Default.WallTypeSelected).FirstOrDefault();
            if (wallTypeSelected !=null) ComboBox_WallTypes.SelectedIndex = (int)typeNames.IndexOf(wallTypeSelected.Name);
            else
            {
                wallTypeSelected = types.Where(t => t.FamilyName.Contains("Basic")).FirstOrDefault();
                if (wallTypeSelected == null) ComboBox_WallTypes.SelectedIndex = 0;
            }

            double ViewRangeStep = Settings.Default.ViewRangeStep;
            if (ViewRangeStep == null || ViewRangeStep < 0) ViewRangeStep = 0.5;
            TextBox_ViewRangeStep.Text = ViewRangeStep.ToString();
        }

        private void Button_Ok(object sender, RoutedEventArgs e)
        {
            Settings.Default.WallTypeSelected = ComboBox_WallTypes.SelectedItem as string;
            try
            {
                Settings.Default.Tolerance = Convert.ToDouble(TextBox_tolerance.Text); 
            } catch(Exception ex)
            {
                // write the error here    
            }

            try
            {
                Settings.Default.ViewRangeStep= Convert.ToDouble(TextBox_ViewRangeStep.Text);
            }
            catch (Exception ex)
            {
                // write the error here    
            }

            Settings.Default.Save();
            this.Close();
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
