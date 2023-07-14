using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1500MentoriaSistema.Models
{
    public class PersonFeedback
    {
		[Key]
		public int Id { get; set; }
        [DisplayName("Tipo")]
        public TypePerson TypePerson { get; set; }
        [DisplayName("Pessoa")]
        public int PersonId { get; set; }
        [DisplayName("Pessoa")]
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
        [DisplayName("Feedback")]
        public int FeedbackId { get; set; }
        [DisplayName("Feedback")]
        public  Feedback Feedback { get; set; }
    }
}
