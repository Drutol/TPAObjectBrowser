﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ObjectBrowser.Models.Enums;

namespace ObjectBrowser.Models.Entities
{
    [DataContract(IsReference = true)]
    public class TypeModifiers
    {
        public long Id { get; set; }

        [DataMember]
        public AccessLevel AccessLevel { get; set; }
        [DataMember]
        public bool IsSealed { get; set; }
        [DataMember]
        public bool IsAbstract { get; set; }

        [IgnoreDataMember]
        public TypeMetadata ParentType { get; set; }
    }
}
