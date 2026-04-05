using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace QuanLyChiTieuCaNhan
{
    public partial class ChiTieu : System.Web.UI.Page
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
                txtNgayChi.Text = DateTime.Now.ToString("yyyy-MM-dd");
                LoadDanhMuc();
                LoadData();
            }
        }

        void LoadDanhMuc()
        {
            int user = Convert.ToInt32(Session["user"]);

            using (SqlConnection con = new SqlConnection(conn))
            {
                string sql = "SELECT MaDanhMuc, TenDanhMuc FROM DanhMucChi WHERE MaNguoiDung=@user";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@user", SqlDbType.Int).Value = user;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    ddlDanhMuc.DataSource = dt;
                    ddlDanhMuc.DataTextField = "TenDanhMuc";
                    ddlDanhMuc.DataValueField = "MaDanhMuc";
                    ddlDanhMuc.DataBind();
                }
            }
        }

        void LoadData()
        {
            int user = Convert.ToInt32(Session["user"]);

            using (SqlConnection con = new SqlConnection(conn))
            {
                string sql = @"SELECT c.MaChiTieu, d.TenDanhMuc, c.SoTien, c.NgayChi, c.GhiChu
                               FROM ChiTieu c
                               JOIN DanhMucChi d ON c.MaDanhMuc=d.MaDanhMuc
                               WHERE c.MaNguoiDung=@user
                               ORDER BY c.NgayChi DESC";

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

            string tien = txtSoTien.Text.Replace(".", "");

            if (!decimal.TryParse(tien, out decimal soTien) || soTien <= 0)
            {
                lblThongBao.Text = "❌ Số tiền không hợp lệ!";
                return;
            }

            if (!DateTime.TryParse(txtNgayChi.Text, out DateTime ngay))
            {
                lblThongBao.Text = "❌ Ngày không hợp lệ!";
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string sql = @"INSERT INTO ChiTieu
                    (MaNguoiDung,MaDanhMuc,SoTien,NgayChi,GhiChu)
                    VALUES(@user,@dm,@tien,@ngay,@ghichu)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@user", SqlDbType.Int).Value = user;
                        cmd.Parameters.Add("@dm", SqlDbType.Int).Value = ddlDanhMuc.SelectedValue;
                        cmd.Parameters.Add("@tien", SqlDbType.Decimal).Value = soTien;
                        cmd.Parameters.Add("@ngay", SqlDbType.Date).Value = ngay;
                        cmd.Parameters.Add("@ghichu", SqlDbType.NVarChar).Value = txtGhiChu.Text;

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                lblThongBao.Text = "✅ Thêm thành công!";
                txtSoTien.Text = "";
                txtGhiChu.Text = "";
                LoadData();
            }
            catch
            {
                lblThongBao.Text = "❌ Lỗi!";
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

            string sotien = ((TextBox)row.Cells[2].Controls[0]).Text.Replace(".", "");

            if (!decimal.TryParse(sotien, out decimal tien) || tien <= 0)
            {
                lblThongBao.Text = "❌ Số tiền không hợp lệ!";
                return;
            }

            DateTime ngay = Convert.ToDateTime(((TextBox)row.Cells[3].Controls[0]).Text);
            string ghichu = ((TextBox)row.Cells[4].Controls[0]).Text;

            int user = Convert.ToInt32(Session["user"]);

            using (SqlConnection con = new SqlConnection(conn))
            {
                string sql = @"UPDATE ChiTieu
                               SET SoTien=@tien, NgayChi=@ngay, GhiChu=@ghichu
                               WHERE MaChiTieu=@id AND MaNguoiDung=@user";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@user", SqlDbType.Int).Value = user;
                    cmd.Parameters.Add("@tien", SqlDbType.Decimal).Value = tien;
                    cmd.Parameters.Add("@ngay", SqlDbType.Date).Value = ngay;
                    cmd.Parameters.Add("@ghichu", SqlDbType.NVarChar).Value = ghichu;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            GridView1.EditIndex = -1;
            LoadData();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            int user = Convert.ToInt32(Session["user"]);

            using (SqlConnection con = new SqlConnection(conn))
            {
                string sql = "DELETE FROM ChiTieu WHERE MaChiTieu=@id AND MaNguoiDung=@user";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@user", SqlDbType.Int).Value = user;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            LoadData();
        }
    }
}