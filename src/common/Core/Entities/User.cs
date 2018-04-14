using System;

namespace Core.Entities
{
    public class User: BaseEntity
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
