using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventHub.Models
{
    public class EventViewModel
    {
        public Event AnEvent { get; set; }
        public bool ReceiveNotifications { get; set; }
    }
}