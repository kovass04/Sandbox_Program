using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox_Program.Models
{
    public class CompilationResult
    {
        public string compile_status { get; set; }
    }
    public class PostResult : CompilationResult
    {
        public ExecutionStatusBase run_status { get; set; }
    }
    public class StatusResult : CompilationResult
    {
        public ExecutionStatus run_status { get; set; }
    }
    public class ExecutionStatusBase
    {
        public string status { get; set; }
    }
    public class ExecutionStatus : ExecutionStatusBase
    {
        public string output { get; set; }
        public string status_detail { get; set; }
        public float time_used { get; set; }
        public int memory_used { get; set; }
    }
    public class RequestStatus
    {
        public string code { get; set; }
        public string message { get; set; }
    }
}
