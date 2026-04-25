<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ThongKe.aspx.cs" Inherits="QuanLyChiTieuCaNhan.ThongKe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

<style>
.page-title{
    font-weight:600;
    margin-bottom:20px;
}
.card{
    border-radius:14px;
    box-shadow:0 4px 18px rgba(0,0,0,0.08);
    border:none;
}
.summary{
    font-size:24px;
    font-weight:bold;
}
</style>

<div class="container-fluid">

<h3 class="page-title">Dashboard tài chính cá nhân</h3>

<!-- FILTER -->
<div class="row mb-4">

<div class="col-md-3">
<label>Từ ngày</label>
<asp:TextBox ID="txtFrom" runat="server" TextMode="Date" CssClass="form-control"/>
</div>

<div class="col-md-3">
<label>Đến ngày</label>
<asp:TextBox ID="txtTo" runat="server" TextMode="Date" CssClass="form-control"/>
</div>

<div class="col-md-2 d-flex align-items-end">
<asp:Button ID="btnFilter" runat="server" Text="Lọc" CssClass="btn btn-primary w-100" OnClick="BtnFilter_Click"/>
</div>

<div class="col-md-2 d-flex align-items-end">
<asp:Button ID="btnExcel" runat="server" Text="Xuất Excel" CssClass="btn btn-success w-100" OnClick="BtnExcel_Click"/>
</div>

</div>

<!-- SUMMARY -->
<div class="row mb-4">

<div class="col-md-3">
<div class="card p-3 text-center">
<h6>Tổng thu</h6>
<div class="summary text-success">
<asp:Label ID="lblThu" runat="server"></asp:Label>
</div>
</div>
</div>

<div class="col-md-3">
<div class="card p-3 text-center">
<h6>Tổng chi</h6>
<div class="summary text-danger">
<asp:Label ID="lblChi" runat="server"></asp:Label>
</div>
</div>
</div>

<div class="col-md-3">
<div class="card p-3 text-center">
<h6>Số dư</h6>
<div class="summary text-primary">
<asp:Label ID="lblConLai" runat="server"></asp:Label>
</div>
</div>
</div>

<div class="col-md-3">
<div class="card p-3 text-center">
<h6>Tỷ lệ tiết kiệm</h6>
<div class="summary text-info">
<asp:Label ID="lblTietKiem" runat="server"></asp:Label>
</div>
</div>
</div>

</div>

<!-- CHART -->
<div class="row mb-4">

<div class="col-md-8">
<div class="card p-4">
<h5>Xu hướng thu chi theo tháng</h5>
<canvas id="lineChart"></canvas>
</div>
</div>

<div class="col-md-4">
<div class="card p-4">
<h5>Chi tiêu theo danh mục</h5>
<canvas id="pieChart"></canvas>
</div>
</div>

</div>

<!-- TOP -->
<div class="card p-4">

<h5>Top 5 danh mục chi nhiều nhất</h5>

<asp:GridView
ID="gridTop"
runat="server"
CssClass="table table-striped"
AutoGenerateColumns="false"
EmptyDataText="Không có dữ liệu">

<Columns>
<asp:BoundField DataField="TenDanhMuc" HeaderText="Danh mục"/>
<asp:BoundField DataField="TongTien" HeaderText="Tổng tiền" DataFormatString="{0:N0}"/>
</Columns>

</asp:GridView>

</div>

</div>

<script>
    var thu = <%= chartThu %>;
    var chi = <%= chartChi %>;
var danhMuc = <%= chartDanhMuc %>;
var tienDanhMuc = <%= chartTienDanhMuc %>;

    var labels = ['T1', 'T2', 'T3', 'T4', 'T5', 'T6', 'T7', 'T8', 'T9', 'T10', 'T11', 'T12'];

    new Chart(document.getElementById("lineChart"), {
        type: 'line',
        data: {
            labels: labels,
            datasets: [
                { label: 'Thu', borderColor: 'green', data: thu, fill: false },
                { label: 'Chi', borderColor: 'red', data: chi, fill: false }
            ]
        }
    });

    new Chart(document.getElementById("pieChart"), {
        type: 'pie',
        data: {
            labels: danhMuc,
            datasets: [{
                data: tienDanhMuc,
                backgroundColor: ['#4CAF50', '#2196F3', '#FFC107', '#FF5722', '#9C27B0']
            }]
        }
    });
</script>

</asp:Content>