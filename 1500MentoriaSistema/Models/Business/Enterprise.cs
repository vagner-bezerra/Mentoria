using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _1500MentoriaSistema.Models
{
    public class Enterprise
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Nome")]
        public string Name { get; set; }
        [DisplayName("CNPJ")]
        public string CNPJ { get; set; }
        [DisplayName("Telefone")]
        public string Phone { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
    }
}
