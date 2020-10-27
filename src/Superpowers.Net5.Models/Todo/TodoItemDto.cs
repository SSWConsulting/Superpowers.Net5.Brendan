using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superpowers.Net5.Models.Todo
{
    public record TodoItemDto
    {
        public int Id { get; init; }

        public int ListId { get; init; }

        public string Title { get; init; }

        public bool Done { get; init; }

        public PriorityLevelDto Priority { get; init; }

        public string Note { get; init; }
    }
}
