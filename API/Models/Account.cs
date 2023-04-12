using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_accounts")]
    public class Account
    {
        [Key, Column("employee_nik", TypeName = "char(5)")]
        public string EmployeeNik { get; set; }
        [Column("password", TypeName = "varchar(255)")]
        public string Password { get; set; }

        //Cardinality
        public Employee Employee { get; set; }
        public ICollection<AccountRole> AccountRoles { get; set; }
    }
}
