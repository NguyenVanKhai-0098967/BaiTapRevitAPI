using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace C0012_Parameter_Wall
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class C0012_Parameter_Wall : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
            IList<Element> wallList = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();

            StringBuilder output = new StringBuilder("Tất cả các Wall trong BIM model: " + wallList.Count + "\n\n");

            foreach (Element e1 in wallList)
            {
                string elemName = e1.Id.ToString() + "\n";
                ElementType type = doc.GetElement(e1.GetTypeId()) as ElementType;

                Parameter width1 = type.LookupParameter("Width");
                double width11 = ChangeUnitFeetToMillimeter(width1.AsDouble());           
                elemName = elemName + "Width = " + width11.ToString() + "\n";              
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

