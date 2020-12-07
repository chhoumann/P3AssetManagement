namespace AssetManagement.Server.Shared
{
    public partial class MainLayout
    {
        private bool navDrawerOpened;

        private void ButtonClicked() => navDrawerOpened = !navDrawerOpened;
    }
}