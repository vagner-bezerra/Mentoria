using _1500MentoriaSistema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class Person : IdentityUser<int>
{
    [DisplayName("Tipo")]
    public TypePerson Type { get; set; }
    [DisplayName("Turma/Circulo")]
    public int? CircleId { get; set; }
    [DisplayName("Turma/Circulo")]
    public Circle Circle { get; set; }
    [DisplayName("Nome")]
    public string Name { get; set; }
    [DisplayName("CPF")]
    [DisplayFormat(DataFormatString = "{0:###.###.###-##}")]
    public string CPF { get; set; }
    [DisplayName("Número")]
    [DisplayFormat(DataFormatString = "{0:(###) ###-####}")]
    public string Phone { get; set; }
    [DisplayName("Data de Nascimento")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime BornDate { get; set; }
    [DisplayName("Nível de Estudo")]
    public NivelStudy NivelStudy { get; set; }
    [DisplayName("Universidade")]
    public string University { get; set; }
    [DisplayName("Data/Formado")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime GraduateDate { get; set; }
    [DisplayName("Data de Registro")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime RegisterDate { get; set; }
    [DisplayName("Empresa")]
    public string Enterprise { get; set; }
    [DisplayName("Recomendado")]
    public bool Recommendation { get; set; }
    [DisplayName("Estudando")]
    public bool IsStudying { get; set; }

    public virtual List<PersonLearning> PersonLearning { get; set; }
    public virtual List<ActualState> PlanResume { get; set; }
    public virtual List<PersonFeedback> PersonFeedbacks { get; set; }

}

public enum TypePerson
{
	Mentorado,
	Mentor
}

public enum NivelStudy
{
    EnsinoFundamentalIncompleto,
    EnsinoFundamentalCompleto,
	EnsinoMedioIncompleto,
	EnsinoMedioCompleto,
	EnsinoSuperiorIncompleto,
	EnsinoSuperiorCompleto
}