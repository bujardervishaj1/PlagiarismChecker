using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlagarismChecker.Domain.Entities
{
    [Table("UserHistory")]
    public class UserHistory
    {
        [Key]
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Request { get; set; }

        public string Result { get; set; }

        public DateTime RequestDate { get; set; }
    }
}
