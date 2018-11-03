using ObjectLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectLibrary.ExtensionMethods
{
    public static class DynamicObjectExtension
    {
        public static void AddDefaultFields(this List<DynamicObject> list)
        {
            foreach (DynamicObject dynamicObject in list)
            {
                if(!dynamicObject.Fields.Any(x => x.Name == "Id"))
                {
                    dynamicObject.Fields.Insert(0, new Field("Id", InputType.Number));
                }

                dynamicObject.Fields.Add(new Field("Created", InputType.DateTime, true));
                dynamicObject.Fields.Add(new Field("CreatedBy", InputType.Number, true));
                dynamicObject.Fields.Add(new Field("Modified", InputType.DateTime, true));
                dynamicObject.Fields.Add(new Field("ModifiedBy", InputType.Number, true));
            }
        }

        public static void CreateTypes(this List<DynamicObject> list)
        {
            foreach (DynamicObject dynamicObject in list)
            {
                CustomTypeBuilder.CreateType(dynamicObject);
            }
        }
    }
}
