﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;
using Nina.Revit;

namespace Nina
{
    class ExternalApplication : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            /// <summary>
            /// Create a new Tab on Ribbon Bar.
            /// </summary>
            const string RIBBON_TAB = "BBr";
            Ribbon.CreateRibbonTab(application, RIBBON_TAB);

            /// <summary>
            /// Create a new Panel on Ribbon Tab.
            /// </summary>

            const string RIBBON_PANEL = "Scan to BIM";
            RibbonPanel ribbonPanel = Ribbon.CreateRibbonPanel(application, RIBBON_PANEL, RIBBON_TAB);

            #region Colors - Icons
            System.Drawing.Bitmap ico1 = Properties.Resources.dark;
            System.Windows.Media.Imaging.BitmapSource icon1 = Ribbon.Icon(ico1);

            System.Drawing.Bitmap ico2 = Properties.Resources.green;
            System.Windows.Media.Imaging.BitmapSource icon2 = Ribbon.Icon(ico2);

            System.Drawing.Bitmap ico3 = Properties.Resources.dark;
            System.Windows.Media.Imaging.BitmapSource icon3 = Ribbon.Icon(ico3);

            System.Drawing.Bitmap ico4 = Properties.Resources.green;
            System.Windows.Media.Imaging.BitmapSource icon4 = Ribbon.Icon(ico4);

            System.Drawing.Bitmap ico5 = Properties.Resources.red;
            System.Windows.Media.Imaging.BitmapSource icon5 = Ribbon.Icon(ico5);

            System.Drawing.Bitmap ico6 = Properties.Resources.gray;
            System.Windows.Media.Imaging.BitmapSource icon6 = Ribbon.Icon(ico6);

            System.Drawing.Bitmap ico7 = Properties.Resources.purple;
            System.Windows.Media.Imaging.BitmapSource icon7 = Ribbon.Icon(ico7);
            #endregion


            PulldownButtonData group1Data = new PulldownButtonData("PulldownGroup1", "Pulldown Group 1");
            group1Data.ToolTip = group1Data.Text;
            group1Data.Image = icon7;

            const string pointcloud_hide_name = "pointcloud_hide";
            const string pointcloud_hide_desc = "Hide Point Cloud";
            PushButtonData pointcloud_hide_button = Ribbon.CreatePushButtonData(pointcloud_hide_name, 
                                                                                pointcloud_hide_desc,
                                                                                "Nina.Hide");
            pointcloud_hide_button.Image = icon1;

            const string pointcloud_unhide_name = "pointcloud_unhide";
            const string pointcloud_unhide_desc = "Show Point Cloud";
            PushButtonData pointcloud_unhide_button = Ribbon.CreatePushButtonData(pointcloud_unhide_name,
                                                                                  pointcloud_unhide_desc,
                                                                                  "Nina.Unhide");
            pointcloud_unhide_button.Image = icon2;

            const string pointcloud_elevation_name = "pointcloud_elevation";
            const string pointcloud_elevation_desc = "Set Elevation";
            PushButtonData pointcloud_elevation_button = Ribbon.CreatePushButtonData(pointcloud_elevation_name,
                                                                                     pointcloud_elevation_desc,
                                                                                     "Nina.ColorMode.SetElevation");
           // pointcloud_elevation_button.Image = icon3;

            const string pointcloud_fixedColor_name = "pointcloud_fixedColor";
            const string pointcloud_fixedColor_desc = "Set Fixed Color";

            PushButtonData pointcloud_fixedColor_button = Ribbon.CreatePushButtonData(pointcloud_fixedColor_name,
                                                                                      pointcloud_fixedColor_desc,
                                                                                      "Nina.ColorMode.SetFixedColor");
           // pointcloud_fixedColor_button.Image = icon4;

            const string pointcloud_intensity_name = "pointcloud_intensity";
            const string pointcloud_intensity_desc = "Set Intensity";
            PushButtonData pointcloud_intensity_button = Ribbon.CreatePushButtonData(pointcloud_intensity_name,
                                                                                     pointcloud_intensity_desc,
                                                                                     "Nina.ColorMode.SetIntensity");
           // pointcloud_intensity_button.Image = icon5;

            const string pointcloud_noOverride_name = "pointcloud_noOverride";
            const string pointcloud_noOverride_desc = "Set No Override";
            PushButtonData pointcloud_noOverride_button = Ribbon.CreatePushButtonData(pointcloud_noOverride_name,
                                                                                      pointcloud_noOverride_desc,
                                                                                      "Nina.ColorMode.SetNoOverride");
           // pointcloud_noOverride_button.Image = icon6;

            const string pointcloud_normals_name = "pointcloud_normals";
            const string pointcloud_normals_desc = "Set Normals";
            PushButtonData pointcloud_normals_button = Ribbon.CreatePushButtonData(pointcloud_normals_name,
                                                                                   pointcloud_normals_desc,
                                                                                   "Nina.ColorMode.SetNormals");
            //pointcloud_normals_button.Image = icon7;





            // A PulldownButton data
            PulldownButtonData ChangeModeGroup = new PulldownButtonData("ChangeMode", "Change Color Mode");
            ChangeModeGroup.ToolTip = "PointCloud visibility change mode";
            ChangeModeGroup.Image = icon6;


            IList<RibbonItem> stackedGroup1 =  ribbonPanel.AddStackedItems(pointcloud_hide_button, 
                                                                           pointcloud_unhide_button,
                                                                           ChangeModeGroup);
            PulldownButton group1 = stackedGroup1[2] as PulldownButton;

            PushButton item11 = group1.AddPushButton(pointcloud_elevation_button) as PushButton;
            item11.LargeImage = icon3;

            PushButton item12 = group1.AddPushButton(pointcloud_fixedColor_button) as PushButton;
            item12.LargeImage = icon5;

            PushButton item13 = group1.AddPushButton(pointcloud_intensity_button) as PushButton;
            item13.LargeImage = icon4;

            PushButton item14 = group1.AddPushButton(pointcloud_noOverride_button) as PushButton;
            item14.LargeImage = icon6;

            PushButton item15 = group1.AddPushButton(pointcloud_normals_button) as PushButton;
            item15.LargeImage = icon7;


            ribbonPanel.AddSeparator();

            return Result.Succeeded;


        }
    }
}
