<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DangNhap.aspx.cs" Inherits="QuanLyChiTieuCaNhan.DangNhap" %>

<!DOCTYPE html>
<html>
<head runat="server">
<meta charset="utf-8"/>
<title>Đăng nhập hệ thống</title>

<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.2/css/all.min.css">

<style>

/* BACKGROUND */
body{
    background: linear-gradient(135deg,#43cea2,#185a9d);
    height:100vh;
    display:flex;
    justify-content:center;
    align-items:center;
    font-family: 'Segoe UI', sans-serif;
}

/* BOX */
.login-box{
    width:400px;
    background: white;
    padding:40px;
    border-radius:16px;
    box-shadow:0 15px 40px rgba(0,0,0,0.2);
}

/* TITLE */
.login-title{
    text-align:center;
    margin-bottom:25px;
    font-weight:600;
    color:#2c3e50;
}

/* INPUT */
.input-group-text{
    background:#f1f1f1;
    border:none;
}

.form-control{
    background:#f9f9f9;
    border:none;
}

.form-control:focus{
    background:#fff;
    box-shadow:none;
    border:1px solid #43cea2;
}

/* ICON */
.toggle-password{
    cursor:pointer;
    background:#f1f1f1;
}

.toggle-password:hover{
    background:#e0e0e0;
}

/* BUTTON */
.btn-login{
    width:100%;
    font-weight:bold;
    background: linear-gradient(45deg,#00c853,#43a047);
    border:none;
    color:white;
}

.btn-login:hover{
    opacity:0.9;
}

/* REGISTER */
.register-link{
    text-align:center;
    margin-top:15px;
}

.register-link a{
    color:#185a9d;
    text-decoration:none;
    font-weight:500;
}

.register-link a:hover{
    text-decoration:underline;
}

/* FOOTER */
.footer{
    text-align:center;
    margin-top:15px;
    font-size:13px;
    color:#888;
}

</style>
</head>

<body>

<form id="form1" runat="server" defaultbutton="btnDangNhap">

<div class="login-box">

<h3 class="login-title">
<i class="fa fa-wallet"></i> QUẢN LÝ CHI TIÊU
</h3>

<!-- USER -->
<div class="mb-3">
<label>Tên đăng nhập</label>
<div class="input-group">
<span class="input-group-text"><i class="fa fa-user"></i></span>
<asp:TextBox ID="txtUser" runat="server" CssClass="form-control" placeholder="Nhập tên đăng nhập"/>
</div>
</div>

<!-- PASS -->
<div class="mb-3">
<label>Mật khẩu</label>
<div class="input-group">
<span class="input-group-text"><i class="fa fa-lock"></i></span>

<asp:TextBox ID="txtPass" runat="server"
    TextMode="Password"
    CssClass="form-control"
    placeholder="Nhập mật khẩu"/>

<span class="input-group-text toggle-password" onclick="togglePassword()">
<i id="eyeIcon" class="fa fa-eye"></i>
</span>

</div>
</div>

<!-- BUTTON -->
<div class="mb-3">
<asp:Button ID="btnDangNhap"
    runat="server"
    Text="Đăng nhập"
    CssClass="btn btn-login"
    OnClick="btnDangNhap_Click"/>
</div>

<!-- MESSAGE -->
<div class="text-center">
<asp:Label ID="lblThongBao" runat="server" CssClass="text-danger"/>
</div>

<!-- REGISTER -->
<div class="register-link">
Chưa có tài khoản?
<a href="DangKy.aspx">
<i class="fa fa-user-plus"></i> Đăng ký ngay
</a>
</div>

<div class="footer">
Quản lý tài chính thông minh hơn mỗi ngày 💰
</div>

</div>

</form>

<!-- JS SHOW/HIDE PASSWORD -->
<script>
    function togglePassword() {
        var txt = document.getElementById("<%= txtPass.ClientID %>");
        var icon = document.getElementById("eyeIcon");

        if (txt.type === "password") {
            txt.type = "text";
            icon.classList.remove("fa-eye");
            icon.classList.add("fa-eye-slash");
        } else {
            txt.type = "password";
            icon.classList.remove("fa-eye-slash");
            icon.classList.add("fa-eye");
        }
    }
</script>

</body>
</html>