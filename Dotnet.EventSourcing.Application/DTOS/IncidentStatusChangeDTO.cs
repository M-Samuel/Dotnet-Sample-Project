﻿using Dotnet.EventSourcing.Domain.IncidentDomain;
using Dotnet.EventSourcing.Domain.UserDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.EventSourcing.Application.DTOS
{
    public class IncidentStatusChangeDTO
    {
        public Guid Id { get; set; }
        public UserDTO? ChangedBy { get; set; }
        public IncidentStatus OldStatus { get; set; }
        public IncidentStatus NewStatus { get; set; }
        public DateTime ChangedDateTime { get; set; }
    }
}