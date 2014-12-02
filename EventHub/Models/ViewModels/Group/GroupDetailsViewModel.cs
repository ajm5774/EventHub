using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventHub.Models
{
    public class GroupDetailsViewModel
    {
        public EventHub.Models.Group Group;
        public List<EventHub.Models.AdminRequest> AdminRequests;
        public string ViewingUserId;
    }
}