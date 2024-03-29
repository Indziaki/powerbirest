﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardPBI.Models
{
    public class AppSettings
    {
        public string AuthorityUri { get; set; }
        public string ResourceUrl { get; set; }
        public string RedirectUrl { get; set; }
        public string ApiUrl { get; set; }
        public string ApplicationId { get; set; }
        public string LoggingRequestUrl { get; set; }
        public string GroupId { get; set; }
        public string Secret { get; set; }
    }
}
