using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace QuanLyChiTieuCaNhan
{
    public partial class ThuNhap : System.Web.UI.Page
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
                txtNgayThu.Text = DateTime.Now.ToString("yyyy-MM-dd");
                LoadData();
                lblThongBao.Text = "";
            }
        }

        void LoadData()
        {
            int user = Convert.ToInt32(Session["user"]);

            using (SqlConnection con = new SqlConnection(conn))
            {
                string sql = @"SELECT MaThuNhap, SoTien, NgayThu, GhiChu
                               FROM ThuNhap
                               WHERE MaNguoiDung=@user
                               ORDER BY NgayThu DESC";

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

        protected void BtnThem_Click(object sender, EventArgs e)
        {
            int user = Convert.ToInt32(Session["user"]);

            string sotien = txtSoTien.Text.Replace(".", "").Replace(",", "");

            if (!decimal.TryParse(sotien, out decimal tien) || tien <= 0)
            {
                lblThongBao.Text = "❌ Số tiền không hợp lệ!";
                return;
            }

            if (!DateTime.TryParse(txtNgayThu.Text, out DateTime ngay))
            {
                lblThongBao.Text = "❌ Ngày không hợp lệ!";
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string sql = @"INSERT INTO ThuNhap
                    (MaNguoiDung,SoTien,NgayThu,GhiChu)
                    VALUES(@user,@tien,@ngay,@ghichu)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@user", SqlDbType.Int).Value = user;
                        cmd.Parameters.Add("@tien", SqlDbType.Decimal).Value = tien;
                        cmd.Parameters.Add("@ngay", SqlDbType.Date).Value = ngay;
                        cmd.Parameters.Add("@ghichu", SqlDbType.NVarChar, 255).Value = txtGhiChu.Text;

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                lblThongBao.Text = "✅ Thêm thu nhập thành công!";
                txtSoTien.Text = "";
                txtGhiChu.Text = "";

                LoadData();
            }
            catch
            {
                lblThongBao.Text = "❌ Lỗi khi thêm!";
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            LoadData();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            LoadData();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            GridViewRow row = GridView1.Rows[e.RowIndex];

            string sotien = ((TextBox)row.Cells[1].Controls[0]).Text.Replace(".", "").Replace(",", "");
            string ghichu = ((TextBox)row.Cells[3].Controls[0]).Text;

            if (!decimal.TryParse(sotien, out decimal tien) || tien <= 0)
            {
                lblThongBao.Text = "❌ Số tiền không hợp lệ!";
                return;
            }

            if (!DateTime.TryParse(((TextBox)row.Cells[2].Controls[0]).Text, out DateTime ngay))
            {
                lblThongBao.Text = "❌ Ngày không hợp lệ!";
                return;
            }

            int user = Convert.ToInt32(Session["user"]);

            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string sql = @"UPDATE ThuNhap
                                   SET SoTien=@tien, NgayThu=@ngay, GhiChu=@ghichu
                                   WHERE MaThuNhap=@id AND MaNguoiDung=@user";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        cmd.Parameters.Add("@user", SqlDbType.Int).Value = user;
                        cmd.Parameters.Add("@tien", SqlDbType.Decimal).Value = tien;
                        cmd.Parameters.Add("@ngay", SqlDbType.Date).Value = ngay;
                        cmd.Parameters.Add("@ghichu", SqlDbType.NVarChar, 255).Value = ghichu;

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                lblThongBao.Text = "✅ Cập nhật thành công!";
                GridView1.EditIndex = -1;
                LoadData();
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
                    string sql = "DELETE FROM ThuNhap WHERE MaThuNhap=@id AND MaNguoiDung=@user";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        cmd.Parameters.Add("@user", SqlDbType.Int).Value = user;

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                lblThongBao.Text = "✅ Xóa thành công!";
                LoadData();
            }
            catch
            {
                lblThongBao.Text = "❌ Lỗi khi xóa!";
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow &&
                (e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                TextBox txtTien = (TextBox)e.Row.Cells[1].Controls[0];
                txtTien.Attributes.Add("onkeyup", "formatMoney(this)");
            }
        }
    }
}