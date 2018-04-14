namespace Core.Entities
{
    public class Message: BaseEntity
    {
        public int MessageId { get; set; }
        public int? ConversationId { get; set; }
        public int? ToProfileId { get; set; }
        public int? FromProfileId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public Conversation Conversation { get; set; }
    }
}
