using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Superpowers.Net5.Blazor.Client.Store.Todo
{
    public record DeleteListAction
    {
        public int Id { get; init; }
    }

    public class DeleteListActionEffect : Effect<DeleteListAction>
    {
        private readonly HttpClient _http;

        public DeleteListActionEffect(HttpClient http)
        {
            _http = http;
        }

        protected override async Task HandleAsync(DeleteListAction action, IDispatcher dispatcher)
        {
            var delResponse = await _http.DeleteAsync($"/api/todolist/{action.Id}");
            if (delResponse.IsSuccessStatusCode)
            {
                dispatcher.Dispatch(new LoadListsAction());
            }
        }
    }
}
