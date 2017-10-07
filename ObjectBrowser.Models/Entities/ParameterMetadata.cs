using System.Runtime.Serialization;

namespace ObjectBrowser.Models.Entities
{
    [DataContract(IsReference = true)]
    public class ParameterMetadata
    {
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
    }
}