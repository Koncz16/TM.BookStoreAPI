﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.DTOs
{
    public class BookDTO
    {

        public string Title { get; set; } = string.Empty;

        public string AuthorId { get; set; } = string.Empty;

        public string PublisherId { get; set; } = string.Empty;

        public DateTime YearOfPublication { get; set; } = new DateTime();

        public List<string> Genres { get; set; } = new List<string>();
    }
}
