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
    public partial class FormLSHT : Form
    {
        private string maSinhVien; // Biến lưu mã sinh viên

        // Hàm khởi tạo nhận mã sinh viên
        public FormLSHT(string maSV)
        {
            InitializeComponent();
            maSinhVien = maSV;
            LoadLichSuHocTap(); // Gọi hàm để tải dữ liệu lịch sử học tập
        }

        // Hàm để tải dữ liệu lịch sử học tập
        private void LoadLichSuHocTap()
        {
            try
            {
                string connectionString = @"Data Source=LAPTOP-8MJ97B04;Initial Catalog=DAMH03;Integrated Security=True";
                string query = "SELECT KyHoc, DiemTrungBinhHocKy, DiemTichLuy FROM LichSuHocTap WHERE MaSV = @MaSV";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.SelectCommand.Parameters.AddWithValue("@MaSV", maSinhVien);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;

                    // Tùy chỉnh tiêu đề các cột
                    dataGridView1.Columns["KyHoc"].HeaderText = "Kỳ Học";
                    dataGridView1.Columns["DiemTrungBinhHocKy"].HeaderText = "Điểm Trung Bình Học Kỳ";
                    dataGridView1.Columns["DiemTichLuy"].HeaderText = "Điểm Tích Lũy";

                    // Tự động điều chỉnh kích thước cột
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // Căn chỉnh dữ liệu giữa các cột
                    dataGridView1.Columns["KyHoc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView1.Columns["DiemTrungBinhHocKy"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView1.Columns["DiemTichLuy"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // Điều chỉnh chiều cao hàng
                    dataGridView1.RowTemplate.Height = 30;

                    // Ngăn người dùng thêm hàng mới
                    dataGridView1.AllowUserToAddRows = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tải dữ liệu: " + ex.Message);
            }
        }
        public FormLSHT()
        {
            InitializeComponent();
        }

        private void FormLSHT_Load(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Tạo đối tượng FormLogin và hiển thị
            FormLogin formLogin = new FormLogin();
            formLogin.Show();

            // Ẩn form hiện tại (FormLSHT)
            this.Hide();
        }

        private void btnTTSV_Click(object sender, EventArgs e)
        {
            // Tạo đối tượng FormTTSV và truyền mã sinh viên vào constructor
            FormTTSV formTTSV = new FormTTSV(maSinhVien.ToString()); // Truyền mã sinh viên
            formTTSV.Show();

            // Ẩn form hiện tại (FormLSHT)
            this.Hide();
        }

        private void btnKQHT_Click(object sender, EventArgs e)
        {
            // Tạo đối tượng FormKQHT và hiển thị
            FormKQHT formKQHT = new FormKQHT(maSinhVien.ToString());
            formKQHT.Show();

            // Ẩn form hiện tại (FormLSHT)
            this.Hide();
        }
    }
}
