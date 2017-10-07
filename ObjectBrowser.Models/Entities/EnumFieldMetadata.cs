using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectBrowser.Models.Entities
{
    public class EnumFieldMetadata
    {
        public EnumFieldMetadata(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public int Value { get; set; }
    }
}
