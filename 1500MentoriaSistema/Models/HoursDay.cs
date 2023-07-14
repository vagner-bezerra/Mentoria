using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _1500MentoriaSistema.Models
{
    public class HoursDay
    {
		[Key]
		public int Id { get; set; }
        [DisplayName("Atividade")]
        public int ActualstateId { get; set; }
        [DisplayName("Atividade")]
        public ActualState ActualState { get; set; }
        [DisplayName("Data Realizada")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM}")]
        public DateTime InsertDate { get; set; }
        [DisplayName("Horas Utilizadas")]
        public float Hours { get; set; }
        [DisplayName("Entregue")]
        public bool Delivered { get; set; }

        [DisplayName("Inserido por")]
        public int PersonId { get; set; }
        [DisplayName("Inserido por")]
        public Person Person { get; set; }
    }
}
