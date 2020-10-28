using Fluxor;
using Superpowers.Net5.Blazor.Client.Pages;
using Superpowers.Net5.Models.Todo;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Superpowers.Net5.Blazor.Client.Store.Todo
{
    public record LoadListsSuccessAction
    {
        public IImmutableList<TodoListDto> TodoLists { get; init; }
    }


    public static partial class Reducers
    {
        [ReducerMethod]
        public static TodoState ReduceLoadListsSuccess(TodoState state, LoadListsSuccessAction action)
        {
            TodoListDto newSelectedList = null;
            // selected attempt to retrive previously selected item
            if (state.SelectedList != null) newSelectedList = action.TodoLists.FirstOrDefault(l => l.Id == state.SelectedList.Id);
            // fallback to first
            if (newSelectedList == null) newSelectedList = action.TodoLists.FirstOrDefault();

            return new TodoState
            {
                SelectedList = newSelectedList,
                TodoLists = action.TodoLists
            };

        }
    }

}
