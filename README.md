# 💰 QUẢN LÝ CHI TIÊU CÁ NHÂN (Personal Finance Manager)

---

## 👤 Thông tin sinh viên
- **Họ tên:** Nguyễn Chí Tấn Thành  
- **Lớp:** DK24TTC6  
- **MSSV:** 170124671  
- **Email:** thanhnct260295@tvu-onschool.edu.vn  

---

## Giới thiệu đề tài

Hiện nay việc quản lý tài chính cá nhân khá quan trọng, nhất là khi chi tiêu ngày càng nhiều.  
Đề tài này làm một website đơn giản để theo dõi thu chi hàng ngày, giúp dễ kiểm soát hơn.

Chức năng chính:
- Ghi lại thu nhập và chi tiêu  
- Phân loại theo danh mục  
- Xem thống kê bằng biểu đồ  
- Xuất báo cáo ra file Excel  
---

## Mục tiêu

- Theo dõi và kiểm soát chi tiêu cá nhân  
- Giảm bớt các khoản chi không cần thiết  
- Hỗ trợ lên kế hoạch tài chính  
- Có báo cáo để dễ xem lại  
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

## Chức năng chính

### 1. Xác thực người dùng
- Đăng ký tài khoản  
- Đăng nhập / đăng xuất  
- Kiểm tra session  
- Mã hóa mật khẩu (SHA-256) trước khi lưu  

---
## Bảo mật hệ thống

Phần mật khẩu có xử lý lại trước khi lưu để tránh lưu trực tiếp:

- Không lưu mật khẩu dạng plain text  
- Dùng SHA-256 để hash  
- Hash xong mới lưu vào database  
- Khi login thì hash lại rồi so sánh  
- Có dùng SecurityHelper để xử lý chung  

=> Mức bảo mật ở mức cơ bản, tránh lộ mật khẩu dạng thô  

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

### 2. Mở bằng Visual Studio
* Mở file: `QuanLyChiTieuCaNhan.slnx`

📂 Vị trí file trong repository:
```text
/scr/QuanLyChiTieuCaNhan/QuanLyChiTieuCaNhan.slnx
```

### 3. Cấu hình database

* Mở file Web.config

* Sửa connection string:

```xml
<connectionStrings>
  <add name="QLChiTieu"
       connectionString="Data Source=localhost\SQLEXPRESS;Initial Catalog=QuanLyChiTieuCaNhan;Integrated Security=True" />
</connectionStrings>
```

### 4. Import database

* Mở SQL Server

* Restore file QuanLyChiTieuCaNhan.bak

* Hướng dẫn dùng lệnh restore file database, ví dụ file để ở ổ đĩa D:

```sql
RESTORE DATABASE QuanLyChiTieuCaNhan
FROM DISK = 'D:\QuanLyChiTieuCaNhan.bak'
WITH REPLACE, RECOVERY;
```

### 5. Chạy project

* Nhấn F5

### 6. 🔑 Tài khoản demo

```text
- Username: admin  
- Password: 123456
```

-----------------------

## Giao diện hệ thống

Các trang chính:
- Đăng nhập  
- Quản lý danh mục  
- Quản lý chi tiêu  
- Quản lý thu nhập  
- Trang thống kê (dashboard)  

## Kết quả đạt được

- Làm được đầy đủ CRUD  
- Giao diện nhìn ổn, dễ dùng  
- Có biểu đồ để xem thu chi  
- Xuất được file Excel  
- Hệ thống chạy ổn  

## Hướng phát triển thêm

- Xuất thêm file PDF  
- Thống kê chi tiết hơn (theo năm, theo tháng)  
- Phân quyền user / admin  
- Deploy lên hosting  
- Có thể làm thêm API hoặc mobile app  

## Tiến độ thực hiện

- Week 1: setup project, tạo database  
- Week 2: làm login / logout  
- Week 3: CRUD danh mục  
- Week 4: CRUD chi tiêu  
- Week 5: CRUD thu nhập  
- Week 6: làm dashboard + biểu đồ  
- Week 7: thêm validate, export Excel  
- Week 8: chỉnh UI và hoàn thiện  

## Ghi chú

- Dữ liệu hiện tại dùng để test/demo  
- Hệ thống làm với mục đích học tập  

## Đánh giá

- Đã làm được các chức năng cơ bản:
  - CRUD  
  - Dashboard  
  - Biểu đồ  
  - Xuất file Excel  

## 📎 Link GitHub

👉 https://github.com/thanhnct28/ASPNET-dk24ttc6-nguyenchitanthanh-QLChiTieu
