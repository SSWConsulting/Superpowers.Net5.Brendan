using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superpowers.Net5.Models.Commands
{
    public record CreateTodoListItem : IRequest<int>
    {
        public int ListId { get; init; }

        public string Title { get; init; }

    }
}
