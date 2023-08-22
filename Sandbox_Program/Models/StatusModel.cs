using System.Runtime.Serialization;

namespace Sandbox_Program.Models
{
    [DataContract]
    public class StatusModel
    {
        [DataMember(Name = "he_id")]
        public string HeId { get; set; }

        [DataMember(Name = "request_status")]
        public RequestStatus RequestStatus { get; set; }

        [DataMember(Name = "status_update_url")]
        public string StatusUpdateUrl { get; set; }

        [DataMember(Name = "result")]
        public StatusResult Result { get; set; }

        [DataMember(Name = "context")]
        public string Context { get; set; }
    }
}
