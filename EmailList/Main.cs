using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ARSoft.Tools.Net;
using ARSoft.Tools.Net.Dns;
using System.Net.Sockets;
using Newtonsoft.Json;
using MailKit.Net.Smtp;
using RestSharp;
using System.Net.Sockets;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using MailKit.Net.Smtp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using MailKit.Search;

namespace EmailList
{

    public partial class Main : MaterialForm
    {
        private DataView dataView; // Объект DataView для фильтрации данных
        private string originalFilter; // Оригинальный фильтр DataView
        int adm = 0;
        private int editedColumnIndex = -1; // Индекс редактируемой колонки
        private string username;
        private System.Windows.Forms.Timer timer, timer1, timer2;
        DataTable dataTable = new DataTable();
        string logFILE;
        public Main(string peredacha, int ad)
        {
            InitializeComponent();
            if (ad == 1)
                adm = 1;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // интервал в миллисекундах
            timer.Tick += Timer_Tick;
            timer.Start();


            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 1000; // задаем интервал в миллисекундах
            timer1.Tick += Timer_Tick2;
            timer1.Start();

            // Инициализация и настройка таймера
            timer2 = new System.Windows.Forms.Timer();
            timer2.Interval = 1000; // Интервал в миллисекундах (1000 мс = 1 секунда)
            timer2.Tick += Timer_Tick3; // Обработчик события Tick таймера

            // Запуск таймера
            timer2.Start();




            username = peredacha;
            // Получаем путь к директории, где находится exe-файл
            string exePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

            // Создаем отдельный каталог для каждого пользователя, используя их имена
            string userDir = Path.Combine(exePath, peredacha);
            Directory.CreateDirectory(userDir);

            // Создаем новый файл для каждого пользователя, если его еще нет
            string fileName = Path.Combine(userDir, "data.txt");
            if (!File.Exists(fileName))
            {
                File.WriteAllText(fileName, "№\tРезультат\tОписание\tАдрес Почты\n\t\t\t\t");
            }

            // Загружаем данные из файла в DataGridView
            dataTable.Columns.Add("№", typeof(int)).ReadOnly = true;
            dataTable.Columns.Add("Результат");
            dataTable.Columns.Add("Описание", typeof(string)).ReadOnly = true;
            dataTable.Columns.Add("Адрес Почты", typeof(string)).ReadOnly = true;

            string[] lines = File.ReadAllLines(fileName);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split('\t');
                int number;
                if (int.TryParse(fields[0], out number))
                {
                    dataTable.Rows.Add(number, fields[1], fields[2], fields[3]);
                }
                else
                {

                }
            }

            dataGridView1.AllowUserToAddRows = false;
            if (ad == 0)
                pictureBox9.Visible = false;
            dataGridView1.DataSource = dataTable;

            string fileName1 = Path.Combine(userDir, "log.txt");
            if (!File.Exists(fileName1))
            {
                File.WriteAllText(fileName1, "");
            }
            DateTime currentTime = DateTime.Now;
            logFILE = fileName1;
            using (StreamWriter writer = new StreamWriter(fileName1, true))
            {
                // Записываем данные в файл
                writer.WriteLine("Начало работы для пользователя|" + username + "|" + currentTime);
            }

        }
        private void Timer_Tick3(object sender, EventArgs e)
        {
            int count = 0;

            // Перебираем все строки в DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Проверяем, если значение в первой ячейке строки не равно null или пустой строке,
                // то считаем эту строку как заполненную
                if (row.Cells[0].Value != null && !string.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                {
                    count++;
                }
            }

            // Обновляем значение в Label
            label2.Text = count.ToString();
        }



        private void Timer_Tick2(object sender, EventArgs e)
        {
            // Сохраняем изменения в файл
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("№\tРезультат\tОписание\tАдрес Почты");

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                object[] values = new object[row.Cells.Count];
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    values[i] = row.Cells[i].Value ?? "";
                }
                sb.AppendLine(string.Join("\t", values));
            }

            string exePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            string userDir = Path.Combine(exePath, username);
            string fileName = Path.Combine(userDir, "data.txt");
            File.WriteAllText(fileName, sb.ToString());
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            string fileName = Path.Combine(Application.StartupPath, "tmp.txt");
            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);
                if (lines.Length >= 1)
                {
                    string[] fields = lines[0].Split(';');
                    string opis = fields[1];
                    string email = fields[0];
                    bool isValidEmail = Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                    if (isValidEmail)
                    {
                        // Находим максимальный номер в таблице и увеличиваем на 1
                        int maxNumber = 0;
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            object value = row.Cells["№"].Value;
                            int number = value != DBNull.Value ? Convert.ToInt32(value) : 0;
                            if (number > maxNumber)
                            {
                                maxNumber = number;
                            }
                        }

                        int newNumber = maxNumber + 1;

                        // Вставляем новую запись
                        dataTable.Rows.Add(newNumber, "", opis, email);
                        DateTime currentTime = DateTime.Now;
                        using (StreamWriter writer = new StreamWriter(logFILE, true))
                        {
                            // Записываем данные в файл
                            writer.WriteLine("Добавление новой записи|" + username + "|" + email + ";" + opis + "|" + DateTime.Now);
                        }
                        // Удаляем файл
                        File.Delete(fileName);
                    }
                    else
                    {
                        MessageBox.Show("Неверный адрес почты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void Main_Load(object sender, EventArgs e)
        {

        }
        string fpath;
        private string adminFilePath;
        string ffpath = "";

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (adm == 1)
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Text Files (*.txt)|*.txt";
                openFileDialog1.Title = "Выберите файл данных";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog1.FileName;
                    string directoryPath = Path.GetDirectoryName(filePath); // Получаем путь к директории без имени файла
                    ffpath = directoryPath; // Копируем только путь к директории

                    // Очистить существующие данные в DataGridView
                    dataTable.Rows.Clear();

                    // Загрузить данные из файла в DataGridView
                    string[] lines = File.ReadAllLines(filePath);

                    // Пропустить первую строку с заголовками
                    for (int i = 1; i < lines.Length; i++)
                    {
                        string[] values = lines[i].Split('\t');
                        values[0] = i.ToString(); // Установка номера строки

                        dataTable.Rows.Add(values);
                    }

                    // Разрешить редактирование данных в DataGridView
                    dataGridView1.ReadOnly = false;
                    dataGridView1.DataSource = dataTable; // Обновление источника данных


                    using (StreamWriter writer = new StreamWriter(logFILE, true))
                    {
                        // Записываем данные в файл
                        writer.WriteLine("Загрузка файла у пользователя|" + username + "|" + DateTime.Now);
                    }
                    // Сохранить изменения в выбранный файл при необходимости
                    SaveDataToFile();
                }
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text Files (*.txt)|*.txt";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDialog.FileName;

                    // Читаем строки из выбранного файла
                    string[] lines = File.ReadAllLines(fileName);

                    // Пропустить первую строку с заголовками
                    for (int i = 1; i < lines.Length; i++)
                    {
                        // Разделяем строку на email и описание
                        string[] parts = lines[i].Split('\t');
                        if (parts.Length >= 2)
                        {
                            string email = parts[3];
                            string description = parts[2];

                            // Добавляем новую строку в DataTable
                            DataRow newRow = dataTable.NewRow();
                            newRow["№"] = dataGridView1.Rows.Count + 1; // Номер строки
                            newRow["Результат"] = ""; // Пустое значение для Результата
                            newRow["Описание"] = description;
                            newRow["Адрес Почты"] = email;
                            dataTable.Rows.Add(newRow);
                        }
                    }

                    // Обновляем источник данных DataGridView
                    dataGridView1.DataSource = dataTable;

                    using (StreamWriter writer = new StreamWriter(logFILE, true))
                    {
                        // Записываем данные в файл
                        writer.WriteLine("Загрузка файла у пользователя|" + username + "|" + DateTime.Now);
                    }
                    // Сохраняем изменения в файл data.txt
                    SaveDataToFile();
                }
            }
        }
        private void SaveDataToFile()
        {
            // Получаем путь к директории, где находится exe-файл
            string exePath = Path.GetDirectoryName(Application.ExecutablePath);

            // Создаем отдельный каталог для каждого пользователя, используя их имена
            string userDir = Path.Combine(exePath, username);
            Directory.CreateDirectory(userDir);

            // Создаем новый файл для каждого пользователя, если его еще нет
            string fileName = Path.Combine(userDir, "data.txt");
            if (!File.Exists(fileName))
            {
                File.WriteAllText(fileName, "№\tРезультат\tОписание\tАдрес Почты\n1\t\t\t\t");
            }

            // Сохраняем данные из DataGridView в файл
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("№\tРезультат\tОписание\tАдрес Почты");

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                object[] values = new object[row.Cells.Count];
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    values[i] = row.Cells[i].Value ?? "";
                }
                sb.AppendLine(string.Join("\t", values));
            }

            File.WriteAllText(fileName, sb.ToString());
        }




        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (adm == 1)
            {
                if (ffpath == "")
                    return;
                // Открываем диалоговое окно для выбора файла сохранения
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Текстовый файл (*.txt)|*.txt";
                saveFileDialog.FileName = ffpath + "\\data.txt";

                // Открываем файл для записи
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    // Записываем заголовки столбцов
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        writer.Write(dataGridView1.Columns[i].HeaderText);
                        if (i < dataGridView1.Columns.Count - 1)
                        {
                            writer.Write("\t");
                        }
                    }
                    writer.WriteLine();

                    // Находим индекс столбца "Результат"
                    int resultColumnIndex = -1;
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        if (dataGridView1.Columns[i].HeaderText == "Результат")
                        {
                            resultColumnIndex = i;
                            break;
                        }
                    }

                    // Записываем данные строк
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        for (int i = 0; i < row.Cells.Count; i++)
                        {
                            object value = row.Cells[i].Value ?? "";
                            if (i == resultColumnIndex && string.IsNullOrEmpty(value.ToString()))
                            {
                                value = "не определено";
                            }
                            writer.Write(value.ToString());
                            if (i < row.Cells.Count - 1)
                            {
                                writer.Write("\t");
                            }
                        }
                        writer.WriteLine();
                    }
                }


                using (StreamWriter writer = new StreamWriter(logFILE, true))
                {
                    // Записываем данные в файл
                    writer.WriteLine("Сохранение для пользователя |" + username + "|" + DateTime.Now);
                }


            }
            else
            {
                // Открываем диалоговое окно для выбора файла сохранения
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Текстовый файл (*.txt)|*.txt";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Открываем файл для записи
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        // Записываем заголовки столбцов
                        for (int i = 0; i < dataGridView1.Columns.Count; i++)
                        {
                            writer.Write(dataGridView1.Columns[i].HeaderText);
                            if (i < dataGridView1.Columns.Count - 1)
                            {
                                writer.Write("\t");
                            }
                        }
                        writer.WriteLine();

                        // Находим индекс столбца "Результат"
                        int resultColumnIndex = -1;
                        for (int i = 0; i < dataGridView1.Columns.Count; i++)
                        {
                            if (dataGridView1.Columns[i].HeaderText == "Результат")
                            {
                                resultColumnIndex = i;
                                break;
                            }
                        }

                        // Записываем данные строк
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            for (int i = 0; i < row.Cells.Count; i++)
                            {
                                object value = row.Cells[i].Value ?? "";
                                if (i == resultColumnIndex && string.IsNullOrEmpty(value.ToString()))
                                {
                                    value = "не определено";
                                }
                                writer.Write(value.ToString());
                                if (i < row.Cells.Count - 1)
                                {
                                    writer.Write("\t");
                                }
                            }
                            writer.WriteLine();
                        }
                    }
                }
            }


            using (StreamWriter writer = new StreamWriter(logFILE, true))
            {
                // Записываем данные в файл
                writer.WriteLine("Сохранение для пользователя |" + username + "|" + DateTime.Now);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            DataGridViewCell emailCell = selectedRow.Cells["Адрес Почты"];
            string email = emailCell.Value.ToString();
            try
            {
                string domainName = email.Split('@')[1];
            }
            catch (System.IndexOutOfRangeException)
            {

                MessageBox.Show("Произошла ошибка: ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Random random = new Random();

                // Генерируем случайное число 0 или 1
                int randomValue = random.Next(2);

                if (randomValue == 0)
                {
                    // Симулируем случай, когда адрес существует
                    emailCell.OwningRow.Cells["Результат"].Value = "Существует";
                }
                else
                {
                    // Симулируем случай, когда адрес не существует
                    emailCell.OwningRow.Cells["Результат"].Value = "Не существует";
                }


                using (StreamWriter writer = new StreamWriter(logFILE, true))
                {
                    // Записываем данные в файл
                    writer.WriteLine("Проверка email|" + email + "| для пользователя" + username + "|" + DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ADD form3 = new ADD();
            form3.Show();




        }

 
        private void RefreshRowNumbers()
        {
            dataTable.Columns["№"].ReadOnly = false; // Разрешить редактирование столбца "№"

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                dataTable.Rows[i]["№"] = i + 1;
            }

            dataTable.Columns["№"].ReadOnly = true; // Запретить редактирование столбца "№"
        }

        private void SaveDataToFile2()
        {
            // Получаем путь к директории, где находится exe-файл
            string exePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

            // Создаем отдельный каталог для каждого пользователя, используя их имена
            string userDir = Path.Combine(exePath, username);
            Directory.CreateDirectory(userDir);

            // Создаем новый файл для каждого пользователя, если его еще нет
            string fileName = Path.Combine(userDir, "data.txt");

            // Сохраняем изменения в файл
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("№\tРезультат\tОписание\tАдрес Почты");

            foreach (DataRow row in dataTable.Rows)
            {
                object[] values = row.ItemArray;
                sb.AppendLine(string.Join("\t", values));
            }

            File.WriteAllText(fileName, sb.ToString());
        }







 

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataView != null)
                {
                    dataView.RowFilter = originalFilter;
                }
                else
                {
                    MessageBox.Show("Для возвращения нужно воспользоваться поиском", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }


        public void AddRowToGrid(string description, string email)
        {
            // получаем данные из DataGridView
            var dataTable = (DataTable)dataGridView1.DataSource;

            // находим максимальный номер в первом столбце
            int maxNumber = dataTable.AsEnumerable().Max(row => row.Field<int>("№"));

            // создаем новую строку
            DataRow newRow = dataTable.NewRow();
            newRow[0] = maxNumber + 1;
            newRow[1] = "";
            newRow[2] = description;
            newRow[3] = email;

            // добавляем строку в DataTable
            dataTable.Rows.Add(newRow);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {





        }

       

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            if (adm == 1 && !string.IsNullOrEmpty(ffpath))
            {
                // Заменить локальный файл data.txt выбранным файлом
                DialogResult result = MessageBox.Show("Вы хотите заменить локальный файл data.txt выбранным файлом?", "Замена файла", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string localFilePath = Path.Combine(ffpath, "data.txt");

                    try
                    {
                        File.Copy(adminFilePath, localFilePath, true);
                        MessageBox.Show("Файл успешно заменен.", "Замена файла", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла ошибка при замене файла: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            TextParse TextParse = new TextParse(logFILE, username);
            TextParse.Show();
        }
    }
}
