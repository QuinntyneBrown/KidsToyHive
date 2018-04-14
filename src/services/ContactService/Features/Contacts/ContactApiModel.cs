using Core.Entities;

namespace ContactService.Features.Contacts
{
    public class ContactApiModel
    {        
        public int ContactId { get; set; }
        public string Name { get; set; }

        public static ContactApiModel FromContact(Contact contact)
        {
            var model = new ContactApiModel();
            model.ContactId = contact.ContactId;
            model.Name = contact.Name;
            return model;
        }
    }
}
