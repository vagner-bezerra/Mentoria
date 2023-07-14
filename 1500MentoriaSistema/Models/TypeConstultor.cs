using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _1500MentoriaSistema.Models
{
    public class TypeConsultor
    {
		[Key]
		public int Id { get; set; }
        [DisplayName("Tipo")]
        public string Name { get; set; }
        [DisplayName("Valor Tarifa")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public float Fee { get; set; }
    }
}
