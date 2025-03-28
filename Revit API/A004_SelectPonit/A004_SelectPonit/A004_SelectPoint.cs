using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace A004_SelectPoint
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class A004_SelectPoint : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;        
            XYZ point1 = uidoc.Selection.PickPoint();
            XYZ point2 = uidoc.Selection.PickPoint();
            XYZ point3 = uidoc.Selection.PickPoint();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(point1.ToString());
            sb.AppendLine(point2.ToString());
            sb.AppendLine(point3.ToString());
            MessageBox.Show("Ban da lua chon cac diem points:\n" + sb.ToString());
            return Result.Succeeded;
        }
    }
}