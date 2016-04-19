using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class NoteListItemViewModel
    {
        public int NoteId { get; set; }

        public string Title { get; set; }

        public string Importance { get; set; }

        [Display(Name = "Star")]
        [UIHint("Starred")]

        public bool IsStarred { get; set; }

        [Display(Name = "Created")]

        public DateTimeOffset CreatedUtc { get; set; }

        public override string ToString() => $"[{NoteId}] {Title}";
    }
}
