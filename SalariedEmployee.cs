using System;

namespace ProjectManagement
{
    public class SalariedEmployee : Employee
    {
        private double monthlySalary;
        private double responsibilityAllowance;

        public double MonthlySalary
        {
            get { return monthlySalary; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Lương cố định không âm");
                monthlySalary = value;
            }
        }

        public double ResponsibilityAllowance
        {
            get { return responsibilityAllowance; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Phụ cấp trách nhiệm không âm");
                responsibilityAllowance = value;
            }
        }

        public SalariedEmployee(string employeeId, string fullName, double monthlySalary, double responsibilityAllowance)
            : this(employeeId, fullName, "Unassigned", monthlySalary, responsibilityAllowance)
        {
        }

        public SalariedEmployee(string employeeId, string fullName, string department, double monthlySalary, double responsibilityAllowance)
            : base(employeeId, fullName, department) //base gọi đến constructor lớp cha
        {
            if (monthlySalary < 0)
                throw new ArgumentException("Lương cố định không âm");
            if (responsibilityAllowance < 0)
                throw new ArgumentException("Phụ cấp trách nhiệm không âm");

            this.monthlySalary = monthlySalary;
            this.responsibilityAllowance = responsibilityAllowance;
        }

        public override double calculateGrossPay()
        {
            return monthlySalary + responsibilityAllowance + monthlyBonus;
        }

        public override string getEmployeeType()
        {
            return "Salaried Employee";
        }

        public override void displayPayrollInfo()
        {
            Console.WriteLine("[{0}] Mã: {1}, Tên: {2}, Phòng ban: {3}, Lương tháng: {4:N0}, Phụ cấp: {5:N0}, Thưởng: {6:N0}, Tổng thu nhập: {7:N0}",
                getEmployeeType(), employeeId, fullName, department, monthlySalary, responsibilityAllowance, monthlyBonus, calculateGrossPay());
        }

        ~SalariedEmployee()
        {
        }
    }
}
