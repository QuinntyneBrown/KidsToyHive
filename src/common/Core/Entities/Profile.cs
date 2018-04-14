namespace Core.Entities
{
    public class Profile: BaseEntity
    {
        public int ProfileId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }
}
