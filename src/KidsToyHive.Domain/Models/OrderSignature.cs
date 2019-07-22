using System;
using System.Collections.Generic;
using System.Text;

namespace KidsToyHive.Domain.Models
{
    public class OrderSignature
    {
        public Guid OrderSignatureId { get; set; }
        public Guid OrderId { get; set; }
        public byte[] Bytes { get; set; }
        public string FullName { get; set; }
    }
}
