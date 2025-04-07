using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Forms;

namespace A005_TaoListElements
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class B001_ElementsByFilter : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            FilteredElementCollector collector = new FilteredElementCollector(doc);

            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_StructuralColumns);

            IList<Element> columnList = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();
            StringBuilder output = new StringBuilder("Tất cả các Structural Column trong BIM model :\r\n");

            foreach (Element elem in columnList)
            {
                FamilyInstance familyInstance = elem as FamilyInstance;
                FamilySymbol familySymbol = familyInstance.Symbol;
                Family family = familySymbol.Family;
                string elemName = "Category: " + elem.Category.Name + ", Family: " + family.Name + ", Name:" + elem.Name + "\r\n";
                output.Append(elemName);
            }

            MessageBox.Show(output.ToString());
            return Result.Succeeded;
        }
    }
}
