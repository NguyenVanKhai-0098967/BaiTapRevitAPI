using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace A003_SelectEdge
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class A003_SelectEdge : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            IList<Reference> referenceCollection = uidoc.Selection.PickObjects(ObjectType.Edge);
            MessageBox.Show("Ban da lua chon cac diem points:\n" + sb.ToString());
            return Result.Succeeded;
        }
    }
}