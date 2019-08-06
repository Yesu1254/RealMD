using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealMD.Models
{
    public class AddressBook
    {
        [Required]
        [Display(Name = "FirstName")]
        public string firstName { get; set; }
        [Display(Name = "LastName")]
        public string lastName { get; set; }
        [Display(Name = "MobileNo")]
        public string mobileNo { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "DOB")]
        public DateTime? dateofBirth { get; set; }
        public int id { get; set; }
        public IEnumerable<AddressBook> AddressBooks { get; set; }
    }
}