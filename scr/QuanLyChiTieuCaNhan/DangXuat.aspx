<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DangXuat.aspx.cs" Inherits="QuanLyChiTieuCaNhan.DangXuat" %>

<!DOCTYPE html>

<html>

<head runat="server">

<title>Đăng xuất</title>

<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">

<style>

body{
background:linear-gradient(120deg,#2980b9,#8e44ad);
height:100vh;
display:flex;
justify-content:center;
align-items:center;
font-family:Arial;
}

.logout-box{
background:white;
padding:40px;
border-radius:10px;
text-align:center;
box-shadow:0 10px 30px rgba(0,0,0,0.2);
width:380px;
}

.spinner{
margin-top:15px;
}

</style>

</head>

<body>

<form id="form1" runat="server">

<div class="logout-box">

<h3 class="text-success">

Đăng xuất thành công

</h3>

<p>Hệ thống đang chuyển bạn về trang đăng nhập...</p>

<div class="spinner-border text-primary spinner"></div>

</div>

</form>

<script>

    setTimeout(function () {

        window.location = "DangNhap.aspx";

    }, 2000);

</script>

</body>

</html>
