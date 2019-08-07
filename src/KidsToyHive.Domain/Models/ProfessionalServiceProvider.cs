using KidsToyHive.Core.Enums;
using System;

namespace KidsToyHive.Domain.Models
{
    public class ProfessionalServiceProvider: BaseModel
    {
        public Guid ProfessionalServiceProviderId { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public ProfessionalServiceProviderType Type { get; set; }        
    }
}
