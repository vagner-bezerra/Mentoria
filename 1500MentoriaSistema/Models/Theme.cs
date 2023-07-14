using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _1500MentoriaSistema.Models
{
    public class Theme
    {

		[Key]
		public int Id { get; set; }
        [DisplayName("Tema")]
        [StringLength(10, ErrorMessage = "O valor {0} não pode ultrapassar {1} caracteres")]
        public string Name { get; set; }
        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Inserido por")]
        public int UserId { get; set; }
        [DisplayName("Aprendizados")]
        public List<Learning> Learning { get; set; }
    }
}
