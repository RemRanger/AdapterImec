using System;

namespace AdapterImec.Domain.Entities
{
    public class Message
    {
        public long Id { get; set; }
        public string MessageId { get; set; }
        public string CustomerScheme { get; set; }
        public string CustomerValue { get; set; }
        public string ProvidingCompanyScheme { get; set; }
        public string ProvidingCompanyValue { get; set; }
        public string MessageType { get; set; }
        public DateTime DateCreated { get; set; }
        public string Creator { get; set; }
        public DateTime DateReceived { get; set; }
        public string FileContent { get; set; }
    }
}
