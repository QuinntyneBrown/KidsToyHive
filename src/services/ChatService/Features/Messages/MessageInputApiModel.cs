using System;

namespace ChatService.Features.Messages
{
    public class MessageInputApiModel
    {
        public int? ConversationId { get; set; }
        public int FromId { get; set; }
        public int ToProfileId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
