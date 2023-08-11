using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox_Program.Models
{
    //TODO rewrite Request_Status and Result_Status
    public class StatusModel
    {
        public string he_id { get; set; }
        public Request_Status request_status { get; set; }
        public string status_update_url { get; set; }
        public Result_Status result { get; set; }
        public string context { get; set; }
    }
}
