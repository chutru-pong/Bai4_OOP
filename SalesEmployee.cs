using System;

namespace ProjectManagement
{
    public class SalesEmployee : Employee
    {
        private double baseSalary;
        private double salesRevenue;
        private double commissionRate;

        public double BaseSalary
        {
            get { return baseSalary; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Lương cơ bản không âm");
                baseSalary = value;
            }
        }

        public double SalesRevenue
        {
            get { return salesRevenue; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Doanh số không âm");
                salesRevenue = value;
            }
        }

        public double CommissionRate
        {
            get { return commissionRate; }
            set
            {
                if (value < 0 || value > 0.3)
                    throw new ArgumentException("Tỷ lệ hoa hồng từ 0 đến 0.3");
                commissionRate = value;
            }
        }

        public SalesEmployee(string employeeId, string fullName, double baseSalary, double commissionRate)
            : this(employeeId, fullName, "Unassigned", baseSalary, 0, commissionRate)
        {
        }

        public SalesEmployee(string employeeId, string fullName, string department, double baseSalary, double salesRevenue, double commissionRate)
            : base(employeeId, fullName, department) //base gọi đến constructor lớp cha
        {
            if (baseSalary < 0)
                throw new ArgumentException("Lương cơ bản không âm");
            if (salesRevenue < 0)
                throw new ArgumentException("Doanh số không âm");
            if (commissionRate < 0 || commissionRate > 0.3)
                throw new ArgumentException("Tỷ lệ hoa hồng từ 0 đến 0.3");

            this.baseSalary = baseSalary;
            this.salesRevenue = salesRevenue;
            this.commissionRate = commissionRate;
        }

        public void setSalesRevenue(double revenue)
        {
            if (revenue < 0)
                throw new ArgumentException("Doanh số không âm");
            this.salesRevenue = revenue;
        }

        // Ghi đè
        public override double calculateGrossPay()
        {
            return baseSalary + (salesRevenue * commissionRate) + monthlyBonus;
        }

        public override string getEmployeeType()
        {
            return "Sales Employee";
        }

        public override void displayPayrollInfo()
        {
            double commissionAmount = salesRevenue * commissionRate;
            Console.WriteLine("[{0}] Mã: {1}, Tên: {2}, Phòng ban: {3}, Lương CB: {4:N0}, Doanh số: {5:N0}, Hoa hồng ({6:P0}): {7:N0}, Thưởng: {8:N0}, Tổng thu nhập: {9:N0}",
                getEmployeeType(), employeeId, fullName, department, baseSalary, salesRevenue, commissionRate, commissionAmount, monthlyBonus, calculateGrossPay());
        }

        ~SalesEmployee()
        {
        }
    }
}
