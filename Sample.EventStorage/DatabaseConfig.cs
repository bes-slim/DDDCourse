namespace Sample.EventStorage
{
    public interface IDatabaseConfig
    {
        string ConnectionString { get; }
    }

    public class DatabaseConfig : IDatabaseConfig
    {
        public string ConnectionString { get { return "Data Source=tpe-t60;Initial Catalog=DDDCourse;Integrated Security=SSPI;"; } }
    }
}