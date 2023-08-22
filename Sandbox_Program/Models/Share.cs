using System.Runtime.Serialization;

namespace Sandbox_Program.Models
{
    [DataContract]
    public class CompilationResult
    {
        [DataMember(Name = "compile_status")]
        public string CompileStatus { get; set; }
    }
    public class PostResult : CompilationResult
    {
        [DataMember(Name = "run_status")]
        public ExecutionStatusBase RunStatus { get; set; }
    }
    public class StatusResult : CompilationResult
    {
        [DataMember(Name = "run_status")]
        public ExecutionStatus RunStatus { get; set; }
    }
    public class ExecutionStatusBase
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }
    }
    public class ExecutionStatus : ExecutionStatusBase
    {
        [DataMember(Name = "output")]
        public string Output { get; set; }

        [DataMember(Name = "status_detail")]
        public string StatusDetail { get; set; }

        [DataMember(Name = "time_used")]
        public float TimeUsed { get; set; }

        [DataMember(Name = "memory_used")]
        public int MemoryUsed { get; set; }
    }
    public class RequestStatus
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}
