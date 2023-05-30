using MaterialSkin.Controls;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using System.Threading;

namespace EmailList
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // создание ToolTip
            ToolTip toolTip1 = new ToolTip();

            toolTip1.BackColor = Color.LightBlue;
            toolTip1.ForeColor = Color.DarkBlue;
            toolTip1.UseFading = true;
            toolTip1.UseAnimation = true;
            toolTip1.IsBalloon = true;
            toolTip1.ShowAlways = true;


        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            Regiser form2 = new Regiser();
            form2.Show();
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {

            string exePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            string filePath = Path.Combine(exePath, "Logins.txt");
            if (File.Exists(filePath)) { }
            else
            {
                return;
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                int adminValue = 0;
                string[] values = line.Split(';');
                string data1 = values[0];
                string data2 = values[1];
                string data3 = values[2];

                if (data1 == textBox1.Text && data3 == textBox2.Text)
                {
                    string peredacha = textBox1.Text;

                    // Проверяем наличие слова "Admin" после последней ';'
                    if (values.Length > 3 && values[3].Trim() == "Admin")
                    {
                        adminValue = 1;
                    }

                    // Если значения совпадают, переходим на другую форму
                    Main form3 = new Main(peredacha, adminValue);
                    form3.Show();
                    this.Hide();
                    return;
                }
            }

            // Если не найдено совпадений, выводим сообщение об ошибке
            MessageBox.Show("Неверные данные!");

        }


        private void textBox1_KeyUp(object sender, KeyEventArgs e)

        {

        }



        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            toolTip1.AutoPopDelay = 5000;
            toolTip1.Show("от 6 до 32 символов", textBox1, 0, -25);
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            toolTip1.AutoPopDelay = 5000;
            toolTip1.Show("от 6 до 32 символов", textBox2, 0, -25);
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            toolTip1.AutoPopDelay = 5000;
            toolTip1.Show("от 6 до 32 символов", textBox2, 0, -25);
        }
    }
}