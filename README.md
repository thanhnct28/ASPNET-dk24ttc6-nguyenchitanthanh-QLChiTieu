# 💰 QUẢN LÝ CHI TIÊU CÁ NHÂN (Personal Finance Manager)

---

## 👤 Thông tin sinh viên
- **Họ tên:** Nguyễn Chí Tấn Thành  
- **Lớp:** DK24TTC6  
- **MSSV:** 170124671  
- **Email:** thanhnct260295@tvu-onschool.edu.vn  

---

## 📌 Giới thiệu đề tài
Trong thời đại hiện nay, việc quản lý tài chính cá nhân là một nhu cầu thiết yếu.  
Đề tài xây dựng một hệ thống web giúp người dùng theo dõi, kiểm soát và phân tích thu chi một cách hiệu quả.

Hệ thống hỗ trợ:
- Theo dõi thu nhập và chi tiêu
- Phân loại theo danh mục
- Thống kê trực quan bằng biểu đồ
- Xuất báo cáo Excel

---

## 🎯 Mục tiêu hệ thống
- Giúp người dùng kiểm soát tài chính cá nhân
- Hạn chế chi tiêu không cần thiết
- Hỗ trợ lập kế hoạch tài chính
- Cung cấp báo cáo trực quan, dễ hiểu

---

## ⚙️ Công nghệ sử dụng

| Công nghệ | Mô tả |
|----------|------|
| ASP.NET WebForms | Xây dựng giao diện web |
| SQL Server | Lưu trữ dữ liệu |
| ADO.NET | Kết nối và xử lý dữ liệu |
| Bootstrap | Thiết kế giao diện |
| JavaScript | Xử lý phía client |
| Chart.js | Hiển thị biểu đồ |
| HTML/CSS | Giao diện |

---

## 🧩 Chức năng chính

### 🔐 1. Xác thực người dùng
- Đăng ký tài khoản
- Đăng nhập / đăng xuất
- Kiểm tra session
- Mã hóa mật khẩu bằng SHA-256 trước khi lưu database

---
## 🔒 Bảo mật hệ thống

Hệ thống sử dụng cơ chế mã hóa mật khẩu để đảm bảo an toàn thông tin người dùng:

- Mật khẩu không lưu dạng plain text
- Sử dụng thuật toán băm SHA-256
- Mật khẩu được hash trước khi lưu vào database
- Khi đăng nhập, hệ thống hash password nhập vào và so sánh với dữ liệu đã lưu
- Áp dụng SecurityHelper để xử lý hash tập trung
- Đảm bảo bảo mật cơ bản theo nguyên tắc không lưu mật khẩu dạng plain text

👉 **Ví dụ:**
```text
Password: 123456
Hash: 8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92
```

---

### 📂 2. Quản lý danh mục
- Thêm danh mục
- Sửa danh mục
- Xóa danh mục
- Phân loại chi tiêu

---

### 💸 3. Quản lý chi tiêu
- Thêm khoản chi
- Chỉnh sửa chi tiêu
- Xóa chi tiêu
- Ghi chú chi tiết

---

### 💰 4. Quản lý thu nhập
- Thêm thu nhập
- Sửa thu nhập
- Xóa thu nhập

---

### 📊 5. Thống kê & báo cáo
- Tổng thu / tổng chi
- Tính số dư
- Tỷ lệ tiết kiệm (%)
- Biểu đồ thu chi theo tháng (Line Chart)
- Biểu đồ chi tiêu theo danh mục (Pie Chart)

---

### 📤 6. Xuất báo cáo Excel
- Xuất dữ liệu thu + chi chung 1 file
- Lọc theo khoảng thời gian
- Hiển thị:
  - Ngày
  - Loại (Thu / Chi)
  - Danh mục
  - Số tiền
  - Ghi chú

---

## 🗄️ Thiết kế cơ sở dữ liệu

### 📌 Các bảng chính:
- **NguoiDung** – thông tin người dùng
- **DanhMucChi** – danh mục chi tiêu
- **ChiTieu** – các khoản chi
- **ThuNhap** – các khoản thu

---

## 🚀 Hướng dẫn cài đặt & chạy

### 1. Clone project

```powershell
git clone https://github.com/thanhnct28/ASPNET-dk24ttc6-nguyenchitanthanh-QLChiTieu.git
```

2. Mở bằng Visual Studio

Mở file QuanLyChiTieuCaNhan.slnx

3. Cấu hình database

Mở file Web.config

Sửa connection string:

```xml
<connectionStrings>
  <add name="QLChiTieu"
       connectionString="Data Source=localhost\SQLEXPRESS;Initial Catalog=QuanLyChiTieuCaNhan;Integrated Security=True" />
</connectionStrings>
```

4. Import database

Mở SQL Server

Restore file QuanLyChiTieuCaNhan.bak

Hướng dẫn dùng lệnh restore file database, ví dụ file để ở ổ đĩa D:

```sql
RESTORE DATABASE QuanLyChiTieuCaNhan
FROM DISK = 'D:\QuanLyChiTieuCaNhan.bak'
WITH REPLACE, RECOVERY;
```

5. Chạy project

Nhấn F5

6.  ## 🔑 Tài khoản demo

```text
- Username: admin  
- Password: 123456
```

-----------------------

## 🖥️ Giao diện hệ thống
📌 Các trang chính:

Đăng nhập

Quản lý danh mục

Quản lý chi tiêu

Quản lý thu nhập

Dashboard thống kê

📈 Kết quả đạt được

Hoàn thành đầy đủ chức năng CRUD

Giao diện thân thiện, dễ sử dụng

Thống kê trực quan bằng biểu đồ

Xuất báo cáo Excel

Hệ thống hoạt động ổn định

🔧 Hướng phát triển

Xuất báo cáo PDF

Thống kê theo năm nâng cao

Phân quyền người dùng

Triển khai lên hosting

Xây dựng API / Mobile App

📅 Tiến độ thực hiện
Tuần	Nội dung
```text
Week 1	Setup project + database
Week 2	Login / Logout
Week 3	CRUD DanhMuc
Week 4	CRUD ChiTieu
Week 5	CRUD ThuNhap
Week 6	Dashboard + Chart
Week 7	Validate + Export Excel
Week 8	Hoàn thiện UI + Final
```

📌 Ghi chú

Dữ liệu chỉ mang tính demo

Hệ thống phục vụ mục đích học tập

⭐ Đánh giá

Đồ án đáp ứng đầy đủ yêu cầu:

Có CRUD

Có Dashboard

Có biểu đồ

Có xuất báo cáo


## 📎 Link GitHub

👉 https://github.com/thanhnct28/ASPNET-dk24ttc6-nguyenchitanthanh-QLChiTieu
