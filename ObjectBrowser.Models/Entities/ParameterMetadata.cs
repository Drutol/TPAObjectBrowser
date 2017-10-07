using System.Runtime.Serialization;

namespace ObjectBrowser.Models.Entities
{
    [DataContract(IsReference = true)]
    public class ParameterMetadata
    {
        public long Id { get; set; }

        public ParameterMetadata()
        {

        }

        public ParameterMetadata(string name, TypeMetadata typeMetadata)
        {
            Name = name;
            TypeMetadata = typeMetadata;
        }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public TypeMetadata TypeMetadata { get; set; }

        [IgnoreDataMember]
        public MethodMetadata MethodMetadata { get; set; }
    }
}