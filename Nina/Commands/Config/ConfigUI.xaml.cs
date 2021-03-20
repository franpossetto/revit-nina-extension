﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            this.Topmost = true;

            Checkbox_CreateWallType.IsChecked = Settings.Default.CreateWallType;

            double wallTypeTolerance = Settings.Default.Tolerance;
            string wallTypePrefix = Settings.Default.WallTypePrefix;

            if (wallTypeTolerance == null || wallTypeTolerance < 0) wallTypeTolerance = 1;
            TextBox_tolerance.Text = wallTypeTolerance.ToString();

            if (wallTypePrefix == null) wallTypePrefix = "Ninin";
            WallTypeName.Text = wallTypePrefix;


            if (Checkbox_CreateWallType.IsChecked == true)
            {
                WallTypeName.IsEnabled = true;
                TextBox_tolerance.IsEnabled = true;
                ComboBox_WallTypes.IsEnabled = true;
            }
            else
            {
                WallTypeName.IsEnabled = false;
                TextBox_tolerance.IsEnabled = false;
                ComboBox_WallTypes.IsEnabled = false;
            }

            WallType wallTypeSelected = null;
            List<string> typeNames = types.Select(type => type.Name).ToList();
            ComboBox_WallTypes.ItemsSource = typeNames;


            wallTypeSelected = types.Where(type => type.Name == Settings.Default.WallTypeSelected).FirstOrDefault();
            if (wallTypeSelected != null) ComboBox_WallTypes.SelectedIndex = (int)typeNames.IndexOf(wallTypeSelected.Name);
            else
            {
                wallTypeSelected = types.Where(t => t.FamilyName.Contains("Basic")).FirstOrDefault();
                if (wallTypeSelected == null) ComboBox_WallTypes.SelectedIndex = 0;
            }

            if(ComboBox_WallTypes.SelectedIndex == -1) ComboBox_WallTypes.SelectedIndex = 0;

            Checkbox_ViewRangeTopBottom.IsChecked = Settings.Default.ViewRangeTopBottom;
            double ViewRangeStep = Settings.Default.ViewRangeStep;
            if (ViewRangeStep == null || ViewRangeStep < 0) ViewRangeStep = 0.5;
            TextBox_ViewRangeStep.Text = ViewRangeStep.ToString();
        }

        private void Button_Ok(object sender, RoutedEventArgs e)
        {
            Settings.Default.WallTypeSelected = ComboBox_WallTypes.SelectedItem as string;

            try
            {
                Settings.Default.WallTypePrefix = WallTypeName.Text;
            }
            catch (Exception ex)
            {
                // write the error here    
            } // Save Wall type selected

            try
            {
                Settings.Default.Tolerance = Convert.ToDouble(TextBox_tolerance.Text);
            }
            catch (Exception ex)
            {
                // write the error here    
            } // Save Wall type selected

            try
            {
                Settings.Default.ViewRangeStep = Convert.ToDouble(TextBox_ViewRangeStep.Text);
            }
            catch (Exception ex)
            {
                // write the error here    
            } // Save View Range Step

            try
            {
                Settings.Default.ViewRangeTopBottom = (bool) Checkbox_ViewRangeTopBottom.IsChecked;
            }
            catch (Exception ex)
            {
                // write the error here    
            } // Move View Range Top and Bottom

            try
            {
                Settings.Default.CreateWallType = (bool)Checkbox_CreateWallType.IsChecked;
            }
            catch (Exception ex)
            {
                // write the error here    
            } // Create Wall Type

            Settings.Default.Save();
            this.Close();
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void createWallType_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Default.CreateWallType = true;
            WallTypeName.IsEnabled = true;
            TextBox_tolerance.IsEnabled = true;
            ComboBox_WallTypes.IsEnabled = true;
        }

        private void createWallType_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings.Default.CreateWallType = false;
            WallTypeName.IsEnabled = false;
            TextBox_tolerance.IsEnabled = false;
            ComboBox_WallTypes.IsEnabled = false;
        }

        private void MoveTopBottom_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Default.ViewRangeTopBottom = true;
        }

        private void MoveTopBottom_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings.Default.CreateWallType = false;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

    }
}
