using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Logging.Core;
using System;
using System.IO;

namespace Nina.Selection
{
    [Transaction(TransactionMode.Manual)]
    public class WallByDimension : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            Log log = new Log
            {
                Tool = "Wall By Dimension",
                Document = doc.Title,
                Revit = commandData.Application.Application.VersionName,
                UserName = commandData.Application.Application.Username,
                Nina = Settings.Default.Nina
            };

            try
            {

                XYZ p1 = uidoc.Selection.PickPoint("Pick the first point");
                XYZ p2 = uidoc.Selection.PickPoint("Pick the second point");

                double distance = p1.DistanceTo(p2);

                Nina.Revit.FamilyType.SelectWall(uidoc, doc, distance);

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

