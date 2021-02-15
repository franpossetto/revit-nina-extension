using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.DB.Mechanical;
using System.Text.RegularExpressions;

namespace Nina.Revit
{
    public class Selector
    {
        public static string PadNumbers(string input)
        {
            return Regex.Replace(input, "[0-9]+", match => match.Value.PadLeft(10, '0'));
        }
       
        public static void ElementSwitch(UIDocument uiDoc, Document doc, bool order)
        {

            //Get Selected Elements IDs.
            ICollection<ElementId> selectedIds = uiDoc.Selection.GetElementIds();

            Element element = doc.GetElement(selectedIds.FirstOrDefault());
            ElementId categoryId = element.Category.Id;


            ElementId elementtypeid = element.GetTypeId();
            List<ElementId> elementTypes = element.GetValidTypes().ToList();

            List<ElementTypeSelector> etype = new List<ElementTypeSelector>();
            foreach (ElementId eid in elementTypes)
            {
                ElementTypeSelector e = new ElementTypeSelector();
                ElementType el = doc.GetElement(eid) as ElementType;
                e.elementtype = el;
                e.family = el.FamilyName;
                e.type = el.Name;
                e.key = e.family + "-" + e.type;

                etype.Add(e);
            }

            IList<ElementTypeSelector> elementTypesOrdered = etype.OrderBy(x => PadNumbers(x.key)).ToList();


            int index = 0;
            int n = 0;
            //up
            if (order)
            {
                foreach (ElementTypeSelector et in elementTypesOrdered)
                {
                    if (et.elementtype.Id == elementtypeid)
                    {
                        index = n - 1;
                        if (index == -1)
                            index = elementTypesOrdered.Count() - 1;
                    }
                    n++;
                }
            }
            else
            {
                foreach (ElementTypeSelector et in elementTypesOrdered)
                {
                    if (et.elementtype.Id == elementtypeid)
                    {
                        index = n + 1;
                        if (index >= elementTypesOrdered.Count())
                            index = 0;
                    }

                    n++;
                }
            }


            ElementId newSelectedId = elementTypesOrdered.ToList()[index].elementtype.Id;
            ElementType selectedElementType = doc.GetElement(newSelectedId) as ElementType;

            using (Transaction t = new Transaction(doc, "ElementType Switch"))
            {
                t.Start();
                    element.ChangeTypeId(selectedElementType.Id);
                t.Commit();
            }
        }
        public static void WallSwitch(UIDocument uiDoc, Document doc, bool order)
        {

            #region Selection Stuff

            //Get Selected Elements IDs.
            ICollection<ElementId> selectedIds = uiDoc.Selection.GetElementIds();

            #endregion

            Element element = doc.GetElement(selectedIds.FirstOrDefault());
            if (!element.Category.Name.Contains("Wall"))
                return;


            ElementId elementtypeid = element.GetTypeId();
            List<ElementId> wallTypeId = element.GetValidTypes().ToList();
            List<WallType> walls = wallTypeId.Select(x => ((WallType)doc.GetElement(x))).ToList();

            List<WallFamilyType> wallTypeList = new List<WallFamilyType>();
            foreach (WallType wallType in walls)
            {
                WallFamilyType w = new WallFamilyType();
                w.walltype = wallType;
                w.family = wallType.FamilyName;
                w.type = wallType.Name;
                w.key = w.family + "-" + w.type;

                wallTypeList.Add(w);
            }

            IList<WallFamilyType> elementTypesOrdered = wallTypeList.OrderBy(x => PadNumbers(x.key)).ToList();

            Wall wall = element as Wall;
            WallType elementType = wall.WallType;
            ElementId elementTypeId = elementType.Id;


            int index = 0;
            int n = 0;
            //up
            if (order)
            {
                foreach (WallFamilyType wft in elementTypesOrdered)
                {
                    if (wft.walltype.Id == elementTypeId)
                    {
                        index = n - 1;
                        if (index == -1)
                            index = elementTypesOrdered.Count() - 1;
                    }

                    n++;
                }
            }
            else
            {
                foreach (WallFamilyType wft in elementTypesOrdered)
                {
                    if (wft.walltype.Id == elementTypeId)
                    {
                        index = n + 1;
                        if (index >= elementTypesOrdered.Count())
                            index = 0;
                    }

                    n++;
                }
            }

            ElementId newSelectedId = elementTypesOrdered.ToList()[index].walltype.Id;
            ElementType selectedElementType = doc.GetElement(newSelectedId) as ElementType;

            using (Transaction t = new Transaction(doc, "Walltype Switch"))
            {
                t.Start();
                wall.WallType = selectedElementType as WallType;
                t.Commit();
            }
        }
    }
}

