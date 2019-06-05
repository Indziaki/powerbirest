using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardPBI.Models
{
    public class ReportModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EmbedUrl { get; set; }
        public string AccessToken { get; set; }
    }
}
