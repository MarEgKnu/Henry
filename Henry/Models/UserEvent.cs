﻿using Henry.Interfaces;
using Henry.Services;

namespace Henry.Models
{
    public class UserEvent
    {
        public int UserEventId { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }

        public DateTime UserEventStart { get; set; }

        public DateTime UserEventEnd { get; set; }

        public bool Joined { get; set; }
    }
}
