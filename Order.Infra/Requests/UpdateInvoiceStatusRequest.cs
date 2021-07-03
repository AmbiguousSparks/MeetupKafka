﻿using MediatR;
using Order.Domain.Models.Enums;
using System;

namespace Order.Infra.Requests
{
    public class UpdateInvoiceStatusRequest : IRequest<UpdateInvoiceStatusRequest>
    {
        public Guid Id { get; set; }
        public InvoiceStatus Status { get; set; }
    }
}
