using Fluxor;
using Superpowers.Net5.Models.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superpowers.Net5.Blazor.Client.Store.Todo
{
    public record SelectListAction
    {
        public TodoListDto SelectedList { get; init; }
    }


    public static partial class Reducers {

        [ReducerMethod]
        public static TodoState ReductSelectList(TodoState state, SelectListAction action)
        {
            return state with
            {
                SelectedList = action.SelectedList
            };

        }
    }
}
