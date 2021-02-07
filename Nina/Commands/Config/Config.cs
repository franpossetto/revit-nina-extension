using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Nina.Info
{
    [Transaction(TransactionMode.Manual)]
    public class Config : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                Document doc = commandData.Application.ActiveUIDocument.Document;
                UIDocument uidoc = commandData.Application.ActiveUIDocument;

                FilteredElementCollector collector = new FilteredElementCollector(doc);
                ElementClassFilter filter = new ElementClassFilter(typeof(WallType));
                collector.WherePasses(filter);

                List<WallType> types = collector.Cast<WallType>().ToList();

                ConfigUI configUI = new ConfigUI(doc, types);

                configUI.Show();

                return Autodesk.Revit.UI.Result.Succeeded;
            }
            catch(Exception ex)
            {
                TaskDialog.Show("Error", ex.Message);
                message = "Unexpected Exception thrown.";
                return Autodesk.Revit.UI.Result.Failed;
            }

        }
    }
}

