using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox_Program.Models
{
    //TODO rewrite all class names
    public class Result_Base
    {
        public string compile_status { get; set; }
    }
    public class Result_Post : Result_Base 
    {
        public Run_Status_Base run_status { get; set; }
    }
    public class Result_Status : Result_Base
    {
        public Run_Status run_status { get; set; }
    }
    public class Run_Status_Base
    {
        public string status { get; set; }
    }
    public class Run_Status : Run_Status_Base
    {
        public string output { get; set; }
        public string status_detail { get; set; }
        public float time_used { get; set; }
        public int memory_used { get; set; }
    }
    public class Request_Status
    {
        public string code { get; set; }
        public string message { get; set; }
    }
}
