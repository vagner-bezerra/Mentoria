using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace _1500MentoriaSistema.Models
{
    public class BankAccount
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Banco")]
        public string Name { get; set; }
        [DisplayName("Empresa")]
        public int? EnterpriseId { get; set; }
        [DisplayName("Empresa")]
        public Enterprise Enterprise { get; set; }

        [DisplayName("Saldo Atual")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public float Balance { get; set; }
        [DisplayName("Saldo Inicial")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public float InitialBalance { get; set; }

        public List<Expense> Expanses { get; set; }

        public void AttCalculos()
        {
            this.Balance = 
                this.InitialBalance + 
                this.Expanses.Where(x => x.Type).Sum(x => x.Value) -
                this.Expanses.Where(x => !x.Type).Sum(x => x.Value);
        }
    }
}
