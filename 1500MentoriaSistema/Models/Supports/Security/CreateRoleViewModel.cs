using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1500MentoriaSistema.Models
{
    [NotMapped]
    public class CreateRoleViewModel
    {
        [DisplayName("Cargo")]
        public string RoleName { get; set; }
    }
}