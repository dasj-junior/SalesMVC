using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesMVC.Models
{
    public class Seller
    {
        public int ID { get; set; }
        
        [Required(ErrorMessage = "{0} required")]
        [StringLength(60,MinimumLength = 3,ErrorMessage = "{0} size should be between {2} and {1} characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Birth Date")]
        [Required(ErrorMessage = "{0} required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        [Display(Name = "Base Salary")]
        [Range(100.0,50000.0,ErrorMessage = "{0} must be between {1} and {2}")]
        [Required(ErrorMessage = "{0} required")]
        public double BaseSalary { get; set; }

        public Department Department { get; set; }
        public int DepartmentID { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int Id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            ID = Id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales (DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }

    }
}
