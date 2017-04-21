using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace EF_Samples
{
    public partial class TPH_Demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmployeeDBContext employeeDBContext = new EmployeeDBContext();

            switch (RadioButtonList1.SelectedValue)
            {
                case "Permanent":
                    GridView1.DataSource = employeeDBContext.Employees.OfType<PermanentEmployee>().ToList();
                    GridView1.DataBind();
                    break;

                case "Contract":
                    GridView1.DataSource = employeeDBContext.Employees
                        .OfType<ContractEmployee>().ToList();
                    GridView1.DataBind();
                    break;

                default:
                    GridView1.DataSource = ConvertEmployeesForDisplay(
                        employeeDBContext.Employees.ToList());
                    GridView1.DataBind();
                    break;
            }
        }

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
                dr["ID"] = employee.ID;
                dr["FirstName"] = employee.FirstName;
                dr["LastName"] = employee.LastName;
                dr["Gender"] = employee.Gender;

                if (employee is PermanentEmployee)
                {
                    dr["AnuualSalary"] = ((PermanentEmployee)employee).AnuualSalary;
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

        protected void btnAddPermanentEmployee_Click(object sender, EventArgs e)
        {
            PermanentEmployee permanentEmployee = new PermanentEmployee
            {
                FirstName = "Mike",
                LastName = "Brown",
                Gender = "Male",
                AnuualSalary = 70000,
            };

            EmployeeDBContext employeeDBContext = new EmployeeDBContext();
            employeeDBContext.Employees.Add(permanentEmployee);
            employeeDBContext.SaveChanges();
        }

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

            EmployeeDBContext employeeDBContext = new EmployeeDBContext();
            employeeDBContext.Employees.Add(contractEmployee);
            employeeDBContext.SaveChanges();
        }
    }
}