using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superpowers.Net5.Models.Todo
{
    public record PriorityLevelDto
    {
        public int Value { get; init; }

        public string Name { get; init; }
    }
}
