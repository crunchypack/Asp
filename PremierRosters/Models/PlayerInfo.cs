using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PremierRosters.Models
{
    public class PlayerInfo
    {
        public PlayerInfo()
        {
        }

        [StringLength(20,MinimumLength = 2, ErrorMessage = "Must be between 2 and 20 characters. Field is Required")]
        [Required(ErrorMessage = "Must be between 2 an 20 characters. Field is Required")]
        public string FirstName { set; get; }

        [StringLength(20, MinimumLength = 2, ErrorMessage = "Must be between 2 and 20 characters. Field is Required")]
        [Required(ErrorMessage = "Must be between 2 an 20 characters. Field is Required")]
        public string Surname { set; get; }

        [Range(1,99)]
        public int Jersey { set; get; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Letters only. Field is Required")]
        [Required(ErrorMessage ="Letters only. Field is Required")]
        public string Position { set; get; }

        [Range(1970,2005)]
        public int BirthYear { set; get; }

        public int Team { set; get; }

        public string  TeamString { set; get; }

        public int ID { set; get; }
    }
}
