using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superpowers.Net5.Models.Commands
{
    public record ItemDone : IRequest<Unit>
    {
        public int Id { get; init; }
        public bool IsDone { get; init; }
    }
}
