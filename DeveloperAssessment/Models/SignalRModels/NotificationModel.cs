﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.SignalRModels
{
    public sealed class NotificationModel
    {
        public string UserId { get; set; }
        public string Message { get; set; }
    }
}
