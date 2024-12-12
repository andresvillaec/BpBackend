﻿using Client.Application.Data;
using Client.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Application.Client.Queries.Get
{
    internal sealed class GetClientQueryHandler : IRequestHandler<GetClientQuery, ClientResponse>
    {
        private readonly IApplicantionDbContext _context;

        public GetClientQueryHandler(IApplicantionDbContext context)
        {
            _context = context;
        }

        public async Task<ClientResponse> Handle(GetClientQuery request, CancellationToken cancellationToken)
        {
            var client = await _context
                .Clients
                .Where(c => c.Id == request.Id)
                .Select(p => new ClientResponse(
                    p.Id,
                    p.Name,
                    p.Gender,
                    p.Age,
                    p.DocumentNumber,
                    p.Address,
                    p.Phone,
                    p.Password,
                    p.Status
                        )
                ).FirstOrDefaultAsync();

            if (client == null)
            {
                throw new ClientNotFoundException(request.Id);
            }

            return client;
        }
    }
}