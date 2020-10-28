namespace AssetManagement.Models
{
    public abstract class AssetHolder
    {
        public string Name { get; private set; }
        public string Department { get; private set; }

        public AssetHolder(string name) => Name = name;
    }
}
