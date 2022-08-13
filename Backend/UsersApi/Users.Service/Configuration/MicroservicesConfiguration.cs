namespace Users.Service.Configuration;

public class MicroservicesConfiguration
{
    public MicroserviceConfiguration Authentication { get; set; }
    public MicroserviceConfiguration Users { get; set; }


    public class MicroserviceConfiguration
    {
        public string Url { get; set; }
        public string Scheme { get; set; }
    }
}

