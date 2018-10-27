﻿using System.Collections.Generic;
using YamlDotNet.Serialization;

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