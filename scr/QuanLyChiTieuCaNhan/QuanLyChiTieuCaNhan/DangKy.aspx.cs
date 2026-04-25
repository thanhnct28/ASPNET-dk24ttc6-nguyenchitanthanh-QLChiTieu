using System;
using System.Data.SqlClient;
using System.Configuration;

namespace QuanLyChiTieuCaNhan
{
    public partial class DangKy : System.Web.UI.Page
    {
        string conn = ConfigurationManager.ConnectionStrings["QLChiTieu"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblThongBao.Text = "";
            }
        }

        protected void btnDangKy_Click(object sender, EventArgs e)
        {
            string hoten = txtHoTen.Text.Trim();
            string email = txtEmail.Text.Trim();
            string user = txtUser.Text.Trim();
            string pass = txtPass.Text.Trim();
            string confirm = txtConfirm.Text.Trim();

            if (string.IsNullOrWhiteSpace(hoten) ||
                string.IsNullOrWhiteSpace(user) ||
                string.IsNullOrWhiteSpace(pass))
            {
                lblThongBao.ForeColor = System.Drawing.Color.Red;
                lblThongBao.Text = "⚠ Vui lòng nhập đầy đủ thông tin!";
                return;
            }

            if (pass != confirm)
            {
                lblThongBao.ForeColor = System.Drawing.Color.Red;
                lblThongBao.Text = "⚠ Mật khẩu không khớp!";
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    con.Open();

                    // CHECK TRÙNG USER
                    string checkSql = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap=@u";
                    using (SqlCommand check = new SqlCommand(checkSql, con))
                    {
                        check.Parameters.Add("@u", System.Data.SqlDbType.NVarChar, 50).Value = user;

                        int exists = (int)check.ExecuteScalar();

                        if (exists > 0)
                        {
                            lblThongBao.ForeColor = System.Drawing.Color.Red;
                            lblThongBao.Text = "❌ Tên đăng nhập đã tồn tại!";
                            return;
                        }
                    }

                    // HASH PASSWORD
                    string hashedPass = SecurityHelper.HashPassword(pass);

                    // INSERT
                    string insertSql = @"INSERT INTO NguoiDung(TenDangNhap, MatKhau, HoTen, Email)
                                         VALUES(@u,@p,@h,@e)";

                    using (SqlCommand cmd = new SqlCommand(insertSql, con))
                    {
                        cmd.Parameters.Add("@u", System.Data.SqlDbType.NVarChar, 50).Value = user;
                        cmd.Parameters.Add("@p", System.Data.SqlDbType.NVarChar, 64).Value = hashedPass; // đổi 64 cho đủ SHA256
                        cmd.Parameters.Add("@h", System.Data.SqlDbType.NVarChar, 100).Value = hoten;
                        cmd.Parameters.Add("@e", System.Data.SqlDbType.NVarChar, 100).Value = email;

                        cmd.ExecuteNonQuery();
                    }
                }

                lblThongBao.ForeColor = System.Drawing.Color.LightGreen;
                lblThongBao.Text = "✅ Đăng ký thành công!";

                Response.AddHeader("REFRESH", "1;URL=DangNhap.aspx");
            }
            catch
            {
                lblThongBao.ForeColor = System.Drawing.Color.Red;
                lblThongBao.Text = "❌ Lỗi hệ thống!";
            }
        }
    }
}