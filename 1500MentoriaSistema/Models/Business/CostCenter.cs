using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _1500MentoriaSistema.Models
{
    public class CostCenter
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Centro de Custo")]
        public string Name { get; set; }
    }
}
