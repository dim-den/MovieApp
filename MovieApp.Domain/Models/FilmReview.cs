using System;

namespace MovieApp.Domain.Models
{
    public class FilmReview : DbObject
    {
        public string ReviewText { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }
        public Film Film { get; set; }
        public User User { get; set; }
        public int? FilmID { get; set; }
        public int? UserID { get; set; }
    }
}