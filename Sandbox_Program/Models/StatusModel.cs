using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox_Program.Models
{
    public class StatusModel
    {
        public string he_id { get; set; }
        public RequestStatus request_status { get; set; }
        public string status_update_url { get; set; }
        public StatusResult result { get; set; }
        public string context { get; set; }
    }
}
