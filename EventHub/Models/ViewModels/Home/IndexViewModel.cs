using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventHub.Models
{
    public class IndexViewModel
    {
        public string PicturePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public School School { get; set; }
    }
}