using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1500MentoriaSistema.Models
{
    [NotMapped]
    public class AssignRoleViewModel
    {
        public List<Person> Users { get; set; }
        public List<IdentityRole<int>> Roles { get; set; }
    }
}
