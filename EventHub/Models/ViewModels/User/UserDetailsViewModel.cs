using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventHub.Models.ViewModels.User
{
    public class UserDetailsViewModel
    {
        public string UserId{ set; get; }
        public string UserName { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string SchoolName { set; get; }

    }
}