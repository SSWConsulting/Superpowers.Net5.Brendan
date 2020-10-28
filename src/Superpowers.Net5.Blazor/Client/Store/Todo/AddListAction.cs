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
    public record AddListAction
    {
        public string Title { get; init; }
    }


    public class AddListActionEffect : Effect<AddListAction>
    {
        private readonly HttpClient _http;
        public AddListActionEffect(HttpClient http)
        {
            _http = http;
        }
        protected override async Task HandleAsync(AddListAction action, IDispatcher dispatcher)
        {
            var postResponse = await _http.PostAsJsonAsync<CreateTodoList>(
                "/api/todolist/create",
                new CreateTodoList { Title = action.Title }
            );

            if (postResponse.IsSuccessStatusCode)
            {
                dispatcher.Dispatch(new LoadListsAction());
            }
        }
    }
}
