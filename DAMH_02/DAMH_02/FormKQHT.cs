using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAMH_02
{
    public partial class FormKQHT : Form
    {
        //Thêm biến toàn cục để lưu mã sinh viên
        private string maSinhVien; // Biến lưu mã sinh viên

        public FormKQHT(string maSV)
        {
            InitializeComponent();
            maSinhVien = maSV;
            LoadKyHocList(); // Gọi hàm để tải danh sách kỳ học vào ListBox
        }

        private void LoadKyHocList()
        {
            // Giả sử bạn có các kỳ học là 1, 2, 3, ..., bạn có thể tùy chỉnh lại danh sách này theo yêu cầu
            listBoxKyHoc.Items.Add("Kỳ 1");
            listBoxKyHoc.Items.Add("Kỳ 2");
            listBoxKyHoc.Items.Add("Kỳ 3");
            // ...
        }
        //Viết hàm để lấy dữ liệu và hiển thị chart
        private void LoadKetQua(int kyHoc)
        {
            // Chuỗi kết nối tới SQL
            string connectionString = "Data Source=your_server;Initial Catalog=your_database;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            SELECT MonHoc, DiemThi, DiemTB
            FROM KetQuaHocTap
            WHERE MaSinhVien = @MaSinhVien AND KyHoc = @KyHoc";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
                command.Parameters.AddWithValue("@KyHoc", kyHoc);

                SqlDataReader reader = command.ExecuteReader();

                // Xóa dữ liệu cũ trên chart trước khi thêm mới
                chart1.Series["Điểm thi"].Points.Clear();
                chart1.Series["Điểm tb"].Points.Clear();

                int index = 1;
                while (reader.Read())
                {
                    // Đọc dữ liệu từ bảng và thêm vào chart
                    string tenMonHoc = reader["MonHoc"].ToString();
                    double diemThi = Convert.ToDouble(reader["DiemThi"]);
                    double diemTB = Convert.ToDouble(reader["DiemTB"]);

                    // Thêm dữ liệu vào chart
                    chart1.Series["Điểm thi"].Points.AddXY(index, diemThi);
                    chart1.Series["Điểm tb"].Points.AddXY(index, diemTB);

                    index++;
                }

                reader.Close();
            }
        }
        //Gọi hàm khi chọn kỳ học trong ListBox
        private void listBoxKyHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxKyHoc.SelectedItem != null)
            {
                // Lấy kỳ học từ listbox (ví dụ: "Kỳ 1" -> lấy ra số 1)
                int kyHoc = listBoxKyHoc.SelectedIndex + 1;
                LoadKetQua(kyHoc); // Gọi hàm để tải dữ liệu lên chart
            }
        }
        //Kết nối sự kiện cho ListBox
        //listBoxKyHoc.SelectedIndexChanged += listBoxKyHoc_SelectedIndexChanged;
        public FormKQHT()
        {
            InitializeComponent();
        }

        private void FormKQHT_Load(object sender, EventArgs e)
        {

        }
    }
}
