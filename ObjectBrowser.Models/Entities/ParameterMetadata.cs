namespace ObjectBrowser.Models.Entities
{
    public class ParameterMetadata
    {
        public ParameterMetadata(string name, TypeMetadata typeMetadata)
        {
            Name = name;
            TypeMetadata = typeMetadata;
        }

        public string Name { get; set; }
        public TypeMetadata TypeMetadata { get; set; }
    }
}