using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EventHub.Models
{
    public class ChangeInfoModel
    {
        [Display(Name = "Email/Username")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        public List<SchoolListViewModel> Schools { set; get; }

        public int SelectedSchoolId { set; get; }

	}
}