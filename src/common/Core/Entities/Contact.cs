using System.Collections.Generic;

namespace Core.Entities
{
    public class Contact: BaseEntity
    {
        public int ContactId { get; set; }           
		public string Name { get; set; }
        public string EmailAddress { get; set; }
        public ICollection<ContactRequest> ContactRequests { get; set; } = new HashSet<ContactRequest>();
    }
}
