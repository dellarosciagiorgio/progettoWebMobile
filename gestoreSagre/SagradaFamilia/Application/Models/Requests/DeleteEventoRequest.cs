﻿using Application.Abstraction.Requests;

namespace Application.Models.Requests
{
    public class DeleteEventoRequest : BaseRequest
    {
        public int IdEvento { get; set; }
    }
}