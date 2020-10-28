﻿using MediatR;
using Microsoft.AspNetCore.SignalR;
using Superpowers.Net5.Models.Commands;
using Superpowers.Net5.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superpowers.Net5.WebApi.Hubs
{
    public class TodoHub : Hub
    {

        private readonly IMediator _mediator;

        public TodoHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task ItemDone(ItemDone command)
        {
            await _mediator.Send(command);
            var newLists = await _mediator.Send(new GetTodoLists());

            await Clients.All.SendAsync("ListsChanged", newLists);
        }

    }
}
