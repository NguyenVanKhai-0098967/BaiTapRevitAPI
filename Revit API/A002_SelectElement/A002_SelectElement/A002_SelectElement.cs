using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace A002_SelectElement
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class A002_SelectElement : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            
            ICollection<ElementId> selectElements = uidoc.Selection.GetElementIds();
            int totalCount = selectElements.Count;
            MessageBox.Show("Ban da chon " + totalCount.ToString() + "elements");
            return Result.Succeeded;      
        }
    }
}