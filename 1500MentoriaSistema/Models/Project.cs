using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class Project
{
	[Key]
	public int Id { get; set; }
	[DisplayName("Projeto")]
    [Required(ErrorMessage = "Um nome deve ser atribuido para o projeto.")]
	public string Name { get; set; }
    [DisplayName("Tipo de Contrato")]
    [Required(ErrorMessage = "Um tipo de contrato deve ser atribuido para o projeto.")]
    public TypeTime Type { get; set; }
    [DisplayName("Descrição")]
    public string Description { get; set; }
    [DisplayName("Empresa")]
    public string Enterprise { get; set; }
    [DisplayName("Horas Contratadas")]
    [Required(ErrorMessage = "As horas contratadas devem ser atribuidas no projeto.")]
    public float Duration { get; set; }
    [DisplayName("Valor")]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    [Required(ErrorMessage = "O valor do projeto deve ser especificado.")]
    public float Value { get; set; }

    [DisplayName("Data Inicio")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime StartDate { get; set; }
    [DisplayName("Data Entrega")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime EndDate { get; set; }
    [DisplayName("Situação")]
    public bool Finished { get; set; }
    [DisplayName("Inserido por")]
    public int UserId { get; set; }
    [DisplayName("Data Inserção")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime InsertDate { get; set; }
    [DisplayName("Status")]
    public bool Status { get; set; }
    public List<ActualState> PlanResumes { get; set; }
}

//Se for 'PrecoFechado' entra horas e valor
//Tarifa = Value / Duration;

//Se for por 'Horas' entra qual tarífa
//Tarifa * Horas;

public enum TypeTime
{
    Horas,
    PrecoFechado
}