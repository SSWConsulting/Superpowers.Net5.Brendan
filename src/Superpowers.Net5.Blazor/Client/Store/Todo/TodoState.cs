using Superpowers.Net5.Models.Todo;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Superpowers.Net5.Blazor.Client.Store.Todo
{
    public record TodoState
    {

        public IImmutableList<TodoListDto> TodoLists { get; init; } = ImmutableList<TodoListDto>.Empty;

        public TodoListDto SelectedList { get; init; }

    }
}
