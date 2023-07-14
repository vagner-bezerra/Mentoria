using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1500MentoriaSistema.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Tipo")]
        public bool Type { get; set; }
        [DisplayName("Data de Realização")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime RegisterDate { get; set; }
        [DisplayName("Data do Caixa")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CashDate { get; set; }
        [DisplayName("Mês Caixa")]
        [DisplayFormat(DataFormatString = "{0:MM/yyyy}")]
        public DateTime MonthDate { get; set; }
        [DisplayName("Data Competência")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CompeteDate { get; set; }
        [DisplayName("Mês Competência")]
        [DisplayFormat(DataFormatString = "{0:MM/yyyy}")]
        public DateTime CompeteMonthDate { get; set; }
        [DisplayName("Centro de Custo")]
        public int? CostCenterId { get; set; }
        [DisplayName("Centro de Custo")]
        public CostCenter CostCenter { get; set; }
        [DisplayName("Conta")]
        public int? CaptureId { get; set; }
        [DisplayName("Conta")]
        public Captures Capture { get; set; }
        [DisplayName("Pessoa")]
        public int? PersonId { get; set; }
        [DisplayName("Pessoa")]
        public Person Person { get; set; }
        [DisplayName("Banco")]
        public int? BankAccountId { get; set; }
        [DisplayName("Banco")]
        public BankAccount BankAccount { get; set; }
        [DisplayName("Categoria")]
        public string TargetBill { get; set; }
        [DisplayName("Descrição")]
        public string Description { get; set; }
        [DisplayName("Valor")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public float Value { get; set; }
        [DisplayName("Contabilizado")]
        public bool Validated { get; set; }
        [DisplayName("Planejamento")]
        public bool Plan { get; set; }

        [DisplayName("Saldo em Banco")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public float BankValueMoment { get; set; }
        [DisplayName("Saldo da Empresa")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public float EnterpriseValueMoment { get; set; }
    }
}
