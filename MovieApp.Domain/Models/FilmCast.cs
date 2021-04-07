using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Domain.Models
{
    public enum RoleType
    {
        Main,
        Minor
    }
    public class FilmCast : DbObject
    {
        public RoleType RoleType { get; set; }
        public string RoleName { get; set; }
        public Actor Actor { get; set; }
        public Film Film { get; set; }
    }
}
