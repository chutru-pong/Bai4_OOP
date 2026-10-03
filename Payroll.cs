using System;
using System.Collections.Generic;

namespace ProjectManagement
{
    public class Payroll
    {
        private string period;
        private List<Employee> employees;

        public string Period
        {
            get { return period; }
            set
            {
                if (value == null || value == "")
                    throw new ArgumentException("Kỳ lương không rỗng");
                period = value;
            }
        }

        public List<Employee> Employees
        {
            get { return employees; }
        }

        public Payroll(string period)
        {
            if (period == null || period == "")
                throw new ArgumentException("Kỳ lương không rỗng");

            this.period = period;
            this.employees = new List<Employee>();
        }

        public bool addEmployee(Employee employee)
        {
            if (employee == null)
                return false;

            if (findEmployee(employee.EmployeeId) != null)
            {
                return false;
            }

            employees.Add(employee);
            return true;
        }

        public Employee findEmployee(string employeeId)
        {
            if (employeeId == null || employeeId == "")
                return null;

            foreach (Employee emp in employees)
            {
                if (string.Equals(emp.EmployeeId, employeeId))
                    return emp;
            }

            return null;
        }

        public double calculateTotalPayroll()
        {
            double total = 0;
            foreach (Employee emp in employees)
            {
                total += emp.calculateGrossPay();
            }
            return total;
        }


        public double calculatePayrollByDepartment(string department)
        {
            if (department == null || department == "")
                return 0;

            double totalDept = 0;
            foreach (Employee emp in employees)
            {
                if (string.Equals(emp.Department, department))
                {
                    totalDept += emp.calculateGrossPay();
                }
            }
            return totalDept;
        }

        public Employee findHighestPaidEmployee()
        {
            if (employees.Count == 0)
                return null;

            Employee highest = employees[0];
            double maxGrossPay = highest.calculateGrossPay();

            for (int i = 1; i < employees.Count; i++)
            {
                double currentPay = employees[i].calculateGrossPay();
                if (currentPay > maxGrossPay)
                {
                    maxGrossPay = currentPay;
                    highest = employees[i];
                }
            }

            return highest;
        }

        public void displayPayroll()
        {
            Console.WriteLine("BẢNG LƯƠNG KỲ: {0}", period);
        

            if (employees.Count == 0)
            {
                Console.WriteLine("Danh sách nhân sự trống.");
                return;
            }

            int index = 1;
            foreach (Employee emp in employees)
            {
                Console.Write("{0,2}. ", index++);
                emp.displayPayrollInfo();
            }

            Console.WriteLine("Tổng số nhân sự : {0}", employees.Count);
            Console.WriteLine("TỔNG CHI TRẢ LƯƠNG: {0:N0} VNĐ", calculateTotalPayroll());

        }

        ~Payroll()
        {
            employees.Clear();
        }
    }
}