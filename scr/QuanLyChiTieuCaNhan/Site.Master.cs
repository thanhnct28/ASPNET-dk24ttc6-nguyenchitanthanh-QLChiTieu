using System;

namespace QuanLyChiTieuCaNhan
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            string page = System.IO.Path.GetFileName(Request.Url.AbsolutePath);

            if (Session["user"] == null && page != "DangNhap.aspx")
            {
                Response.Redirect("~/DangNhap.aspx");
                return;
            }

            if (Session["name"] != null)
            {
                lblUser.Text = Server.HtmlEncode(Session["name"].ToString());
            }

            HighlightMenu(page);

        }

        void HighlightMenu(string page)
        {

            menuDashboard.Attributes.Remove("class");
            menuChiTieu.Attributes.Remove("class");
            menuThuNhap.Attributes.Remove("class");
            menuDanhMuc.Attributes.Remove("class");
            menuThongKe.Attributes.Remove("class");

            switch (page)
            {

                case "Default.aspx":
                    menuDashboard.Attributes["class"] = "active";
                    break;

                case "ChiTieu.aspx":
                    menuChiTieu.Attributes["class"] = "active";
                    break;

                case "ThuNhap.aspx":
                    menuThuNhap.Attributes["class"] = "active";
                    break;

                case "DanhMuc.aspx":
                    menuDanhMuc.Attributes["class"] = "active";
                    break;

                case "ThongKe.aspx":
                    menuThongKe.Attributes["class"] = "active";
                    break;

            }

        }

    }
}
