﻿namespace FeedBackAdmin.Models
{
    public class User
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Password { get; set; }

        public string? Salt { get; set; }
        public User()
        {
            this.Messages = new HashSet<Message>();
        }
        public ICollection<Message>? Messages { get; set; }

    }
}