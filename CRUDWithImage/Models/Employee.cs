using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRUDWithImage.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public HttpPostedFileBase File { get; set; }
    }
}