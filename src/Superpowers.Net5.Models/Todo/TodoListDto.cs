using System;
using System.Collections.Generic;
using System.Text;

namespace Superpowers.Net5.Models.Todo
{
    public record TodoListDto
    {

        public int Id { get; init; }

        public string Title { get; init; }

        public IEnumerable<TodoItemDto> Items { get; init; }

    }
}
