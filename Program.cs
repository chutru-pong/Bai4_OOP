// Ngô Trường Phúc - 202418964

using System;

namespace ProjectManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Payroll payroll = new Payroll("2026-10");

            SalariedEmployee e1 = new SalariedEmployee("E001", "Nguyễn Minh An", "Đào tạo", 15000000, 2000000);
            e1.addBonus(1000000);
            payroll.addEmployee(e1);

            HourlyEmployee e2 = new HourlyEmployee("E002", "Trần Thu Bình", "Hỗ trợ", 100000, 150);
            e2.addBonus(500000);
            payroll.addEmployee(e2);

            HourlyEmployee e3 = new HourlyEmployee("E003", "Lê Hoàng Chi", "Hỗ trợ", 100000, 170);
            payroll.addEmployee(e3);

            SalesEmployee e4 = new SalesEmployee("E004", "Phạm Quốc Dũng", "Kinh doanh", 8000000, 200000000, 0.05);
            e4.addBonus(0.02, 50000000, "Thưởng 2% doanh số vượt mức");
            payroll.addEmployee(e4);

            Console.WriteLine("Tổng lương phòng Hỗ trợ: {0:N0}", payroll.calculatePayrollByDepartment("Hỗ trợ"));
            Console.WriteLine("Tổng lương phòng Đào tạo: {0:N0}", payroll.calculatePayrollByDepartment("Đào tạo"));
            Console.WriteLine("Tổng lương phòng Kinh doanh: {0:N0}", payroll.calculatePayrollByDepartment("Kinh doanh"));

            Employee topEarner = payroll.findHighestPaidEmployee();
            if (topEarner != null)
            {
                Console.WriteLine("Nhân viên thu nhập cao nhất: {0} - {1:N0} VNĐ",
                    topEarner.FullName, topEarner.calculateGrossPay());
            }

            PrintCheckResult("E001 - SalariedEmployee", 18000000, e1.calculateGrossPay());
            PrintCheckResult("E002 - HourlyEmployee (150h)", 15500000, e2.calculateGrossPay());
            PrintCheckResult("E003 - HourlyEmployee (170h)", 17500000, e3.calculateGrossPay());
            PrintCheckResult("E004 - SalesEmployee", 19000000, e4.calculateGrossPay());
            PrintCheckResult("Tổng bảng lương", 70000000, payroll.calculateTotalPayroll());
            PrintCheckResult("Tổng chi trả phòng Hỗ trợ", 33000000, payroll.calculatePayrollByDepartment("Hỗ trợ"));


            Console.Write("Test 1: Mã nhân viên rỗng | ");
            try
            {
                new SalariedEmployee("", "Test", "Kỹ thuật", 10000000, 0);
                Console.Write("Không lỗi ");
                PrintStatus(false);
            }
            catch (ArgumentException ex)
            {
                Console.Write("Lỗi: {0} | ", ex.Message);
                PrintStatus(true);
            }

            Console.Write("Test 2: Họ tên nhân viên rỗng | ");
            try
            {
                new HourlyEmployee("ERR02", null, "Sản xuất", 100000, 100);
                Console.Write("Không lỗi ");
                PrintStatus(false);
            }
            catch (ArgumentException ex)
            {
                Console.Write("Lỗi: {0} | ", ex.Message);
                PrintStatus(true);
            }

            Console.Write("Test 3: Phòng ban nhân viên rỗng | ");
            try
            {
                new SalesEmployee("ERR03", "Test", "", 10000000, 0, 0.05);
                Console.Write("Không lỗi ");
                PrintStatus(false);
            }
            catch (ArgumentException ex)
            {
                Console.Write("Lỗi: {0} | ", ex.Message);
                PrintStatus(true);
            }

            Console.Write("Test 4: Đơn giá giờ bằng 0 | ");
            try
            {
                new HourlyEmployee("ERR04", "Test", "Sản xuất", 0, 100);
                Console.Write("Không lỗi ");
                PrintStatus(false);
            }
            catch (ArgumentException ex)
            {
                Console.Write("Lỗi: {0} | ", ex.Message);
                PrintStatus(true);
            }

            Console.Write("Test 5: Số giờ làm âm (-5h) | ");
            try
            {
                new HourlyEmployee("ERR05", "Test", "Sản xuất", 100000, -5);
                Console.Write("Không lỗi ");
                PrintStatus(false);
            }
            catch (ArgumentException ex)
            {
                Console.Write("Lỗi: {0} | ", ex.Message);
                PrintStatus(true);
            }

            Console.Write("Test 6: Số giờ làm vượt 250h (251h) | ");
            try
            {
                new HourlyEmployee("ERR06", "Test", "Sản xuất", 100000, 251);
                Console.Write("Không lỗi ");
                PrintStatus(false);
            }
            catch (ArgumentException ex)
            {
                Console.Write("Lỗi: {0} | ", ex.Message);
                PrintStatus(true);
            }

            Console.Write("Test 7: Số giờ làm đúng biên 250h (Hợp lệ) | ");
            try
            {
                HourlyEmployee value = new HourlyEmployee("E250", "Test", "Sản xuất", 100000, 250);
                Console.Write("Khởi tạo thành công | ");
                PrintStatus(value.WorkedHours == 250);
            }
            catch (Exception ex)
            {
                Console.Write("Lỗi ngoài ý muốn: {0} ", ex.Message);
                PrintStatus(false);
            }

            Console.Write("Test 8: Lương cố định âm (-5.000.000) | ");
            try
            {
                new SalariedEmployee("ERR08", "Test", "Kỹ thuật", -5000000, 0);
                Console.Write("Không lỗi ");
                PrintStatus(false);
            }
            catch (ArgumentException ex)
            {
                Console.Write("Lỗi: {0} | ", ex.Message);
                PrintStatus(true);
            }

            Console.Write("Test 9: Tỷ lệ hoa hồng vượt 0.3 (0.35) | ");
            try
            {
                new SalesEmployee("ERR09", "Test", "Kinh doanh", 10000000, 0, 0.35);
                Console.Write("Không lỗi ");
                PrintStatus(false);
            }
            catch (ArgumentException ex)
            {
                Console.Write("Lỗi: {0} | ", ex.Message);
                PrintStatus(true);
            }

            Console.Write("Test 10: Tỷ lệ thưởng vượt 0.5 (rate = 0.6) | ");
            try
            {
                e1.addBonus(0.6, 10000000, "Vượt trần");
                Console.Write("Không lỗi ");
                PrintStatus(false);
            }
            catch (ArgumentException ex)
            {
                Console.Write("Lỗi: {0} | ", ex.Message);
                PrintStatus(true);
            }

            Console.Write("Test 11: Thêm trùng mã E001 vào Payroll | ");
            try
            {
                bool trungMa = payroll.addEmployee(new SalariedEmployee("E001", "Trùng", "Đào tạo", 10000000, 0));
                Console.Write("Trả về {0} | ", trungMa);
                PrintStatus(!trungMa);
            }
            catch (Exception ex)
            {
                Console.Write("Lỗi: {0} | ", ex.Message);
                PrintStatus(false);
            }

            Console.Write("Test 12: Thêm đối tượng rỗng vào Payroll | ");
            try
            {
                bool maRong = payroll.addEmployee(null);
                Console.Write("Trả về {0} | ", maRong);
                PrintStatus(!maRong);
            }
            catch (Exception ex)
            {
                Console.Write("Lỗi: {0} | ", ex.Message);
                PrintStatus(false);
            }
        }

        private static void PrintCheckResult(string ten, double kiVong, double thucTe)
        {
            bool isPassed = (kiVong - thucTe) == 0;
            Console.Write(string.Format("{0} | Chạy: {1:N0} | Test: {2:N0} ", ten, thucTe, kiVong));
            PrintStatus(isPassed);
        }

        private static void PrintStatus(bool isPassed)
        {
            if (isPassed)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[PASS]");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[FAIL]");
            }
            Console.ResetColor();
        }
    }
}
