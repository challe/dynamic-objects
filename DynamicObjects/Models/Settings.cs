using System;
using System.Collections.Generic;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DynamicObjects.Models
{
    public class Settings
    {
        public Storage Storage { get; set; }

        public string Name { get; set; }
 
        [YamlMember(Alias = "objects")]
        public List<DynamicObject> DynamicObjects { get; set; }
    }
}
