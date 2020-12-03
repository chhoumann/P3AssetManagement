namespace AssetManagement.Models.AssetHolder
{
    public sealed class AssetHolder : IAssetHolder
    {
        public string Label { get; }

        public string Username { get; }

        public string Department { get; }
        
        public AssetHolder(string label, string username, string department)
        {
            Label = label;
            Username = username;
            Department = department;
        }

        public override string ToString() => $"{nameof(Label)}: {Label}, {nameof(Username)}: {Username}, {nameof(Department)}: {Department}";

        public override bool Equals(object obj)
        {
            if (obj is AssetHolder otherAssetHolder)
            {
                return Label == otherAssetHolder.Label;
            }

            return false;
        }
    }
}
