namespace Cybertek.Settings
{
    public class AppSettingsBase : IAppSettings
    {
        public SqlDatabaseSettings SqlDatabase { get; set; }
    }
}
