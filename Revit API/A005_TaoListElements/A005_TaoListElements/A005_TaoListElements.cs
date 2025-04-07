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
    public class A005_TaoListElements : IExternalCommand
    {
       
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            ICollection<ElementId> selectedIds = uidoc.Selection.GetElementIds();

            TaskDialog.Show("Revit", "Số lượng đối tượng được chọn: " + selectedIds.Count.ToString());
            ICollection<ElementId> selectedWallIds = new List<ElementId>();
            ICollection<ElementId> SelectedFloor = new List<ElementId>();
            ICollection<ElementId> SelectedColumn = new List<ElementId>();
            FilteredElementCollector collector = new FilteredElementCollector(document);
            ICollection<Element> elementsColumns = collector.OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_StructuralColumns).ToElements();
            foreach (ElementId id in selectedIds)
            {
                Element elements1 = uidoc.Document.GetElement(id);

                if (elements1 is Wall)

                {
                    selectedWallIds.Add(id);
                }
                if (elements1 is Floor)

                {
                    SelectedFloor.Add(id);
                }
               
            }
            uidoc.Selection.SetElementIds(selectedWallIds);

            if (0 != selectedWallIds.Count)
            {
                TaskDialog.Show("Revit", selectedWallIds.Count.ToString() + "Đối tượng Tường đã được chọn!");
            }
            else
            {
                TaskDialog.Show("Revit", "Không có đối tượng tường nào được chọn!");
            }
            return Result.Succeeded;

            uiDocument.Selection.SetElementIds(SelectedFloor);

            if (0 != SelectedFloor.Count)
            {
                TaskDialog.Show("Revit", SelectedFloor.Count.ToString() + "Đối tượng Tường đã được chọn!");
            }
            else
            {
                TaskDialog.Show("Revit", "Không có đối tượng tường nào được chọn!");
            }
            return Result.Succeeded;
        }
    }
    }
}
