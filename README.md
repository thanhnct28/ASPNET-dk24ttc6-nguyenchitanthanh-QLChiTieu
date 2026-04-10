# 💰 QUẢN LÝ CHI TIÊU CÁ NHÂN (Personal Finance Manager)

---

## 👤 Thông tin sinh viên
* **Họ tên:** Nguyễn Chí Tấn Thành  
* **Lớp:** DK24TTC6  
* **MSSV:** 170124671  
* **Email:** thanhnct260295@tvu-onschool.edu.vn  

---

## 📌 Giới thiệu đề tài
Trong thời đại hiện nay, việc quản lý tài chính cá nhân là một nhu cầu thiết yếu.  
Đề tài xây dựng một hệ thống web giúp người dùng theo dõi, kiểm soát và phân tích thu chi một cách hiệu quả.

**Hệ thống hỗ trợ:**
* Theo dõi thu nhập và chi tiêu.
* Phân loại theo danh mục linh hoạt.
* Thống kê trực quan bằng biểu đồ (Charts).
* Xuất báo cáo dữ liệu ra file Excel.

---

## 🎯 Mục tiêu hệ thống
* Giúp người dùng kiểm soát tài chính cá nhân chặt chẽ.
* Hạn chế các khoản chi tiêu không cần thiết.
* Hỗ trợ lập kế hoạch tài chính dài hạn.
* Cung cấp báo cáo trực quan, dễ hiểu cho người dùng.

---

## ⚙️ Công nghệ sử dụng

| Công nghệ | Mô tả |
| :--- | :--- |
| **ASP.NET WebForms** | Framework chính xây dựng giao diện và xử lý logic web |
| **SQL Server** | Hệ quản trị cơ sở dữ liệu quan hệ |
| **ADO.NET** | Thư viện kết nối và tương tác dữ liệu SQL |
| **Bootstrap 5** | Framework CSS thiết kế giao diện Responsive |
| **JavaScript / jQuery** | Xử lý các tương tác phía Client |
| **Chart.js** | Thư viện hiển thị biểu đồ thống kê |
| **EPPlus** | Thư viện hỗ trợ xuất báo cáo ra file Excel |

---

## 🧩 Chức năng chính

### 🔐 1. Xác thực người dùng
* Đăng ký tài khoản mới.
* Đăng nhập / Đăng xuất hệ thống.
* Kiểm tra Session để bảo vệ các trang nội bộ.
* Mã hóa mật khẩu bằng thuật toán **SHA-256** trước khi lưu vào Database.

### 📂 2. Quản lý danh mục
* Thêm, sửa, xóa các loại danh mục (Ăn uống, Di chuyển, Lương...).
* Phân loại khoản chi dựa trên danh mục đã tạo.

### 💸 3. Quản lý chi tiêu & Thu nhập
* Ghi chép chi tiết các khoản chi và thu nhập hàng ngày.
* Hỗ trợ chỉnh sửa và xóa các bản ghi sai sót.
* Ghi chú chi tiết cho từng giao dịch.

### 📊 4. Thống kê & Báo cáo
* Tự động tính toán Tổng thu / Tổng chi và Số dư hiện tại.
* **Biểu đồ thu chi theo tháng:** Sử dụng Line Chart để theo dõi biến động.
* **Biểu đồ theo danh mục:** Sử dụng Pie Chart để xem tỷ trọng chi tiêu.

### 📤 5. Xuất báo cáo Excel
* Xuất toàn bộ dữ liệu thu và chi chung vào 1 file Excel chuyên nghiệp.
* Hỗ trợ lọc dữ liệu theo khoảng thời gian trước khi xuất.

---

## 🔒 Bảo mật hệ thống

Hệ thống sử dụng cơ chế mã hóa mật khẩu để đảm bảo an toàn thông tin:
* Mật khẩu tuyệt đối **không** lưu dạng plain text.
* Sử dụng lớp `SecurityHelper` để thực hiện hash SHA-256 tập trung.
* Khi đăng nhập, hệ thống hash password nhập vào và so sánh với chuỗi hash trong database.

> **Ví dụ:**
> * **Password:** `123456`
> * **Hash:** `8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92`

---

## 🗄️ Thiết kế cơ sở dữ liệu

Hệ thống bao gồm các bảng chính liên kết chặt chẽ với nhau:
* `NguoiDung`: Lưu trữ thông tin tài khoản và mật khẩu đã hash.
* `DanhMucChi`: Định nghĩa các nhóm chi tiêu riêng cho từng user.
* `ChiTieu`: Lưu trữ lịch sử các khoản chi.
* `ThuNhap`: Lưu trữ lịch sử các khoản thu.

---

## 🚀 Hướng dẫn cài đặt & Chạy project

### 1. Clone project
Mở Terminal hoặc Git Bash và chạy lệnh:
```powershell
git clone https://github.com/thanhnct28/ASPNET-dk24ttc6-nguyenchitanthanh-QLChiTieu.git

2. Mở bằng Visual Studio

Mở file giải pháp: QuanLyChiTieuCaNhan.slnx (hoặc .sln).
3. Cấu hình Database

Mở file Web.config và chỉnh sửa chuỗi kết nối phù hợp với máy của bạn:
XML

<connectionStrings>
  <add name="QLChiTieu" 
       connectionString="Data Source=localhost\SQLEXPRESS;Initial Catalog=QuanLyChiTieuCaNhan;Integrated Security=True" 
       providerName="System.Data.SqlClient" />
</connectionStrings>

4. Import Database

Sử dụng lệnh sau trong SQL Server để Restore dữ liệu từ file .bak:
SQL

USE master;
GO
RESTORE DATABASE QuanLyChiTieuCaNhan 
FROM DISK = 'D:\QuanLyChiTieuCaNhan.bak' 
WITH REPLACE, RECOVERY;
GO

5. Chạy project

Nhấn F5 để khởi chạy ứng dụng.
🔑 Tài khoản Demo

    Username: admin

    Password: 123456

🖥️ Giao diện hệ thống

📌 Các trang chức năng chính:

    Trang Đăng nhập

    Dashboard thống kê tổng quan

    Quản lý danh mục & Thu chi

    Trang xuất báo cáo Excel

📅 Tiến độ thực hiện

    [x] Week 1: Setup project + database

    [x] Week 2: Login / Logout

    [x] Week 3: CRUD Danh mục

    [x] Week 4: CRUD Chi tiêu

    [x] Week 5: CRUD Thu nhập

    [x] Week 6: Dashboard + Chart

    [x] Week 7: Validate + Export Excel

    [x] Week 8: Hoàn thiện UI + Final

📎 Liên kết dự án

👉 GitHub: https://github.com/thanhnct28/ASPNET-dk24ttc6-nguyenchitanthanh-QLChiTieu
