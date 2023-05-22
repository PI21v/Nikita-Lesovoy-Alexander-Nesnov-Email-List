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

namespace EmailList
{
    public partial class ADD : Form
    {
        public ADD()
        {
            InitializeComponent();
        }

        private void materialLabel2_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string opis = textBox2.Text;
            bool isValidEmail = Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            if (isValidEmail)
            {
                try
                {
                    string exePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

                    // Создаем путь к файлу tmp.txt в директории с exe-файлом
                    string filePath = Path.Combine(exePath, "tmp.txt");

                    // Записываем данные в файл, разделяя их символом ";"
                    string data = $"{email};{opis}";
                    File.WriteAllText(filePath, data);

                    MessageBox.Show("Данные успешно записаны в файл.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при записи данных в файл: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Неверный адрес почты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
