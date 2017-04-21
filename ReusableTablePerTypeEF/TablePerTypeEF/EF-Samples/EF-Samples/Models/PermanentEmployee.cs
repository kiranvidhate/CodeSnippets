using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Samples
{
    [Table("PermanentEmployees")]
    public class PermanentEmployee : Employee
    {
        /// <summary>
        /// Gets or sets the annual salary.
        /// </summary>
        /// <value>
        /// The annual salary.
        /// </value>
        public int AnnualSalary { get; set; }
    }
}