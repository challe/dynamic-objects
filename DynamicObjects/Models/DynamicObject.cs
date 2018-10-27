using System.Collections.Generic;

namespace DynamicObjects.Models
{
    public class DynamicObject
    {
        public DynamicObject()
        {
        }

        public DynamicObject(string name, List<Field> fields)
        {
            Name = name;
            Fields = fields;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Field> Fields { get; set; }
    }
}