using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Forms;

namespace B002_ListDoiTuong
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class Class1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Danh sách các loại đối tượng cần lọc
            Dictionary<BuiltInCategory, string> categoryNames = new Dictionary<BuiltInCategory, string>
            {
                { BuiltInCategory.OST_StructuralColumns, "Cột" },
                { BuiltInCategory.OST_StructuralFraming, "Dầm" },
                { BuiltInCategory.OST_Floors, "Sàn" },
                { BuiltInCategory.OST_Walls, "Tường" },
                { BuiltInCategory.OST_Doors, "Cửa " }
            };

            Dictionary<BuiltInCategory, int> elementCount = categoryNames.ToDictionary(kvp => kvp.Key, kvp => 0);

            StringBuilder output = new StringBuilder("Danh sách các đối tượng trong BIM model:\r\n");

            foreach (var category in categoryNames.Keys)
            {
                // Lọc phần tử theo từng loại
                FilteredElementCollector collector = new FilteredElementCollector(doc);
                ElementCategoryFilter filter = new ElementCategoryFilter(category);
                IList<Element> elementsInCategory = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();

                elementCount[category] = elementsInCategory.Count;

                // Ghi thông tin vào chuỗi kết quả
                if (elementsInCategory.Count > 0)
                {
                    output.AppendLine($"{categoryNames[category]}: {elementsInCategory.Count} elements");
                }
            }

            // Hiển thị kết quả
            TaskDialog.Show("Revit", output.ToString());
            return Result.Succeeded;
        }
    }
}

