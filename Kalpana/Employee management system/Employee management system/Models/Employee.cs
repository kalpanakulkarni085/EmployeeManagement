using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_management_system.Models
{
    [Table("Employees")]
   
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        public string Name { get; set; }

        [Required]
        public string EmployeeCode { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }

        // Navigation property for Designation
        [ForeignKey("DesignationId")]
        public Designation Designation { get; set; }

        public int DesignationId { get; set; }

        // Navigation property for Department
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        public int DepartmentId { get; set; }

        public string Address { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }

    [Table("Departments")]
    
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }

    //[Table("Designation")]
    [Table("Designations")]
    public class Designation
    {
        [Key]
        
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
    public class EmployeeViewModel
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmployeeCode { get; set; }
        public string Designation  { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }

        // Navigation property for Designation
        [ForeignKey("DesignationId")]
        public int DesignationId { get; set; }

        [ForeignKey("DepartmentId")]

        public int DepartmentId { get; set; }

        public string Address { get; set; }
    }
}
