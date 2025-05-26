namespace MyDoctorAppointment.Data.Configuration
{
    public static class Constants
    {
        public static readonly string AppSettingsPath = Path.Combine(
            AppContext.BaseDirectory,
            "Configuration", "appsettings.json"
        );
    }
}