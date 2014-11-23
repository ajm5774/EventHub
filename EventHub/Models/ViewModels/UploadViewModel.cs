using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventHub.Models
{
    public class UploadViewModel
    {
        public String Action { get; set; }
        public String Controller { get; set; }
        public bool AllowMultiple { get; set; }
        public List<HttpPostedFileBase> Files { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase File { get; set; }
        public int id { get; set; }
    }
}