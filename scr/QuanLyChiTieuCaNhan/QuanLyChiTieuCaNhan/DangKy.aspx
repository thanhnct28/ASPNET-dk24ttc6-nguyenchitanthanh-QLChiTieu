<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DangKy.aspx.cs" Inherits="QuanLyChiTieuCaNhan.DangKy" %>

<!DOCTYPE html>
<html>
<head runat="server">
<meta charset="utf-8"/>
<title>Đăng ký</title>

<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.2/css/all.min.css">

<style>

/* BACKGROUND  */
body{
    background: linear-gradient(135deg,#56ab2f,#a8e063);
    height:100vh;
    display:flex;
    justify-content:center;
    align-items:center;
    font-family: 'Segoe UI', sans-serif;
}

/* BOX */
.register-box{
    width:420px;
    background:white;
    padding:35px;
    border-radius:16px;
    box-shadow:0 15px 40px rgba(0,0,0,0.2);
}

/* TITLE */
.title{
    text-align:center;
    font-weight:600;
    margin-bottom:20px;
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
    border:1px solid #56ab2f;
}

.toggle-password{
    cursor:pointer;
    background:#f1f1f1;
}

.toggle-password:hover{
    background:#e0e0e0;
}

/* BUTTON */
.btn-register{
    width:100%;
    font-weight:bold;
    background: linear-gradient(45deg,#00c853,#66bb6a);
    border:none;
    color:white;
}

.btn-register:hover{
    opacity:0.9;
}

/* FOOTER */
.footer{
    text-align:center;
    margin-top:15px;
}

.footer a{
    color:#2c3e50;
    text-decoration:none;
    font-weight:500;
}

.footer a:hover{
    text-decoration:underline;
}

</style>
</head>

<body>

<form id="form1" runat="server">

<div class="register-box">

<h3 class="title">
<i class="fa fa-user-plus"></i> ĐĂNG KÝ TÀI KHOẢN
</h3>

<!-- HỌ TÊN -->
<div class="mb-3">
<label>Họ tên</label>
<asp:TextBox ID="txtHoTen" runat="server" CssClass="form-control"/>
</div>

<!-- EMAIL -->
<div class="mb-3">
<label>Email</label>
<asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"/>
</div>

<!-- USER -->
<div class="mb-3">
<label>Tên đăng nhập</label>
<asp:TextBox ID="txtUser" runat="server" CssClass="form-control"/>
</div>

<!-- PASS -->
<div class="mb-3">
<label>Mật khẩu</label>
<div class="input-group">
<span class="input-group-text"><i class="fa fa-lock"></i></span>

<asp:TextBox ID="txtPass" runat="server"
    TextMode="Password"
    CssClass="form-control"/>

<span class="input-group-text toggle-password" onclick="togglePass()">
<i id="eyePass" class="fa fa-eye"></i>
</span>
</div>
</div>

<!-- CONFIRM -->
<div class="mb-3">
<label>Nhập lại mật khẩu</label>
<div class="input-group">
<span class="input-group-text"><i class="fa fa-lock"></i></span>

<asp:TextBox ID="txtConfirm" runat="server"
    TextMode="Password"
    CssClass="form-control"/>

<span class="input-group-text toggle-password" onclick="toggleConfirm()">
<i id="eyeConfirm" class="fa fa-eye"></i>
</span>
</div>
</div>

<!-- BUTTON -->
<asp:Button ID="btnDangKy" runat="server"
    Text="Đăng ký"
    CssClass="btn btn-register"
    OnClick="btnDangKy_Click" />

<!-- MESSAGE -->
<div class="text-center mt-3">
<asp:Label ID="lblThongBao" runat="server"/>
</div>

<div class="footer">
<a href="DangNhap.aspx">← Quay lại đăng nhập</a>
</div>

</div>

</form>

<!-- JS -->
<script>
    function togglePass() {
        var txt = document.getElementById("<%= txtPass.ClientID %>");
    var icon = document.getElementById("eyePass");

    if (txt.type === "password") {
        txt.type = "text";
        icon.classList.replace("fa-eye", "fa-eye-slash");
    } else {
        txt.type = "password";
        icon.classList.replace("fa-eye-slash", "fa-eye");
    }
}

function toggleConfirm() {
    var txt = document.getElementById("<%= txtConfirm.ClientID %>");
        var icon = document.getElementById("eyeConfirm");

        if (txt.type === "password") {
            txt.type = "text";
            icon.classList.replace("fa-eye", "fa-eye-slash");
        } else {
            txt.type = "password";
            icon.classList.replace("fa-eye-slash", "fa-eye");
        }
    }
</script>

</body>
</html>