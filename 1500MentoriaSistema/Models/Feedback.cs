using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1500MentoriaSistema.Models
{
    public class Feedback
    {
		[Key]
		public int Id { get; set; }
		[DisplayName("Data")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayName("Turma/Circulo")]
        public int CircleId { get; set; }
        [DisplayName("Turma/Circulo")]
        public Circle Circle { get; set; }

        [DisplayName("Tema")]
        public int ThemeId { get; set; }
        [DisplayName("Tema")]
        public Theme Theme { get; set; }

        [DisplayName("Oportunidade de Aprendizagem")]
        public string OportunityLearning { get; set; }
        [DisplayName("Nota")]
        public float Note { get; set; }
        [DisplayName("Comentário")]
        public string Comment { get; set; }
        [DisplayName("Inserido por")]
        public int UserId { get; set; }

        public List<PersonFeedback> PersonFeedback { get; set; }

        [NotMapped]
        [DisplayName("Professor")]
        public int TeacherPersonId { get; set; }
        [NotMapped]
        [DisplayName("Aluno")]
        public int StudentPersonId { get; set; }
        [NotMapped]
        public Person TeacherPerson { get; set; }
        [NotMapped]
        public Person StudentPerson { get; set; }
    }
}
