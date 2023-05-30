using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;

namespace EmailList
{
    public partial class Regiser : MaterialForm
    {
        public Regiser()
        {
            InitializeComponent();
        }

        private void Regiser_Load(object sender, EventArgs e)
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

        private bool IsValidEmail(string email)
        {
            // Регулярное выражение для проверки формата email-адреса
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            // Создаем объект Regex и проверяем email на соответствие регулярному выражению
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length < 6 || textBox1.Text.Length > 32)
                {
                    MessageBox.Show("Длина логина должна быть от 6 до 32 символов!");
                    return;
                }

                if (textBox3.Text.Length < 6 || textBox3.Text.Length > 32)
                {
                    MessageBox.Show("Длина пароля должна быть от 6 до 32 символов!");
                    return;
                }

                if (textBox2.Text.Length > 88)
                {
                    MessageBox.Show("Длина email должна быть не более 80 символов ");
                    return;
                }

                if (textBox3.Text != textBox4.Text)
                {
                    MessageBox.Show("Поле 'Пароль' и 'Ввод пароля' должны совпадать ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string email = textBox2.Text;

                if (!IsValidEmail(email))
                {
                    MessageBox.Show("Email-адрес не является допустимым!");
                    return;
                }

                string data1 = textBox1.Text;
                string data2 = textBox2.Text;
                string data3 = textBox4.Text;
                bool isChecked = materialCheckBox1.Checked;

                if (isChecked)
                {
                    string exePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                    string filePath = Path.Combine(exePath, "Logins.txt");

                    using (StreamWriter sw = File.AppendText(filePath))
                    {
                        sw.WriteLine($"{data1};{data2};{data3};Admin");
                    }

                    this.Close();
                    return;
                }

                string exePath1 = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                string filePath2 = Path.Combine(exePath1, "Logins.txt");

                using (StreamWriter sw = File.AppendText(filePath2))
                {
                    sw.WriteLine($"{data1};{data2};{data3}");
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            toolTip1.AutoPopDelay = 5000;
            toolTip1.Show("от 6 до 32 символов", textBox1, 0, -25);
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            toolTip1.AutoPopDelay = 5000;
            toolTip1.Show("Почта в формате: название_почты@сервис.домен, не более 88 символов", textBox2, 0, -25);
        }

        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {
            toolTip1.AutoPopDelay = 5000;
            toolTip1.Show("от 6 до 32 символов", textBox3, 0, -25);
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            toolTip1.AutoPopDelay = 5000;
            toolTip1.Show("от 6 до 32 символов", textBox4, 0, -25);
        }
    }
}
