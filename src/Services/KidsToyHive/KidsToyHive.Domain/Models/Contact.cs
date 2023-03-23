using System;
using System.Collections.Generic;

namespace KidsToyHive.Domain.Models;

public class Contact : BaseModel
{
    public Guid ContactId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<ContactMessage> ContactMessages { get; set; }
        = new HashSet<ContactMessage>();
}
