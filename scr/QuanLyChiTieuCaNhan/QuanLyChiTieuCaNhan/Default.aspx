<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="QuanLyChiTieuCaNhan.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

<h2 class="mb-4">Dashboard</h2>

<div class="row">

<div class="col-md-4">
<div class="card shadow">
<div class="card-body text-center">

<h5>Tổng thu</h5>

<h3 class="text-success">
<asp:Label ID="lblThu" runat="server"></asp:Label>
</h3>

</div>
</div>
</div>

<div class="col-md-4">
<div class="card shadow">
<div class="card-body text-center">

<h5>Tổng chi</h5>

<h3 class="text-danger">
<asp:Label ID="lblChi" runat="server"></asp:Label>
</h3>

</div>
</div>
</div>

<div class="col-md-4">
<div class="card shadow">
<div class="card-body text-center">

<h5>Số dư</h5>

<h3 class="text-primary">
<asp:Label ID="lblConLai" runat="server"></asp:Label>
</h3>

</div>
</div>
</div>

</div>

<hr>

<h4>Biểu đồ thu chi theo tháng</h4>

<canvas id="chartThuChi" height="100"></canvas>

<hr>

<h4>Chi tiêu gần đây</h4>

<input type="text" id="searchInput" class="form-control mb-3" placeholder="Tìm kiếm...">

<asp:GridView ID="GridView1"
runat="server"
CssClass="table table-bordered table-striped"
AutoGenerateColumns="false"
EmptyDataText="Chưa có dữ liệu">

<Columns>

<asp:BoundField DataField="STT" HeaderText="STT" />
<asp:BoundField DataField="TenDanhMuc" HeaderText="Danh mục" />
<asp:BoundField DataField="SoTien" HeaderText="Số tiền" DataFormatString="{0:N0}" />
<asp:BoundField DataField="NgayChi" HeaderText="Ngày chi" DataFormatString="{0:dd/MM/yyyy}" />
<asp:BoundField DataField="GhiChu" HeaderText="Ghi chú" />

</Columns>

</asp:GridView>

<script>

    document.getElementById("searchInput").addEventListener("keyup", function () {

        var value = this.value.toLowerCase()

        var rows = document.querySelectorAll("#MainContent_GridView1 tbody tr")

        rows.forEach(function (row) {

            row.style.display = row.innerText.toLowerCase().includes(value) ? "" : "none"

        })

    })

    var thu =<%=jsonThu%>
var chi=<%=jsonChi%>

var ctx = document.getElementById('chartThuChi')

    new Chart(ctx, {

        type: 'bar',

        data: {
            labels: ["T1", "T2", "T3", "T4", "T5", "T6", "T7", "T8", "T9", "T10", "T11", "T12"],
            datasets: [
                {
                    label: "Thu nhập",
                    backgroundColor: "#2ecc71",
                    data: thu
                },
                {
                    label: "Chi tiêu",
                    backgroundColor: "#e74c3c",
                    data: chi
                }
            ]
        },

        options: {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top'
                }
            }
        }

    })

</script>

</asp:Content>
