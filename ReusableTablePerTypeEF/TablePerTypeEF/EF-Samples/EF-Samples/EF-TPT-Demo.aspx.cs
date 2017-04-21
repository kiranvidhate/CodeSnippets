using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace EF_Samples
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class EF_TPT_Demo : System.Web.UI.Page
    {
        /// <summary>
        /// The employee database context
        /// </summary>
        EmployeeDBContext employeeDBContext = new EmployeeDBContext();

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataSource = ConvertEmployeesForDisplay(employeeDBContext.Employees.ToList());
            GridView1.DataBind();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the RadioButtonList1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (RadioButtonList1.SelectedValue)
            {
                case "Permanent":
                    GridView1.DataSource = ConvertEmployeesForDisplay(employeeDBContext.Employees.OfType<PermanentEmployee>().ToList<Employee>());
                    GridView1.DataBind();
                    break;

                case "Contract":
                    GridView1.DataSource = ConvertEmployeesForDisplay(employeeDBContext.Employees.OfType<ContractEmployee>().ToList<Employee>());
                    GridView1.DataBind();
                    break;

                default:
                    GridView1.DataSource = ConvertEmployeesForDisplay(employeeDBContext.Employees.ToList());
                    GridView1.DataBind();
                    break;
            }
        }

        /// <summary>
        /// Converts the employees for display.
        /// </summary>
        /// <param name="employees">The employees.</param>
        /// <returns></returns>
        private DataTable ConvertEmployeesForDisplay(List<Employee> employees)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("FirstName");
            dt.Columns.Add("LastName");
            dt.Columns.Add("Gender");
            dt.Columns.Add("AnuualSalary");
            dt.Columns.Add("HourlyPay");
            dt.Columns.Add("HoursWorked");
            dt.Columns.Add("Type");

            foreach (Employee employee in employees)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = employee.EmployeeID;
                dr["FirstName"] = employee.FirstName;
                dr["LastName"] = employee.LastName;
                dr["Gender"] = employee.Gender;

                if (employee is PermanentEmployee)
                {
                    dr["AnuualSalary"] = ((PermanentEmployee)employee).AnnualSalary;
                    dr["Type"] = "Permanent";
                }
                else
                {
                    dr["HourlyPay"] = ((ContractEmployee)employee).HourlyPay;
                    dr["HoursWorked"] = ((ContractEmployee)employee).HoursWorked;
                    dr["Type"] = "Contract";
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// Handles the Click event of the btnAddPermanentEmployee control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnAddPermanentEmployee_Click(object sender, EventArgs e)
        {
            PermanentEmployee permanentEmployee = new PermanentEmployee
            {
                FirstName = "Mike",
                LastName = "Brown",
                Gender = "Male",
                AnnualSalary = 70000,
            };

            employeeDBContext.Employees.Add(permanentEmployee);
            employeeDBContext.SaveChanges();

            GridView1.DataSource = ConvertEmployeesForDisplay(employeeDBContext.Employees.ToList());
            GridView1.DataBind();
        }

        /// <summary>
        /// Handles the Click event of the btnAddContractEmployee control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnAddContractEmployee_Click(object sender, EventArgs e)
        {
            ContractEmployee contractEmployee = new ContractEmployee
            {
                FirstName = "Stacy",
                LastName = "Josh",
                Gender = "Female",
                HourlyPay = 50,
                HoursWorked = 120
            };

            employeeDBContext.Employees.Add(contractEmployee);
            employeeDBContext.SaveChanges();

            GridView1.DataSource = ConvertEmployeesForDisplay(employeeDBContext.Employees.ToList());
            GridView1.DataBind();
        }
    }
}