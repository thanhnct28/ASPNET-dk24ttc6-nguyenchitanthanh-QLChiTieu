<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ThuNhap.aspx.cs" Inherits="QuanLyChiTieuCaNhan.ThuNhap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" rel="stylesheet">

<style>
.page-title{
    font-weight:600;
    margin-bottom:20px;
}

.card{
    border-radius:10px;
    box-shadow:0 2px 8px rgba(0,0,0,0.08);
}

.table thead{
    background:#2f3640;
    color:white;
}

/* ===== ICON ACTION ===== */
.action-wrap{
    display:flex;
    justify-content:center;
    align-items:center;
    gap:6px;
}

.action-btn{
    display:flex;
    align-items:center;
    justify-content:center;
    width:28px;
    height:28px;
    border-radius:50%;
    color:white;
    font-size:12px;
    transition:0.2s;
}

.btn-edit{ background:#007bff; }
.btn-delete{ background:#dc3545; }
.btn-save-icon{ background:#28a745; }
.btn-cancel{ background:#6c757d; }

.action-btn:hover{
    transform:scale(1.1);
    opacity:0.9;
}

.action-col{
    width:90px;
    text-align:center;
}
</style>

<div class="container mt-4">

<h4 class="page-title">
<i class="fa-solid fa-coins"></i>
Quản lý thu nhập
</h4>

<div class="card p-4 mb-4">

<div class="row g-3">

<div class="col-md-3">
<label>Số tiền</label>
<asp:TextBox ID="txtSoTien" runat="server"
CssClass="form-control"
placeholder="100000"/>
</div>

<div class="col-md-3">
<label>Ngày thu</label>
<asp:TextBox ID="txtNgayThu" runat="server"
CssClass="form-control"
TextMode="Date"/>
</div>

<div class="col-md-4">
<label>Ghi chú</label>
<asp:TextBox ID="txtGhiChu" runat="server"
CssClass="form-control"
placeholder="Nhập ghi chú..."/>
</div>

<div class="col-md-2 d-flex align-items-end">
<asp:Button ID="btnThem" runat="server"
Text="Thêm"
CssClass="btn btn-success w-100"
OnClick="BtnThem_Click"/>
</div>

</div>

<div class="mt-3 text-center">
<asp:Label ID="lblThongBao" runat="server" CssClass="fw-bold"/>
</div>

</div>

<div class="card p-4">

<asp:GridView
ID="GridView1"
runat="server"
CssClass="table table-bordered table-hover"
AutoGenerateColumns="false"
DataKeyNames="MaThuNhap"
EmptyDataText="Chưa có dữ liệu"
OnRowEditing="GridView1_RowEditing"
OnRowCancelingEdit="GridView1_RowCancelingEdit"
OnRowUpdating="GridView1_RowUpdating"
OnRowDeleting="GridView1_RowDeleting">

<Columns>

<asp:BoundField DataField="MaThuNhap" HeaderText="ID" ReadOnly="true"/>
<asp:BoundField DataField="SoTien" HeaderText="Số tiền" DataFormatString="{0:N0}"/>
<asp:BoundField DataField="NgayThu" HeaderText="Ngày thu" DataFormatString="{0:dd/MM/yyyy}"/>
<asp:BoundField DataField="GhiChu" HeaderText="Ghi chú"/>

<asp:TemplateField HeaderText="Thao tác">

<ItemTemplate>
<div class="action-wrap">

    <asp:LinkButton runat="server"
        CommandName="Edit"
        CssClass="action-btn btn-edit"
        ToolTip="Sửa">
        <i class="fa-solid fa-pen"></i>
    </asp:LinkButton>

    <asp:LinkButton runat="server"
        CommandName="Delete"
        CssClass="action-btn btn-delete"
        ToolTip="Xóa"
        OnClientClick="return confirm('Bạn có chắc muốn xóa không?');">
        <i class="fa-solid fa-trash"></i>
    </asp:LinkButton>

</div>
</ItemTemplate>

<EditItemTemplate>
<div class="action-wrap">

    <asp:LinkButton runat="server"
        CommandName="Update"
        CssClass="action-btn btn-save-icon"
        ToolTip="Lưu">
        <i class="fa-solid fa-check"></i>
    </asp:LinkButton>

    <asp:LinkButton runat="server"
        CommandName="Cancel"
        CssClass="action-btn btn-cancel"
        ToolTip="Hủy">
        <i class="fa-solid fa-xmark"></i>
    </asp:LinkButton>

</div>
</EditItemTemplate>

<ItemStyle CssClass="action-col" />
<HeaderStyle CssClass="action-col" />

</asp:TemplateField>

</Columns>

</asp:GridView>

</div>

</div>

<script>

    // format tiền
    document.getElementById("<%=txtSoTien.ClientID%>").addEventListener("input", function () {
        let value = this.value.replace(/\D/g, '');
        this.value = value.replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    });

</script>

</asp:Content>