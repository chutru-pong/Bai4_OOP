using System;

namespace ProjectManagement
{
    public abstract class Employee
    {
        //Thuộc tính
        protected string employeeId;
        protected string fullName;
        protected string department;
        protected double monthlyBonus;

        public string EmployeeId
        {
            get { return employeeId; }
        }

        public string FullName
        {
            get { return fullName; }
            set
            {
                if (value == null || value == "")
                    throw new ArgumentException("Họ tên không rỗng");
                fullName = value;
            }
        }

        public string Department
        {
            get { return department; }
            set
            {
                if (value == null || value == "")
                    throw new ArgumentException("Phòng ban không rỗng");
                department = value;
            }
        }

        public double MonthlyBonus
        {
            get { return monthlyBonus; }
        }

        // Constructor
        public Employee(string employeeId, string fullName) 
            : this(employeeId, fullName, "Unassigned", 0) 
        {
        }

        public Employee(string employeeId, string fullName, string department) 
            : this(employeeId, fullName, department, 0)
        {
        }

        public Employee(string employeeId, string fullName, string department, double monthlyBonus)
        {
            if (employeeId == null || employeeId == "")
                throw new ArgumentException("Mã không rỗng");
            if (fullName == null || fullName == "")
                throw new ArgumentException("Họ tên không rỗng");
            if (department == null || department == "")
                throw new ArgumentException("Phòng ban không rỗng");
            if (monthlyBonus < 0)
                throw new ArgumentException("Thưởng không âm");

            this.employeeId = employeeId;
            this.fullName = fullName;
            this.department = department;
            this.monthlyBonus = monthlyBonus;
        }

        public void addBonus(double amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Số tiền thưởng là dương");
            monthlyBonus += amount;
        }

        public void addBonus(double amount, string reason)
        {
            if (amount <= 0)
                throw new ArgumentException("Số tiền thưởng là dương");
            if (reason == null || reason == "")
                throw new ArgumentException("Lý do không rỗng");
            monthlyBonus += amount;
        }

        public void addBonus(double rate, double referenceAmount, string reason)
        {
            if (rate <= 0 || rate > 0.5)
                throw new ArgumentException("Tỷ lệ thưởng: (0, 0.5]");
            if (referenceAmount <= 0)
                throw new ArgumentException("Số tiền tham chiếu là dương");
            if (reason == null || reason == "")
                throw new ArgumentException("Lý do không rỗng");
            monthlyBonus += rate * referenceAmount;
        }

        public void resetBonus()
        {
            monthlyBonus = 0;
        }

        public abstract double calculateGrossPay();

        public abstract string getEmployeeType();

        public virtual void displayPayrollInfo()
        {
            Console.WriteLine("[{0}] Mã: {1}, Tên: {2}, Phòng ban: {3}, Thưởng: {4:N0}, Thu nhập: {5:N0}",
                getEmployeeType(), employeeId, fullName, department, monthlyBonus, calculateGrossPay());
        }

        ~Employee()
        {
        }
    }
}