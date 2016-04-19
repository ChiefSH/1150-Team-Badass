﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class NoteDetailViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Importance { get; set; }
        public bool IsStarred { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
        public int NoteId { get; set; }

    }
}
