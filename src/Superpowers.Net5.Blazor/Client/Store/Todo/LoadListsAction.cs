using Fluxor;
using Superpowers.Net5.Models.Todo;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Superpowers.Net5.Blazor.Client.Store.Todo
{
    public class LoadListsAction
    {
    }

    public class LoadListsActionEffect : Effect<LoadListsAction>
    {
        private readonly HttpClient _http;

        public LoadListsActionEffect(HttpClient http)
        {
            _http = http;
        }

        protected override async Task HandleAsync(LoadListsAction action, IDispatcher dispatcher)
        {
            var lists = await _http.GetFromJsonAsync<TodoListDto[]>("/api/todolist");
            dispatcher.Dispatch(new LoadListsSuccessAction() { TodoLists = lists.ToImmutableList() });
        }
    }
}
