using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;

namespace C0011_Parameter_Beam_Column
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class C0011_Parameter_Beam_Column
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_StructuralColumns);
            IList<Element> columnList = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();

            StringBuilder output = new StringBuilder("Tất cả các Structural Column trong BIM model: " + columnList.Count + "\n\n");

            foreach (Element e1 in columnList)
            {
                string elemName = e1.Id.ToString() + "\n";
                ElementType type = doc.GetElement(e1.GetTypeId()) as ElementType;

                Parameter h1 = type.LookupParameter("h");
                double hh1 = ChangeUnitFeetToMillimeter(h1.AsDouble());
                Parameter b = type.LookupParameter("b");
                double bb1 = ChangeUnitFeetToMillimeter(b.AsDouble());

                elemName = elemName + "h = " + hh1.ToString() + "\n";
                elemName = elemName + "b = " + bb1.ToString() + "\n";
                output.Append(elemName);
                output.AppendLine();
            }

            MessageBox.Show(output.ToString());
            return Result.Succeeded;
        }

        static double ChangeUnitFeetToMillimeter(double Value)
        {
            return Math.Round(UnitUtils.ConvertFromInternalUnits(Value, UnitTypeId.Millimeters), 2);
        }
    }
}