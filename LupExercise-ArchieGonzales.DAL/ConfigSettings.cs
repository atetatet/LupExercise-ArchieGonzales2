using System;
using System.Collections.Generic;
using System.Text;

namespace LupExercise_ArchieGonzales.DAL
{
    public class ConfigSettings
    {
        public static string AdminRole = "Admin";
        public static string UserRole = "User";
        public static readonly string[] Roles = { AdminRole, UserRole };
    }
}
