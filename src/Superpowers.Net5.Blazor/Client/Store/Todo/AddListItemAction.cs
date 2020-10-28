using Fluxor;
using MediatR;
using Superpowers.Net5.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Superpowers.Net5.Blazor.Client.Store.Todo
{
    public record AddListItemAction
    {
        public int ListId { get; init; }

        public string Title { get; init; }
    }


    public class AddListItemActionEffect : Effect<AddListItemAction>
    {

        private readonly HttpClient _http;

        public AddListItemActionEffect(HttpClient http)
        {
            _http = http;
        }

        protected override async Task HandleAsync(AddListItemAction action, IDispatcher dispatcher)
        {
            var postResponse = await _http.PostAsJsonAsync<CreateTodoListItem>(
                "/api/todolist/createitem",
                new CreateTodoListItem { ListId = action.ListId, Title = action.Title }
            );

            if (postResponse.IsSuccessStatusCode)
            {
                dispatcher.Dispatch(new LoadListsAction());
            }
        }
    }


}
