using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LupExercise_ArchieGonzales.DAL.Entities
{
    public class Users : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
