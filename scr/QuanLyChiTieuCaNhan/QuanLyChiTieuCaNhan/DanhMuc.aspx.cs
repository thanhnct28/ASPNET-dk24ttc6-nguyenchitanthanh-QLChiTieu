using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace QuanLyChiTieuCaNhan
{
    public partial class DanhMuc : System.Web.UI.Page
    {
        string conn = ConfigurationManager.ConnectionStrings["QLChiTieu"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("DangNhap.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadDanhMuc();
                lblThongBao.Text = "";
            }
        }

        void LoadDanhMuc()
        {
            int user = Convert.ToInt32(Session["user"]);

            using (SqlConnection con = new SqlConnection(conn))
            {
                string sql = @"SELECT MaDanhMuc, TenDanhMuc, MoTa
                               FROM DanhMucChi
                               WHERE MaNguoiDung=@user
                               ORDER BY TenDanhMuc";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@user", SqlDbType.Int).Value = user;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            string ten = txtTenDanhMuc.Text.Trim();
            string mota = txtMoTa.Text.Trim();

            if (string.IsNullOrWhiteSpace(ten))
            {
                lblThongBao.Text = "⚠ Vui lòng nhập tên danh mục!";
                return;
            }

            int user = Convert.ToInt32(Session["user"]);

            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(
                        @"INSERT INTO DanhMucChi(TenDanhMuc, MoTa, MaNguoiDung)
                          VALUES(@ten, @mota, @user)", con))
                    {
                        cmd.Parameters.Add("@ten", SqlDbType.NVarChar).Value = ten;
                        cmd.Parameters.Add("@mota", SqlDbType.NVarChar).Value = mota;
                        cmd.Parameters.Add("@user", SqlDbType.Int).Value = user;

                        cmd.ExecuteNonQuery();
                    }
                }

                lblThongBao.Text = "✅ Thêm thành công!";
                txtTenDanhMuc.Text = "";
                txtMoTa.Text = "";

                LoadDanhMuc();
            }
            catch
            {
                lblThongBao.Text = "❌ Lỗi khi thêm!";
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            LoadDanhMuc();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            LoadDanhMuc();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

            TextBox txtTen = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtTenEdit");
            TextBox txtMoTa = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtMoTaEdit");

            string ten = txtTen.Text.Trim();
            string mota = txtMoTa.Text.Trim();

            int user = Convert.ToInt32(Session["user"]);

            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string sql = @"UPDATE DanhMucChi
                                   SET TenDanhMuc=@ten, MoTa=@mota
                                   WHERE MaDanhMuc=@id AND MaNguoiDung=@user";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        cmd.Parameters.Add("@ten", SqlDbType.NVarChar).Value = ten;
                        cmd.Parameters.Add("@mota", SqlDbType.NVarChar).Value = mota;
                        cmd.Parameters.Add("@user", SqlDbType.Int).Value = user;

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                lblThongBao.Text = "✅ Cập nhật thành công!";
                GridView1.EditIndex = -1;
                LoadDanhMuc();
            }
            catch
            {
                lblThongBao.Text = "❌ Lỗi khi cập nhật!";
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            int user = Convert.ToInt32(Session["user"]);

            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string sql = @"DELETE FROM DanhMucChi
                                   WHERE MaDanhMuc=@id AND MaNguoiDung=@user";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        cmd.Parameters.Add("@user", SqlDbType.Int).Value = user;

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                lblThongBao.Text = "✅ Xóa thành công!";
                LoadDanhMuc();
            }
            catch
            {
                lblThongBao.Text = "❌ Lỗi khi xóa!";
            }
        }
    }
}