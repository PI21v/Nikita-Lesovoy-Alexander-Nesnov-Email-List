using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;
using MailKit.Search;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace EmailList
{
    public partial class TextParse : Form
    {
        string FILElog;
        string username1;
        public TextParse(string logFILE,string username)
        {
            InitializeComponent();
            // Создаем столбцы DataGridView
            DataGridViewTextBoxColumn emailColumn = new DataGridViewTextBoxColumn();
            emailColumn.HeaderText = "Email";
            emailColumn.Name = "EmailColumn";

            DataGridViewTextBoxColumn descriptionColumn = new DataGridViewTextBoxColumn();
            descriptionColumn.HeaderText = "Описание";
            descriptionColumn.Name = "DescriptionColumn";
            username1 = username;
            // Добавляем столбцы к DataGridView
            dataGridView1.Columns.Add(emailColumn);
            dataGridView1.Columns.Add(descriptionColumn);
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            FILElog = logFILE;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Создаем диалоговое окно открытия файла
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Открываем выбранный файл и считываем его содержимое
                string fileName = openFileDialog.FileName;
                string fileContent = File.ReadAllText(fileName);

                // Отображаем содержимое файла в RichTextBox
                richTextBox1.Text = fileContent;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            materialLabel2.Text = "0";
            int sc = 0;
            // Получаем текст из RichTextBox
            string text = richTextBox1.Text;

            // Используем регулярное выражение для поиска email адресов
            string pattern = @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b";
            MatchCollection matches = Regex.Matches(text, pattern);
            dataGridView1.Rows.Clear();
            // Подсвечиваем найденные email адреса цветом
            foreach (Match match in matches)
            {
                int startIndex = match.Index;
                int length = match.Length;
                richTextBox1.Select(startIndex, length);
                richTextBox1.SelectionBackColor = System.Drawing.Color.Yellow;
                richTextBox1.SelectionColor = System.Drawing.Color.Black;
                string email = match.Value;
                dataGridView1.Rows.Add(email, "");
                sc++;
            }
            materialLabel2.Text = sc.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                // Проверяем, что текущая строка не является последней пустой строкой
                if (!row.IsNewRow)
                {
                    string email = row.Cells[dataGridView1.Columns["EmailColumn"].Index].Value.ToString();
                    string opis = row.Cells[dataGridView1.Columns["DescriptionColumn"].Index].Value.ToString();
                    bool isValidEmail = Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

                    if (isValidEmail)
                    {
                        try
                        {
                            string exePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                            string filePath = Path.Combine(exePath, "tmp.txt");
                            string data = $"{email};{opis}";

                            File.WriteAllText(filePath, data);

                            MessageBox.Show("Данные успешно записаны", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            using (StreamWriter writer = new StreamWriter(FILElog, true))
                            {
                                // Записываем данные в файл
                                writer.WriteLine("Добавление почты из текста|" + email + ";" + opis + "|" + "для пользователя " + username1 + "|" + DateTime.Now);
                            }
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
    }
}
