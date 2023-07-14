using _1500MentoriaSistema.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;

public enum TypeObject
{
    GESTAO,
    ETL,//Extração de dados
    DASH,
    BBP,//Bussiness blueprint
    AULA,
    PREPAULA,//Preparação de aula
    DEV,
    MANUT,//Manutenção
    INFRA,
    SUP,//Suporte
    None
}

public enum Delivered
{
    VAZIO,
    [Display(Name = "Não")]
    NAO,
    [Display(Name = "Entregue")]
    ENTREGUE,
    [Display(Name = "Esperando Aprovação")]
    EMANDAMENTO
}

public class ActualState
{
    [Key]
    public int Id { get; set; }

    [DisplayName("Círculo")]
    [Required(ErrorMessage = "É necessario indentificar de qual círculo/turma o registro pertence")]
    public int CircleId { get; set; }
    [DisplayName("Círculo")]
    public Circle Circle { get; set; }

    [DisplayName("Projeto")]
    [Required(ErrorMessage = "É necessario indentificar o projeto que está sendo trabalhado")]
    public int ProjectId { get; set; }
    [DisplayName("Projeto")]
    public Project Project { get; set; }

    [DisplayName("Tipo de Atuação")]
    [Required(ErrorMessage = "É necessario indentificar o tipo de ação que foi executado")]
    public TypeObject TypeObject { get; set; }

    [DisplayName("Tarifa")]
    [Required(ErrorMessage = "É necessario indentificar qual tarifa foi utilizada")]
    public int TypeConsultorId { get; set; }
    [DisplayName("Tarifa")]
    public TypeConsultor TypeConsultor { get; set; }

    [DisplayName("Descrição")]
    public string Description { get; set; }
    [DisplayName("Tempo Planejado")]
    [Required(ErrorMessage = "É necessario indentificar quanto tempo foi alocado para esta tarefa")]
    public float TimePlanned { get; set; }
    [DisplayName("Valor")]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    public float Value { get; set; }

    [DisplayName("Consultor")]
    [Required(ErrorMessage = "É necessario indentificar qual o consultor responsável")]
    public int PersonId { get; set; }

    [DisplayName("Consultor")]
    [ForeignKey("PersonId")]
    public Person Person { get; set; }

    [DisplayName("Tempo Utilizado")]
    public float RealTime { get; set; }
    [DisplayName("Entregue")]
    public Delivered Delivered { get; set; }
    [DisplayName("Produtividade")]
    [DisplayFormat(DataFormatString = "{0:N}")]
    public float Productivity { get; set; }
    [DisplayName("Sprint")]
    [Required(ErrorMessage = "É necessario indentificar qual será a data de entrega")]
    [DisplayFormat(DataFormatString = "{0:dd/MM}")]
    public DateTime Sprint { get; set; }
    [DisplayName("Valor Final")]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    public float FinalValue { get; set; }

    [DisplayName("Inserido por")]
    public int UserId { get; set; }
    [DisplayName("Horas Totais")]
    public List<HoursDay> HoursDay { get; set; }

    public void AttCalculos(bool checkDelivered)
    {
        if (this.HoursDay != null && this.HoursDay.Count > 0)
        {
            this.RealTime = this.HoursDay.Sum(x => x.Hours);

            if (checkDelivered)
                this.Delivered = this.HoursDay.FirstOrDefault(x => x.Delivered == true) != null ? Delivered.ENTREGUE : Delivered.NAO;
        }

        this.Value = this.TypeConsultor.Fee * this.TimePlanned;

        if (this.Delivered == Delivered.ENTREGUE)
        {
            this.Productivity = (this.TimePlanned / this.RealTime) * 100; //Produtividade
            if (double.IsInfinity(this.Productivity))
                this.Productivity = 0;

            this.FinalValue = this.RealTime * this.TypeConsultor.Fee;
        }
    }
}