using System.Collections.Generic;

namespace Core.Entities
{
    public class Conversation: BaseEntity
    {
        public int ConversationId { get; set; }
        public ICollection<Profile> Profiles { get; set; } = new HashSet<Profile>();
        public ICollection<Message> Messages { get; set; } = new HashSet<Message>();
    }
}
