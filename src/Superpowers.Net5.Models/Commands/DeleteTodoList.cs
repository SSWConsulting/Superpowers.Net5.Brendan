using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superpowers.Net5.Models.Commands
{
    public record DeleteTodoList: IRequest<Unit>
    {
        public int Id { get; init; }
    }
}
