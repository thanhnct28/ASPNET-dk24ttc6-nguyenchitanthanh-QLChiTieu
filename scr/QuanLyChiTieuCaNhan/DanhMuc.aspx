<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DanhMuc.aspx.cs" Inherits="QuanLyChiTieuCaNhan.DanhMuc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" rel="stylesheet">

<style>
.page-title{
    font-weight:600;
    margin-bottom:20px;
}

.card{
    border:none;
    border-radius:10px;
    box-shadow:0 3px 12px rgba(0,0,0,0.08);
}

.table thead{
    background:#2c3e50;
    color:white;
}

.table tbody tr:hover{
    background:#f1f3f5;
}

/* ===== FIX ICON NẰM NGANG ===== */
.action-group{
    display:flex;
    justify-content:center;
    align-items:center;
    gap:6px; /* khoảng cách giữa 2 icon */
}

/* ICON BUTTON */
.action-btn{
    display:flex;
    align-items:center;
    justify-content:center;
    width:26px;
    height:26px;
    border-radius:50%;
    color:white;
    font-size:12px;
    transition:0.2s;
}

/* màu */
.btn-edit{ background:#007bff; }
.btn-delete{ background:#dc3545; }
.btn-save-icon{ background:#28a745; }
.btn-cancel{ background:#6c757d; }

/* hover */
.action-btn:hover{
    transform:scale(1.1);
    opacity:0.85;
}

/* cột thao tác nhỏ lại */
.gv-action{
    width:80px;
    text-align:center;
}
</style>

<div class="container mt-4">

    <h3 class="page-title">
        <i class="fa-solid fa-list"></i> Quản lý danh mục chi tiêu
    </h3>

    <!-- FORM -->
    <div class="card p-4 mb-4">

        <div class="row g-3">

            <div class="col-md-4">
                <label>Tên danh mục</label>
                <asp:TextBox ID="txtTenDanhMuc"
                    runat="server"
                    CssClass="form-control"
                    placeholder="Nhập tên danh mục"/>
            </div>

            <div class="col-md-6">
                <label>Mô tả</label>
                <asp:TextBox ID="txtMoTa"
                    runat="server"
                    CssClass="form-control"
                    placeholder="Nhập mô tả"/>
            </div>

            <div class="col-md-2 d-flex align-items-end">
                <asp:Button ID="btnThem"
                    runat="server"
                    Text="Thêm"
                    CssClass="btn btn-success w-100"
                    OnClick="btnThem_Click"/>
            </div>

        </div>

        <div class="mt-3 text-center">
            <asp:Label ID="lblThongBao"
                runat="server"
                CssClass="fw-bold"/>
        </div>

    </div>

    <!-- GRID -->
    <div class="card p-4">

        <h5 class="mb-3">Danh sách danh mục</h5>

        <asp:GridView
            ID="GridView1"
            runat="server"
            CssClass="table table-bordered table-hover"
            AutoGenerateColumns="false"
            EmptyDataText="Chưa có danh mục nào"
            DataKeyNames="MaDanhMuc"
            OnRowEditing="GridView1_RowEditing"
            OnRowUpdating="GridView1_RowUpdating"
            OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowDeleting="GridView1_RowDeleting">

            <Columns>

                <asp:BoundField DataField="MaDanhMuc" HeaderText="ID" ReadOnly="true"/>

                <asp:TemplateField HeaderText="Tên danh mục">
                    <ItemTemplate>
                        <%# Eval("TenDanhMuc") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtTenEdit"
                            runat="server"
                            Text='<%# Bind("TenDanhMuc") %>'
                            CssClass="form-control"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Mô tả">
                    <ItemTemplate>
                        <%# Eval("MoTa") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtMoTaEdit"
                            runat="server"
                            Text='<%# Bind("MoTa") %>'
                            CssClass="form-control"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Thao tác"
                    HeaderStyle-CssClass="gv-action"
                    ItemStyle-CssClass="gv-action">

                    <ItemTemplate>
                        <div class="action-group">

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
                        <div class="action-group">

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

                </asp:TemplateField>

            </Columns>

        </asp:GridView>

    </div>

</div>

</asp:Content>