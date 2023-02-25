using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalTask
{
    [Transaction(TransactionMode.Manual)]
    public class AutoNumerate : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                var doc = commandData.Application.ActiveUIDocument.Document;
                List<Room> rooms = new FilteredElementCollector(doc)
                    .OfCategory(BuiltInCategory.OST_Rooms)
                    .OfType<Room>()
                    .ToList();

                int number= 1;
                Transaction transaction = new Transaction(doc, "Автоматическая нумерация помещений");
                transaction.Start();
                foreach (Room room in rooms)
                {
                    Parameter param = room.get_Parameter(BuiltInParameter.ROOM_NUMBER);
                    param.Set(number.ToString());
                    number++;
                }
                transaction.Commit();
            }
            catch
            {
                return Result.Failed;
            }

            return Result.Succeeded;
        }
    }
}
