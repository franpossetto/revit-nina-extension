﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Nina.Revit;
using Logging.Core;

namespace Nina.Visibility
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]

    public class SetIntensity : IExternalCommand
    {
        /// <summary>
        ///     External Command
        /// </summary>
        /// <param name ="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Application app = uiApp.Application;
            Document doc = uiDoc.Document;

            Log log = new Log
            {
                Tool = "Set Color Mode (Intensity)",
                Document = doc.Title,
                Revit = commandData.Application.Application.VersionName,
                UserName = commandData.Application.Application.Username,
                Nina = Settings.Default.Nina
            };

            try
            {
                PointCloud.SetColorMode(doc, 2);
            }

            catch (System.Exception exp)
            {
                log.Message = exp.Message;
                log.Exception = exp;
                return Autodesk.Revit.UI.Result.Failed;
            }

            Logger.Write(log);
            return Autodesk.Revit.UI.Result.Succeeded;


        }
    }
}
