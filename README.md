# BÀI TẬP: HỆ THỐNG TÍNH LƯƠNG VÀ THƯỞNG NHÂN SỰ

---

## 1. Nghiệp vụ

Một doanh nghiệp cần chương trình tính thu nhập hằng tháng cho ba loại nhân sự:
- **Salaried Employee**: Nhân viên hưởng lương cố định.
- **Hourly Employee**: Nhân viên hưởng lương theo giờ.
- **Sales Employee**: Nhân viên kinh doanh hưởng lương cơ bản và hoa hồng.

Mọi nhân sự đều có mã, họ tên, phòng ban và khoản thưởng trong tháng. Mỗi loại nhân sự có công thức tính thu nhập khác nhau. Bộ phận kế toán muốn có nhiều cách ghi nhận thưởng và nhiều cách khởi tạo nhân sự tùy theo mức độ đầy đủ của dữ liệu.

---

## PHẦN A: PHÂN TÍCH VÀ THIẾT KẾ

### A.1. Xác định các lớp tối thiểu cần có
- `Employee`: Lớp cơ sở .
- `SalariedEmployee`: Nhân viên lương cố định.
- `HourlyEmployee`: Nhân viên theo giờ.
- `SalesEmployee`: Nhân viên kinh doanh.
- `Payroll`: Lớp xử lý danh sách và tổng hợp bảng lương.

> **Lưu ý:** `Payroll` không phải là một loại `Employee`; do đó **không** được sử dụng kế thừa giữa hai lớp này (quan hệ Association / Has-a).

---

### A.2. Lớp `Employee`

#### Thuộc tính chung:
- `employeeId`: Mã nhân sự (`string`).
- `fullName`: Họ tên (`string`).
- `department`: Phòng ban (`string`).
- `monthlyBonus`: Tổng thưởng trong tháng (`double`).

#### Bất biến – Quy tắc hợp lệ:
- Mã nhân sự **không rỗng** / null.
- Họ tên **không rỗng** / null.
- Phòng ban **không rỗng** / null.
- Thưởng **không được âm** ($monthlyBonus \ge 0$).

---

### A.3. Các lớp dẫn xuất

#### 1. Salaried Employee
- **Thuộc tính bổ sung**:
  - `monthlySalary`: Lương cố định tháng (`double`).
  - `responsibilityAllowance`: Phụ cấp trách nhiệm (`double`).
- **Quy tắc**: `monthlySalary >= 0`, `responsibilityAllowance >= 0`.
- **Công thức thu nhập trước khấu trừ**:
  $$\text{grossPay} = \text{monthlySalary} + \text{responsibilityAllowance} + \text{monthlyBonus}$$

#### 2. Hourly Employee
- **Thuộc tính bổ sung**:
  - `hourlyRate`: Đơn giá mỗi giờ (`double`).
  - `workedHours`: Số giờ làm trong tháng (`double`).
- **Quy tắc tính**:
  - Số giờ làm hợp lệ: $0 \le \text{workedHours} \le 250$.
  - Đơn giá giờ: $\text{hourlyRate} > 0$.
  - Nếu $\text{workedHours} \le 160$:
    $$\text{basePay} = \text{workedHours} \times \text{hourlyRate}$$
  - Nếu $\text{workedHours} > 160$:
    $$\text{basePay} = 160 \times \text{hourlyRate} + (\text{workedHours} - 160) \times \text{hourlyRate} \times 1.5$$
- **Thu nhập trước khấu trừ**:
  $$\text{grossPay} = \text{basePay} + \text{monthlyBonus}$$

#### 3. Sales Employee
- **Thuộc tính bổ sung**:
  - `baseSalary`: Lương cơ bản (`double`).
  - `salesRevenue`: Doanh số bán hàng (`double`).
  - `commissionRate`: Tỷ lệ hoa hồng (`double`).
- **Quy tắc**:
  - `baseSalary >= 0`, `salesRevenue >= 0`.
  - Tỷ lệ hoa hồng: $0 \le \text{commissionRate} \le 0.3$.
- **Công thức thu nhập trước khấu trừ**:
  $$\text{grossPay} = \text{baseSalary} + (\text{salesRevenue} \times \text{commissionRate}) + \text{monthlyBonus}$$

---

### A.4. Nạp chồng Constructor

- **Lớp `Employee`** có ít nhất 2 constructor:
  1. `Employee(employeeId, fullName)` $\rightarrow$ Mặc định: `department = "Unassigned"`, `monthlyBonus = 0`.
  2. `Employee(employeeId, fullName, department)` $\rightarrow$ Mặc định: `monthlyBonus = 0`.
  3. `Employee(employeeId, fullName, department, monthlyBonus)` (Constructor đầy đủ).
- **Mỗi lớp dẫn xuất** phải có tối thiểu 2 constructor:
  1. Constructor rút gọn (dùng giá trị mặc định hợp lý).
  2. Constructor đầy đủ (nhận toàn bộ dữ liệu cần thiết).
- **Yêu cầu kỹ thuật**: Tránh lặp lại logic kiểm tra bằng ủy quyền constructor (`: this(...)` hoặc `: base(...)`).

---

### A.5. Nạp chồng phương thức thưởng (`addBonus`)

Trong lớp `Employee`, thiết kế 3 phiên bản `addBonus()`:
1. `addBonus(amount)`: Thêm một khoản thưởng cố định.
2. `addBonus(amount, reason)`: Thêm khoản thưởng cố định kèm lý do.
3. `addBonus(rate, referenceAmount, reason)`: Tính thưởng theo tỷ lệ của một giá trị tham chiếu, kèm lý do.

#### Quy tắc kiểm tra tính hợp lệ:
- `amount > 0`
- $0 < \text{rate} \le 0.5$
- `referenceAmount > 0`
- Nếu có `reason`, nội dung **không được rỗng**.
- Mọi khoản thưởng hợp lệ được cộng dồn vào `monthlyBonus`.

---

### A.6. Ghi đè phương thức tính lương (Polymorphism)

- **Lớp `Employee`** khai báo các hành vi đa hình (`virtual` / `abstract`):
  - `calculateGrossPay()`: Tính tổng thu nhập.
  - `getEmployeeType()`: Trả về chuỗi tên loại nhân viên.
  - `displayPayrollInfo()`: In thông tin chi tiết bảng lương của nhân viên.
- **Các lớp dẫn xuất** ghi đè (`override`) các phương thức này theo công thức và thông tin riêng.

---

### A.7. Lớp `Payroll`

Quản lý danh sách nhân sự trong một kỳ lương.

- **Thuộc tính**:
  - `period`: Kỳ lương (ví dụ: `"2026-09"`).
  - Danh sách nhân sự: `List<Employee>`.
- **Phương thức**:
  - `addEmployee(Employee employee)`: Thêm nhân viên vào danh sách (kiểm tra không trùng `employeeId`).
  - `findEmployee(string employeeId)`: Tìm kiếm nhân viên theo mã.
  - `calculateTotalPayroll()`: Tính tổng tiền lương chi trả trong kỳ lương.
  - `calculatePayrollByDepartment(string department)`: Tính tổng tiền lương theo từng phòng ban.
  - `findHighestPaidEmployee()`: Tìm nhân viên có thu nhập cao nhất.
  - `displayPayroll()`: Hiển thị bảng lương toàn thể nhân viên.
- **Ràng buộc**:
  - Không có 2 nhân sự cùng mã trong một bảng lương.
  - Các phép tổng hợp phải gọi `calculateGrossPay()` qua kiểu tham chiếu chung `Employee` (tận dụng tính đa hình).
  - **Không dùng chuỗi `if/else` hoặc `switch-case` theo loại nhân sự** để tính lương.

---


## PHẦN B: CÀI ĐẶT

### B.1. Lớp `Employee`
- Đóng gói thuộc tính với getter/setter.
- Cài đặt constructor nạp chồng với constructor delegation.
- Cài đặt 3 phiên bản `addBonus()`.
- Cung cấp phương thức `resetBonus()` khi sang kỳ lương mới.
- Khai báo các phương thức `virtual` (`calculateGrossPay`, `getEmployeeType`, `displayPayrollInfo`).

### B.2. Lớp `SalariedEmployee`
- Kế thừa `Employee`.
- Cài đặt 2 constructor nạp chồng.
- Kiểm tra hợp lệ `monthlySalary >= 0`, `responsibilityAllowance >= 0`.
- Ghi đè `calculateGrossPay()`, `getEmployeeType()`, `displayPayrollInfo()`.

### B.3. Lớp `HourlyEmployee`
- Kế thừa `Employee`.
- Cài đặt 2 constructor nạp chồng.
- Kiểm tra `hourlyRate > 0`, $0 \le \text{workedHours} \le 250$.
- Tính lương làm thêm ngoài 160 giờ hệ số $1.5$.
- Ghi đè các phương thức tương ứng.

### B.4. Lớp `SalesEmployee`
- Kế thừa `Employee`.
- Cài đặt 2 constructor nạp chồng.
- Kiểm tra $0 \le \text{commissionRate} \le 0.3$.
- Cung cấp phương thức `setSalesRevenue(revenue)` để cập nhật doanh số.
- Ghi đè các phương thức tính thu nhập và hiển thị.

### B.5. Lớp `Payroll`
- Ngăn chặn thêm trùng `employeeId`.
- Gọi `calculateGrossPay()` qua đa hình.
- Cài đặt các hàm tính tổng, tính theo phòng ban, tìm nhân viên thu nhập cao nhất và hiển thị bảng lương.
