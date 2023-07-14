using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1500MentoriaSistema.Models
{
	public enum Status
    {
		BRANCO,
		CONCLUIDO
    }
    public class Learning
    {
		[Key]
		public int Id { get; set; }

        [DisplayName("Turma/Circulo")]
        public int CircleId { get; set; }
        [DisplayName("Turma/Circulo")]
        public Circle Circle { get; set; }

        [DisplayName("Tema")]
        public int ThemeId { get; set; }
        [DisplayName("Tema")]
        public Theme Theme { get; set; }

        [DisplayName("Oportunidade de Aprendizado")]
        public string OportunityLearning { get; set; } //Campo texto - string
        [DisplayName("Ação de Aprendizado")]
        public string LearningAction { get; set; } //Campo texto - string
        [DisplayName("Data Mensuração")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime MeasurementDate { get; set; } //Data
        [DisplayName("Nota")]
        public float MeasurementForm { get; set; }
        [DisplayName("Resultado")]
        public string Result { get; set; }
        [DisplayName("Comentários")]
        public string Comment { get; set; }
        [DisplayName("Situação")]
        public Status Status { get; set; }
        [DisplayName("Inserido por")]
        public int UserId { get; set; }

        public List<PersonLearning> PersonLearning { get; set; }

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