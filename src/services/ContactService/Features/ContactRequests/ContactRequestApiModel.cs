using Core.Entities;

namespace ContactService.Features.ContactRequests
{
    public class ContactRequestApiModel
    {        
        public int ContactRequestId { get; set; }
        public string Name { get; set; }

        public static ContactRequestApiModel FromContactRequest(ContactRequest contactRequest)
        {
            var model = new ContactRequestApiModel();
            model.ContactRequestId = contactRequest.ContactRequestId;
            model.Name = contactRequest.Name;
            return model;
        }
    }
}
