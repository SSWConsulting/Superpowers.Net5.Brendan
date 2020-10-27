using System;
using System.Collections.Generic;

namespace Net5DomainModel
{
    public class TodoItem
    {
        public int Id { get; set; }

        public int ListId { get; set; }

        public string Title { get; set; }

        public string Note { get; set; }

        public PriorityLevel Priority { get; set; }

        public DateTime? Reminder { get; set; }

        public bool Done { get; set; }

        public TodoList List { get; set; }

        public IList<Tag> Tags { get; } = new List<Tag>();
    }
}