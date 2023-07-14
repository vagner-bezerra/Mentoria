using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _1500MentoriaSistema.Models
{
    public class Captures
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Conta")]
        public string Name { get; set; }
    }
}
