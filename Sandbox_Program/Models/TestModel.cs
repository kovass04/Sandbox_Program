using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox_Program.Models
{
    public class TestModel
    {
        public string context { get; set; }
        public Result result { get; set; }
        public string he_id { get; set; }
        public string status_update_url { get; set; }
        public Request_Status request_status { get; set; }
    }

    public class Result
    {
        public Run_Status run_status { get; set; }
        public string compile_status { get; set; }
    }

    public class Run_Status
    {
        public string status { get; set; }
    }

    public class Request_Status
    {
        public string code { get; set; }
        public string message { get; set; }
    }

}
