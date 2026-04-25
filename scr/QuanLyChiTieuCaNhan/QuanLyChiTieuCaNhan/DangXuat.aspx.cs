using System;

namespace QuanLyChiTieuCaNhan
{
    public partial class DangXuat : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                // Xóa toàn bộ session
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();

                // Xóa toàn bộ cookie
                foreach (string cookie in Request.Cookies.AllKeys)
                {
                    Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
                }

                // Redirect fallback nếu JS bị tắt
                Response.AddHeader("REFRESH", "3;URL=DangNhap.aspx");

            }

        }

    }
}
