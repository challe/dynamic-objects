using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DynamicObjects
{
    public class YamlDeserializer
    {
        public T DeserializeConfiguration<T>()
        {
            using (var input = new StreamReader("settings.yaml"))
            {
                Deserializer deserializer = new DeserializerBuilder()
                    .WithNamingConvention(new UnderscoredNamingConvention())
                    .Build();

                T deserialized = deserializer.Deserialize<T>(input);

                return deserialized;
            }
        }
    }
}
