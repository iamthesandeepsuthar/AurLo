using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Shared.ContractModel
{
    public class UserRoleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public bool? IsActive { get; set; }
        public string ParentRole { get; set; }

        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
    }

    public class UserRolePostModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is Require...!")]
        [DisplayName("Name")]
        [StringLength(maximumLength: 50, ErrorMessage = "{0} cannot be less then {2} and longer than {1} characters.", MinimumLength = 4)]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} is Require...!")]
        [DisplayName("Parent Role")]
        public int? ParentId { get; set; }

        [Required(ErrorMessage = "{0} is Require...!")]
        [DisplayName("Active Status")]
        public bool IsActive { get; set; }

    }

    public class StudentDTO
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }

    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }
}
