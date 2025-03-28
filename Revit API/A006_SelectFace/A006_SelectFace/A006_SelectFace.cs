using System;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace A006_SelectFace
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class A006_SelectFace : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            try
            {
                // Chọn một mặt từ mô hình Revit
                Reference faceRef = uidoc.Selection.PickObject(ObjectType.Face, "Vui lòng chọn một mặt!");
                Element element = doc.GetElement(faceRef);
                GeometryObject geoObject = element.GetGeometryObjectFromReference(faceRef);

                if (geoObject is Face face)
                {
                    TaskDialog.Show("Revit", "Bạn đã chọn một mặt thuộc về: " + element.Name);
                }
                else
                {
                    TaskDialog.Show("Revit", "Không thể xác định mặt đã chọn!");
                }

                return Result.Succeeded;
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                TaskDialog.Show("Revit", "Bạn đã hủy chọn mặt.");
                return Result.Cancelled;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
    }
}