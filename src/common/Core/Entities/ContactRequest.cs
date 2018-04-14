namespace Core.Entities
{
    public class ContactRequest
    {
        public int ContactRequestId { get; set; }           
		public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}
