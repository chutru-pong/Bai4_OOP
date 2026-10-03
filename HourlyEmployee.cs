using System;

namespace ProjectManagement
{
    public class HourlyEmployee : Employee
    {
        private double hourlyRate;
        private double workedHours;

        public double HourlyRate
        {
            get { return hourlyRate; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Đơn giá giờ là dương");
                hourlyRate = value;
            }
        }

        public double WorkedHours
        {
            get { return workedHours; }
            set
            {
                if (value < 0 || value > 250)
                    throw new ArgumentException("Số giờ làm từ 0 đến 250");
                workedHours = value;
            }
        }

        public HourlyEmployee(string employeeId, string fullName, double hourlyRate)
            : this(employeeId, fullName, "Unassigned", hourlyRate, 0) 
        {
        }

        public HourlyEmployee(string employeeId, string fullName, string department, double hourlyRate, double workedHours)
            : base(employeeId, fullName, department)
        {
            if (hourlyRate <= 0)
                throw new ArgumentException("Đơn giá giờ là dương");
            if (workedHours < 0 || workedHours > 250)
                throw new ArgumentException("Số giờ làm từ 0 đến 250");

            this.hourlyRate = hourlyRate;
            this.workedHours = workedHours;
        }

        public override double calculateGrossPay()
        {
            double basePay;
            if (workedHours <= 160)
            {
                basePay = workedHours * hourlyRate;
            }
            else
            {
                basePay = 160 * hourlyRate + (workedHours - 160) * hourlyRate * 1.5;
            }
            return basePay + monthlyBonus;
        }

        public override string getEmployeeType()
        {
            return "Hourly Employee";
        }

        public override void displayPayrollInfo()
        {
            double overtimeHours = workedHours > 160 ? workedHours - 160 : 0;
            Console.WriteLine("[{0}] Mã: {1}, Tên: {2}, Phòng ban: {3}, Đơn giá/h: {4:N0}, Giờ làm: {5}h (Tăng ca: {6}h), Thưởng: {7:N0}, Tổng thu nhập: {8:N0}",
                getEmployeeType(), employeeId, fullName, department, hourlyRate, workedHours, overtimeHours, monthlyBonus, calculateGrossPay());
        }

        ~HourlyEmployee()
        {
        }
    }
}
