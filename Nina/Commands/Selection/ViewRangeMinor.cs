using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Logging.Core;

namespace Nina.Selection
{
    [Transaction(TransactionMode.Manual)]
    public class ViewRangeMinor : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            Log log = new Log
            {
                Tool = "Set View Range (-)",
                Document = doc.Title,
                Revit = commandData.Application.Application.VersionName,
                UserName = commandData.Application.Application.Username,
                Nina = Settings.Default.Nina
            };

            try
            {
                View activeView = doc.ActiveView as View;

                if (!(activeView is ViewPlan)) return Result.Cancelled;

                ViewPlan viewPlan = activeView as ViewPlan;
                PlanViewRange viewRange = viewPlan.GetViewRange();

                double currentOffset = viewRange.GetOffset(PlanViewPlane.CutPlane);
                double offsetValue = Settings.Default.ViewRangeStep;

                if ((currentOffset - offsetValue) > viewRange.GetOffset(PlanViewPlane.BottomClipPlane))
                    viewRange.SetOffset(PlanViewPlane.CutPlane, (currentOffset - offsetValue));
                else
                    TaskDialog.Show("Revit Nina Extension", "Top Clip plane is set below te cut plane");

                using (Transaction t = new Transaction(doc, string.Format("View Range -{0}", offsetValue)))
                {
                    t.Start();
                    viewPlan.SetViewRange(viewRange);
                    t.Commit();
                }
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

