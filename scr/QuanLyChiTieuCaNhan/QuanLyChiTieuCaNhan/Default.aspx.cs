using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace QuanLyChiTieuCaNhan
{
    public partial class Default : System.Web.UI.Page
    {

        string conn = ConfigurationManager.ConnectionStrings["QLChiTieu"].ConnectionString;

        public string jsonThu = "[0,0,0,0,0,0,0,0,0,0,0,0]";
        public string jsonChi = "[0,0,0,0,0,0,0,0,0,0,0,0]";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["user"] == null)
            {
                Response.Redirect("DangNhap.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadDashboard();
                LoadChart();
            }

        }

        void LoadDashboard()
        {

            int user = Convert.ToInt32(Session["user"]);

            using (SqlConnection con = new SqlConnection(conn))
            {

                con.Open();

                SqlCommand cmd = new SqlCommand(

                @"SELECT
(SELECT ISNULL(SUM(SoTien),0) FROM ThuNhap WHERE MaNguoiDung=@user),
(SELECT ISNULL(SUM(SoTien),0) FROM ChiTieu WHERE MaNguoiDung=@user)"

                , con);

                cmd.Parameters.AddWithValue("@user", user);

                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {

                    decimal thu = Convert.ToDecimal(rd[0]);
                    decimal chi = Convert.ToDecimal(rd[1]);

                    lblThu.Text = thu.ToString("N0") + " VND";
                    lblChi.Text = chi.ToString("N0") + " VND";
                    lblConLai.Text = (thu - chi).ToString("N0") + " VND";

                }

            }

            LoadChiTieuGanDay();

        }

        void LoadChiTieuGanDay()
        {

            int user = Convert.ToInt32(Session["user"]);

            using (SqlConnection con = new SqlConnection(conn))
            {

                string sql = @"

SELECT TOP 10
ROW_NUMBER() OVER(ORDER BY c.NgayChi DESC) STT,
d.TenDanhMuc,
c.SoTien,
c.NgayChi,
c.GhiChu
FROM ChiTieu c
LEFT JOIN DanhMucChi d
ON c.MaDanhMuc=d.MaDanhMuc
WHERE c.MaNguoiDung=@user
ORDER BY c.NgayChi DESC";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@user", user);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();

            }

        }

        void LoadChart()
        {

            int user = Convert.ToInt32(Session["user"]);

            List<decimal> thu = new List<decimal>();
            List<decimal> chi = new List<decimal>();

            using (SqlConnection con = new SqlConnection(conn))
            {

                con.Open();

                for (int i = 1; i <= 12; i++)
                {

                    SqlCommand cmd = new SqlCommand(

                    @"SELECT
ISNULL((SELECT SUM(SoTien)
FROM ThuNhap
WHERE MONTH(NgayThu)=@m
AND MaNguoiDung=@user),0),

ISNULL((SELECT SUM(SoTien)
FROM ChiTieu
WHERE MONTH(NgayChi)=@m
AND MaNguoiDung=@user),0)"

                    , con);

                    cmd.Parameters.AddWithValue("@m", i);
                    cmd.Parameters.AddWithValue("@user", user);

                    SqlDataReader rd = cmd.ExecuteReader();

                    if (rd.Read())
                    {
                        thu.Add(Convert.ToDecimal(rd[0]));
                        chi.Add(Convert.ToDecimal(rd[1]));
                    }

                    rd.Close();

                }

            }

            JavaScriptSerializer js = new JavaScriptSerializer();

            jsonThu = js.Serialize(thu);
            jsonChi = js.Serialize(chi);

        }

    }
}
