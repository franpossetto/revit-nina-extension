using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Nina.Revit;

namespace Nina
{
    public class NinaPanel
    {
        public static void Build(UIControlledApplication application, string tabName)
        {
            RibbonPanel ninaPanel = Ribbon.CreateRibbonPanel(application, "Nina ", tabName);
            #region Info Button

            PulldownButtonData info_button = new PulldownButtonData("info", "Info")
            {
                LargeImage = Icons.info_30
            };

            PushButtonData about_button = Ribbon
                .CreatePushButtonData("about", "About", "Nina.Info.About");
            PushButtonData repo_button = Ribbon
                .CreatePushButtonData("github", "Github Repository", "Nina.Info.Github");
            PushButtonData logs_button = Ribbon
                .CreatePushButtonData("logs", "Logs", "Nina.Info.Logs");
        
            PulldownButton info_pullDownButton = ninaPanel.AddItem(info_button) as PulldownButton;
            info_pullDownButton.AddPushButton(about_button);
            info_pullDownButton.AddPushButton(repo_button);
            info_pullDownButton.AddPushButton(logs_button);

            #endregion

            # region Config Button

            PushButtonData settings_button_data = Ribbon
                .CreatePushButtonData("settings", "Config", "Nina.Info.Config");

            PushButton settings_button = ninaPanel.AddItem(settings_button_data) as PushButton;
            settings_button.LargeImage = Icons.settings_30;

            #endregion

        }
    }
}
