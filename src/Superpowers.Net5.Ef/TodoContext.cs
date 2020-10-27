using Microsoft.EntityFrameworkCore;
using Net5DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Superpowers.Net5.Ef
{
    public class TodoContext: DbContext
    {
        public DbSet<TodoList> TodoLists { get; set; }

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<Tag> Tags { get; set; }

    }
}
