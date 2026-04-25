using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace QuanLyChiTieuCaNhan
{
    public partial class ThongKe : System.Web.UI.Page
    {
        string conn = ConfigurationManager.ConnectionStrings["QLChiTieu"].ConnectionString;

        public string chartThu = "[0,0,0,0,0,0,0,0,0,0,0,0]";
        public string chartChi = "[0,0,0,0,0,0,0,0,0,0,0,0]";
        public string chartDanhMuc = "[]";
        public string chartTienDanhMuc = "[]";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("DangNhap.aspx");
                return;
            }

            if (!IsPostBack)
            {
                txtFrom.Text = DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd");
                txtTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
                LoadAll();
            }
        }

        void LoadAll()
        {
            LoadThongKe();
            LoadChart();
            LoadPieChart();
            LoadTopDanhMuc();
        }

        DateTime FromDate()
        {
            return DateTime.TryParse(txtFrom.Text, out DateTime d) ? d : DateTime.Now.AddMonths(-6);
        }

        DateTime ToDate()
        {
            return DateTime.TryParse(txtTo.Text, out DateTime d) ? d : DateTime.Now;
        }

        void LoadThongKe()
        {
            int user = Convert.ToInt32(Session["user"]);

            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();

                string sql = @"SELECT
                (SELECT ISNULL(SUM(SoTien),0) FROM ThuNhap WHERE MaNguoiDung=@user AND NgayThu BETWEEN @from AND @to),
                (SELECT ISNULL(SUM(SoTien),0) FROM ChiTieu WHERE MaNguoiDung=@user AND NgayChi BETWEEN @from AND @to)";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@user", user);
                    cmd.Parameters.AddWithValue("@from", FromDate());
                    cmd.Parameters.AddWithValue("@to", ToDate());

                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        decimal thu = Convert.ToDecimal(rd[0]);
                        decimal chi = Convert.ToDecimal(rd[1]);

                        lblThu.Text = thu.ToString("N0") + " VND";
                        lblChi.Text = chi.ToString("N0") + " VND";
                        lblConLai.Text = (thu - chi).ToString("N0") + " VND";

                        lblTietKiem.Text = thu > 0
                            ? (((thu - chi) / thu) * 100).ToString("0") + " %"
                            : "0 %";
                    }
                }
            }
        }

        void LoadChart()
        {
            int user = Convert.ToInt32(Session["user"]);

            StringBuilder thu = new StringBuilder();
            StringBuilder chi = new StringBuilder();

            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();

                for (int i = 1; i <= 12; i++)
                {
                    string sql = @"SELECT
                    ISNULL((SELECT SUM(SoTien) FROM ThuNhap WHERE MaNguoiDung=@user AND MONTH(NgayThu)=@m),0),
                    ISNULL((SELECT SUM(SoTien) FROM ChiTieu WHERE MaNguoiDung=@user AND MONTH(NgayChi)=@m),0)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@user", user);
                        cmd.Parameters.AddWithValue("@m", i);

                        var rd = cmd.ExecuteReader();
                        if (rd.Read())
                        {
                            thu.Append(rd[0] + ",");
                            chi.Append(rd[1] + ",");
                        }
                        rd.Close();
                    }
                }
            }

            chartThu = "[" + thu.ToString().TrimEnd(',') + "]";
            chartChi = "[" + chi.ToString().TrimEnd(',') + "]";
        }

        void LoadPieChart()
        {
            int user = Convert.ToInt32(Session["user"]);

            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();

                string sql = @"SELECT TOP 5 TenDanhMuc, SUM(SoTien) TongTien
                               FROM ChiTieu ct
                               JOIN DanhMucChi dm ON ct.MaDanhMuc=dm.MaDanhMuc
                               WHERE ct.MaNguoiDung=@user
                               GROUP BY TenDanhMuc
                               ORDER BY TongTien DESC";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@user", user);

                    var rd = cmd.ExecuteReader();

                    StringBuilder label = new StringBuilder();
                    StringBuilder data = new StringBuilder();

                    while (rd.Read())
                    {
                        label.Append("'" + rd["TenDanhMuc"] + "',");
                        data.Append(rd["TongTien"] + ",");
                    }

                    chartDanhMuc = "[" + label.ToString().TrimEnd(',') + "]";
                    chartTienDanhMuc = "[" + data.ToString().TrimEnd(',') + "]";
                }
            }
        }

        void LoadTopDanhMuc()
        {
            int user = Convert.ToInt32(Session["user"]);

            using (SqlConnection con = new SqlConnection(conn))
            {
                string sql = @"SELECT TOP 5 TenDanhMuc, SUM(SoTien) TongTien
                               FROM ChiTieu ct
                               JOIN DanhMucChi dm ON ct.MaDanhMuc=dm.MaDanhMuc
                               WHERE ct.MaNguoiDung=@user
                               GROUP BY TenDanhMuc
                               ORDER BY TongTien DESC";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    da.SelectCommand.Parameters.AddWithValue("@user", user);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gridTop.DataSource = dt;
                    gridTop.DataBind();
                }
            }
        }

        protected void BtnFilter_Click(object sender, EventArgs e)
        {
            LoadAll();
        }

        // XUẤT EXCEL
        protected void BtnExcel_Click(object sender, EventArgs e)
        {
            int user = Convert.ToInt32(Session["user"]);

            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();

                string sql = @"
        SELECT NgayThu AS Ngay, N'Thu nhập' AS Loai, SoTien, GhiChu, '' AS DanhMuc
        FROM ThuNhap
        WHERE MaNguoiDung=@user AND NgayThu BETWEEN @from AND @to

        UNION ALL

        SELECT NgayChi AS Ngay, N'Chi tiêu' AS Loai, SoTien, GhiChu, TenDanhMuc
        FROM ChiTieu ct
        JOIN DanhMucChi dm ON ct.MaDanhMuc = dm.MaDanhMuc
        WHERE ct.MaNguoiDung=@user AND NgayChi BETWEEN @from AND @to

        ORDER BY Ngay DESC";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    da.SelectCommand.Parameters.AddWithValue("@user", user);
                    da.SelectCommand.Parameters.AddWithValue("@from", FromDate());
                    da.SelectCommand.Parameters.AddWithValue("@to", ToDate());

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // dùng .xls
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=ThongKe.xls");
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                    Response.Charset = "utf-8";

                    Response.Write("<meta http-equiv='Content-Type' content='text/html; charset=utf-8'/>");

                    Response.Write("<table border='1' style='border-collapse:collapse;'>");

                    // HEADER
                    Response.Write("<tr style='background:#2c3e50;color:white;font-weight:bold;'>");
                    Response.Write("<th>Ngày</th>");
                    Response.Write("<th>Loại</th>");
                    Response.Write("<th>Danh mục</th>");
                    Response.Write("<th>Số tiền</th>");
                    Response.Write("<th>Ghi chú</th>");
                    Response.Write("</tr>");

                    // DATA
                    foreach (DataRow row in dt.Rows)
                    {
                        string loai = row["Loai"].ToString();

                        Response.Write("<tr>");

                        Response.Write("<td>" + Convert.ToDateTime(row["Ngay"]).ToString("dd/MM/yyyy") + "</td>");

                        if (loai == "Thu nhập")
                            Response.Write("<td style='color:green;font-weight:bold'>" + loai + "</td>");
                        else
                            Response.Write("<td style='color:red;font-weight:bold'>" + loai + "</td>");

                        Response.Write("<td>" + row["DanhMuc"] + "</td>");
                        Response.Write("<td>" + Convert.ToDecimal(row["SoTien"]).ToString("N0") + "</td>");
                        Response.Write("<td>" + row["GhiChu"] + "</td>");

                        Response.Write("</tr>");
                    }

                    Response.Write("</table>");

                    Response.Flush();
                    Response.End();
                }
            }
        }
    }
}