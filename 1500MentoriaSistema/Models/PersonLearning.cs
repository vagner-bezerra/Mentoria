using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1500MentoriaSistema.Models
{
    public class PersonLearning
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
        [DisplayName("Aprendizado")]
        public int LearningId { get; set; }
        [DisplayName("Feedback")]
        public Learning Learning { get; set; }
    }
}
