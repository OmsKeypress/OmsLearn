namespace EmployeeDirectory.Model
{
    public static class Connection
    {
        public static string ConnectionString { get; set; }
        public static string ConnectionString1 { get; set; }
        public static string MongoConnectionString { get; set; }
        public static dynamic LoggerTimeSpan { get; set; } = 5;
    }

    
}


