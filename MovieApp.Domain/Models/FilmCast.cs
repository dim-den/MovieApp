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
        public int? ActorID { get; set; }
        public int? FilmID { get; set; }
    }
}