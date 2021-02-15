using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nina.Revit
{
    public class FamilyInstance
    {
        public static void SetDiameterByDimension(UIDocument uiDoc, Document doc, double diameter)
        {
            ElementId level = new FilteredElementCollector(doc).OfClass(typeof(Level)).WhereElementIsNotElementType().ToElementIds().FirstOrDefault();
            //get the first pipe type
            ElementClassFilter filter = new ElementClassFilter(typeof(PipeType));
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.WherePasses(filter);

            IList<Element> elements = collector.ToElements();
            PipeType pipeType = elements[0] as PipeType;

            double dblnewValue = 3.28084; //Value for 100mm

            using (Transaction t = new Transaction(doc, "Transaction Name"))
            {
                t.Start();
                // DO something
                pipeType.get_Parameter(BuiltInParameter.RBS_PIPE_DIAMETER_PARAM).Set(0.49213);
                t.Commit();
            }


            uiDoc.PostRequestForElementTypePlacement(pipeType);




        }
    }
}
