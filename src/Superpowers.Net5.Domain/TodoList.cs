using System.Collections.Generic;

namespace Net5DomainModel
{
    public class TodoList
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Colour { get; set; }

        public IList<TodoItem> Items { get; } = new List<TodoItem>();
    }
}
