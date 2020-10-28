using Fluxor;
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
        private readonly HttpClient _http;

        public ItemDoneActionEffect(HttpClient http)
        {
            _http = http;
        }

        protected override async Task HandleAsync(ItemDoneAction action, IDispatcher dispatcher)
        {
            var response = await _http.PutAsJsonAsync<ItemDone>("/api/todolist/itemdone", new ItemDone()
            {
                Id = action.Id,
                IsDone = action.IsDone
            });

            if (response.IsSuccessStatusCode)
            {
                dispatcher.Dispatch(new LoadListsAction());
            }
        }
    }
}
