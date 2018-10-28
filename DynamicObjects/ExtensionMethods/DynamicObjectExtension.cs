using DynamicObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicObjects.ExtensionMethods
{
    public static class DynamicObjectExtension
    {
        public static void AddIdentityColumns(this List<DynamicObject> list)
        {
            foreach (DynamicObject dynamicObject in list)
            {
                if(!dynamicObject.Fields.Any(x => x.Name == "Id"))
                {
                    dynamicObject.Fields.Insert(0, new Field("Id", InputType.Number));
                }

                dynamicObject.Fields.Add(new Field("Created", InputType.DateTime));
                dynamicObject.Fields.Add(new Field("CreatedBy", InputType.Number));
                dynamicObject.Fields.Add(new Field("Modified", InputType.DateTime));
                dynamicObject.Fields.Add(new Field("ModifiedBy", InputType.Number));
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
