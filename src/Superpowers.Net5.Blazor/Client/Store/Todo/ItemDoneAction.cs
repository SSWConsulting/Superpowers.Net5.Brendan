using Fluxor;
using Microsoft.AspNetCore.SignalR.Client;
using Superpowers.Net5.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Superpowers.Net5.Blazor.Client.Store.Todo
{
    public record ItemDoneAction
    {
        public int Id { get; init; }

        public bool IsDone { get; init; }
    }


    public class ItemDoneActionEffect : Effect<ItemDoneAction>
    {
        private readonly HubConnection _hubConnection;

        public ItemDoneActionEffect(HubConnection hubConnection)
        {
            _hubConnection = hubConnection;
        }

        protected override async Task HandleAsync(ItemDoneAction action, IDispatcher dispatcher)
        {
            await _hubConnection.SendAsync("ItemDone", new ItemDone { Id = action.Id, IsDone = action.IsDone });
            // no action. signal R should receive update separately
        }
    }
}
