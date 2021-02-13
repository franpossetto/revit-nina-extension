using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace Nina.Revit
{
    public static class Units
    {
        public static string TransformValueIfNecessary(Document doc, double _value)
        {
            double newValue = _value;

            // It can be IMPERIAL or METRIC
            DisplayUnit currentUnit = doc.DisplayUnitSystem;
            if(currentUnit.ToString() == "METRIC") newValue = _value / 3.281;
            return Math.Round(newValue, 2).ToString();
        }
    }
}
