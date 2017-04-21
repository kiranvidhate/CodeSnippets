using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Samples
{
    [Table("ContractEmployees")]
    public class ContractEmployee : Employee
    {
        /// <summary>
        /// Gets or sets the hours worked.
        /// </summary>
        /// <value>
        /// The hours worked.
        /// </value>
        public int HoursWorked { get; set; }

        /// <summary>
        /// Gets or sets the hourly pay.
        /// </summary>
        /// <value>
        /// The hourly pay.
        /// </value>
        public int HourlyPay { get; set; }
    }
}