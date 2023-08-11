using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox_Program.Models
{
    //TODO rewrite Result_Post and Request_Status
    public class PostModel
    {
        public string context { get; set; }
        public Result_Post result { get; set; }
        public string he_id { get; set; }
        public string status_update_url { get; set; }
        public Request_Status request_status { get; set; }
    }
}
