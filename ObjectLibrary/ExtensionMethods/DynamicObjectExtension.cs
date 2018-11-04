using ObjectLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace ObjectLibrary.ExtensionMethods
{
    public static class DynamicObjectExtension
    {
        public static void AddDefaultFields(this List<DynamicObject> list)
        {
            foreach (DynamicObject dynamicObject in list)
            {
                if (!dynamicObject.Fields.Any(x => x.Name == "Id"))
                {
                    dynamicObject.Fields.Insert(0, new Field("Id", "Number"));
                }

                dynamicObject.Fields.Add(new Field("Created", "DateTime", true));
                dynamicObject.Fields.Add(new Field("CreatedBy", "Number", true));
                dynamicObject.Fields.Add(new Field("Modified", "DateTime", true));
                dynamicObject.Fields.Add(new Field("ModifiedBy", "Number", true));
            }
        }
    }
}