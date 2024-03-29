﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Trainer : BaseEntity
    {
        public string? FirstName { get; set; }   
        public string LastName { get; set; }    
        public string Sex { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Salary { get; set; }
        public string Notes { get; set; }  
        public ICollection<Member> Members { get; set; }


    }
}
