using System;
using System.Data.SqlClient;
using System.Configuration;

namespace QuanLyChiTieuCaNhan
{
    public partial class DangNhap : System.Web.UI.Page
    {
        string conn = ConfigurationManager.ConnectionStrings["QLChiTieu"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                txtUser.Focus();
                lblThongBao.Text = "";
            }
        }

        protected void btnDangNhap_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text.Trim();
            string pass = txtPass.Text.Trim();

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                lblThongBao.Text = "⚠ Vui lòng nhập đầy đủ thông tin!";
                return;
            }

            if (user.Length > 50 || pass.Length > 50)
            {
                lblThongBao.Text = "⚠ Dữ liệu quá dài!";
                return;
            }

            try
            {
                // HASH password trước khi so sánh
                string hashedPass = SecurityHelper.HashPassword(pass);

                using (SqlConnection con = new SqlConnection(conn))
                {
                    string sql = @"SELECT MaNguoiDung, HoTen
                                   FROM NguoiDung
                                   WHERE TenDangNhap=@u AND MatKhau=@p";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@u", System.Data.SqlDbType.NVarChar, 50).Value = user;
                        cmd.Parameters.Add("@p", System.Data.SqlDbType.NVarChar, 64).Value = hashedPass;

                        con.Open();
                        SqlDataReader rd = cmd.ExecuteReader();

                        if (rd.Read())
                        {
                            Session["user"] = rd["MaNguoiDung"].ToString();
                            Session["name"] = rd["HoTen"].ToString();

                            Response.Redirect("Default.aspx");
                        }
                        else
                        {
                            lblThongBao.Text = "❌ Sai tài khoản hoặc mật khẩu!";
                            txtPass.Text = "";
                            txtPass.Focus();
                        }
                    }
                }
            }
            catch
            {
                lblThongBao.Text = "❌ Lỗi kết nối hệ thống!";
            }
        }
    }
}