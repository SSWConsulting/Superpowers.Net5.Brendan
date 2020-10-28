using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Superpowers.Net5.Blazor.Client.Store.Todo
{
    public record DeleteListItemAction
    {
        public int Id { get; set; }
    }

    public class DeleteListItemActionEffect : Effect<DeleteListItemAction>
    {
        private readonly HttpClient _http;

        public DeleteListItemActionEffect(HttpClient http)
        {
            _http = http;
        }

        protected override async Task HandleAsync(DeleteListItemAction action, IDispatcher dispatcher)
        {
            var delResponse = await _http.DeleteAsync($"/api/todolist/item/{action.Id}");
            if (delResponse.IsSuccessStatusCode)
            {
                dispatcher.Dispatch(new LoadListsAction());
            }
        }
    }
}

