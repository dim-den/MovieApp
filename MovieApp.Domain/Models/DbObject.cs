using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Domain.Models
{
    public class DbObject
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
    }
}