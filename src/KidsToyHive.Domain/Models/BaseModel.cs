namespace KidsToyHive.Domain.Models
{
    public class BaseModel
    {
        public string PartitionKey { get; set; }
        public int Version { get; set; }
    }
}
