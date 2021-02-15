using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using Nina.Logging;

namespace Nina.Selection
{
    [Transaction(TransactionMode.Manual)]
    public class SwitchUp : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            Log log = new Log
            {
                Tool = "Switch-up Element Type",
                Document = doc.Title,
                Revit = commandData.Application.Application.VersionName,
                UserName = commandData.Application.Application.Username,
                Nina = Settings.Default.Nina
            };

            try
            {

                Autodesk.Revit.UI.Selection.Selection selection = uidoc.Selection;
                //Nina.FamilyType.WallSwitch(uidoc, doc, true);
                Nina.Revit.Selector.ElementSwitch(uidoc, doc, true);
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

