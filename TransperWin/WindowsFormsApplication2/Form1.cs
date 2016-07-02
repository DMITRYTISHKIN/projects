using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace WindowsFormsApplication2
{

    public partial class Form1 : Form
    {
        private int[,] m; //Двумерный массив цен перевозок
        private int[] pot; //Массив потребителей
        private int[] pos; //Массив поставщиков
        private int[,] rez; //Двумерный массив результата распределения поставок методами (или оптимизированными методами) северо-западного угла или Лебедева
        private int PL; //
        private DateTime timeAfter;
        private DateTime timeBefore;
        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Метод открытия пункта меню "ввод с клавиатуры"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void сКлавиатурыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            северозападныйToolStripMenuItem.Enabled = false;
            лебедевToolStripMenuItem.Enabled = false;
            оптимизацияToolStripMenuItem.Enabled = false;
            labelInfo.Visible = false;
            dataGridViewRez.Visible = false;
            buttonCreateMatrix.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            labelN.Visible = true;
            labelM.Visible = true;
        }
        /// <summary>
        /// Проверка введенного значения в ячейку таблицы (матрицы)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int a;

            if (!Int32.TryParse((string)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, out a) || Convert.ToInt32((string)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) < 1)
            {
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
            }
        }
        /// <summary>
        ///  Метод открытия пункта меню "ввод из файла"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void изФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Открытие диалогово окна и попытка открытия файла и чтения его данных
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader sr = new StreamReader(openDialog.FileName);
                    var str = "";
                    var S = (char)sr.Read();
                    while (S != ';')
                    {
                        str += S;
                        S = (char)sr.Read();
                    }
                    var N = Convert.ToInt32(str);
                    str = "";
                    S = (char)sr.Read();
                    while (S != ';')
                    {
                        str += S;
                        S = (char)sr.Read();
                    }
                    var M = Convert.ToInt32(str);

                    textBox2.Text = Convert.ToString(N - 1);
                    textBox1.Text = Convert.ToString(M - 1);
                    dataGridView1.RowCount = N;
                    dataGridView1.ColumnCount = M;
                    styleTable();
                    for (int i = 0; i < N; i++)
                    {
                        sr.ReadLine();
                        for (int j = 0; j < M; j++)
                        {
                            str = "";
                            S = (char)sr.Read();
                            while (S != ';')
                            {
                                str += S;
                                S = (char)sr.Read();
                            }
                            dataGridView1.Rows[i].Cells[j].Value = str;
                        }

                    }
                    sr.Close();
                    северозападныйToolStripMenuItem.Enabled = false;
                    лебедевToolStripMenuItem.Enabled = false;
                    оптимизацияToolStripMenuItem.Enabled = false;
                    labelInfo.Visible = false;
                    buttonCreateMatrix.Visible = true;
                    buttonConfimDate.Visible = true;
                    textBox1.Visible = true;
                    textBox2.Visible = true;
                    labelN.Visible = true;
                    labelM.Visible = true;
                    dataGridViewRez.Visible = false;
                    dataGridView1.Visible = true;
                    clearButton.Visible = false;
                }
                catch
                {
                    MessageBox.Show("Ошибка при открытии файла!");
                }
            }
        }
        /// <summary>
        ///  Метод открытия пункта меню "вывод в файл"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void вФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Открытие диалогово окна и попытка создания файла и запись в него данных
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                string filePlace = saveDialog.FileName;
                var sw = new StreamWriter(filePlace);
                try
                {
                    sw.Write(dataGridView1.RowCount + ";");
                    sw.Write(dataGridView1.ColumnCount + ";");
                    sw.WriteLine();
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            sw.Write(dataGridView1.Rows[i].Cells[j].Value.ToString() + ";");
                        }
                        sw.WriteLine();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not write file. Original error: " + ex.Message);
                }
                sw.Close();
            }
        }
        /// <summary>
        /// Метод открытия окна "выход" с подтверждением
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Создание экземпляра окна выхода с подтверждением
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }
        /// <summary>
        /// Метод открытия окна "об авторах"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void обАвтореToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Лабораторна работа\n" +
                "По курсу\n <<Технология разработки программного обеспечения>>\n" +
                "на тему <<Разработка и оценка транспортных средств>>\n" +
                "Решение транспортной задачи линейного программирования методами:\n" +
                "- северо-западного угла\n" +
                "- Лебедева" +
                "и оптимизация ее методом квадратов.\n" +
                "Язык программирования С#. \n\n" +
                "Выполнил студент группы ДКО-131б:\n" +
                "Тишкин Д.С.            ДКО-13191\n"
                , "Об авторе");
        }
        /// <summary>
        /// Функция проверяющая и передающая введенные значения из таблицы для дальнешей обработки этих зачений
        /// </summary>
        /// <returns></returns>
        private bool vvod()
        {
            dataGridView1.ReadOnly = true;
            вФайлToolStripMenuItem.Enabled = false;
            labelInfo.Visible = false;
            //Проверка на пустые поля таблицы с выводом ошибки
            for (int i = 0; i < dataGridView1.RowCount; i++)
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value == null || Convert.ToString(dataGridView1.Rows[i].Cells[j].Value) == "")
                    {
                        labelInfo.Visible = true;
                        labelInfo.ForeColor = System.Drawing.Color.Red;
                        labelInfo.Text = Convert.ToString("Пустые поля!");
                        return true;
                    }
            m = new int[dataGridView1.RowCount - 1, dataGridView1.ColumnCount - 1]; //Задаем размер двемерного массива цен перевозок
            pot = new int[dataGridView1.ColumnCount - 1]; int sumpot = 0; //Задаем размер массива потребителей
            pos = new int[dataGridView1.RowCount - 1]; int sumpos = 0; //Задаем размер массива поставщиков
            rez = new int[dataGridView1.RowCount - 1, dataGridView1.ColumnCount - 1]; //Задаем размер массива распределения поставок
            //запись в двумерный массив цен перевозок и инициализация нулями массива rez (данный массив содержит распределение оптимальных перевозок)
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                for (int j = 0; j < dataGridView1.ColumnCount - 1; j++)
                {
                    m[i, j] = Convert.ToInt32(dataGridView1.Rows[i + 1].Cells[j + 1].Value);
                    rez[i, j] = 0;
                }
            //запись потребителей в массив
            for (int i = 0; i < dataGridView1.ColumnCount - 1; i++)
            {
                pot[i] = Convert.ToInt32(dataGridView1.Rows[0].Cells[i + 1].Value);
                sumpot = sumpot + pot[i];
            }
            //запись поставщиков в массив
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                pos[i] = Convert.ToInt32(dataGridView1.Rows[i + 1].Cells[0].Value);
                sumpos = sumpos + pos[i];
            }
            //проверка на сумму потребителей и поставщиков
            if (sumpot != sumpos)
            {
                labelInfo.Visible = true;
                labelInfo.ForeColor = System.Drawing.Color.Red;
                labelInfo.Text = Convert.ToString("Ошибка потребители и поставки не равны!");
                return true;
            }
            labelInfo.Visible = true;
            labelInfo.ForeColor = System.Drawing.Color.Green;
            labelInfo.Text = Convert.ToString("Данные приняты успешно.");
            северозападныйToolStripMenuItem.Enabled = true;
            вФайлToolStripMenuItem.Enabled = true;
            лебедевToolStripMenuItem.Enabled = true;
            return false;
        }
        /// <summary>
        /// Функция для вывода оптимального распределения поставок
        /// </summary>
        private void vivod()
        {
            //Создание таблицы с распределенными перевозками
            clearButton.Visible = false;
            dataGridViewRez.ClearSelection();
            dataGridViewRez.ColumnCount = dataGridView1.ColumnCount;
            dataGridViewRez.RowCount = (dataGridView1.RowCount * 2) - 1;
            //Задание ширины стобцов
            foreach (DataGridViewColumn i in dataGridViewRez.Columns)
                i.Width = 50;
            dataGridViewRez.Rows[0].Cells[0].Value = "i/j";
            //Заполнение ячеек распределеныя поставок, цен перевозок и потребителей
            for (int i = 0, p = 0; i < dataGridViewRez.RowCount - 1; i = i + 2, p++)
            {
                dataGridViewRez.Rows[i + 1].Cells[0].Value = pos[p];
                dataGridViewRez[0, i + 1].Style.BackColor = System.Drawing.Color.LightBlue;
                dataGridViewRez[0, i + 2].Style.BackColor = System.Drawing.Color.LightBlue;
                for (int j = 0; j < dataGridViewRez.ColumnCount - 1; j++)
                {
                    dataGridViewRez.Rows[i + 2].Cells[j + 1].Value = rez[p, j];
                    dataGridViewRez.Rows[i + 1].Cells[j + 1].Value = m[p, j];
                }
            }
            //Оформление таблицы
            for (int i = 1; i < dataGridViewRez.RowCount; i++)
                for (int j = 1; j < dataGridViewRez.ColumnCount; j++)
                {
                    if (Convert.ToInt32(dataGridViewRez.Rows[i].Cells[j].Value) == 0)
                        dataGridViewRez[j, i].Style.ForeColor = System.Drawing.Color.LightGray;
                    else dataGridViewRez[j, i].Style.ForeColor = System.Drawing.Color.Black;
                }
            //Заполнение ячеек таблицы потребителей
            for (int t = 1; t < dataGridViewRez.ColumnCount; t++)
            {
                dataGridViewRez.Rows[0].Cells[t].Value = pot[t - 1];
                dataGridViewRez[t, 0].Style.BackColor = System.Drawing.Color.LightBlue;
                bool colr = true;
                for (int p = 0; p < dataGridViewRez.RowCount - 1; p = p + 2)
                {
                    if (colr)
                    {
                        dataGridViewRez[t, p + 2].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                        dataGridViewRez[t, p + 1].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                        colr = false;
                    }
                    else
                    {
                        dataGridViewRez[t, p + 2].Style.BackColor = System.Drawing.Color.White;
                        dataGridViewRez[t, p + 1].Style.BackColor = System.Drawing.Color.White;
                        colr = true;
                    }
                }
            }
            dataGridViewRez.Visible = true;
            labelInfo.Visible = true;
            labelInfo.ForeColor = System.Drawing.Color.Black;
            //Вывод итоговой цены 
            long Ticks = timeAfter.Ticks - timeBefore.Ticks;
            labelInfo.Text = String.Format("Стоимость поставки: {0} | Время решения: {1}", PL, Ticks);
            buttonConfimDate.Visible = false;
            dataGridView1.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            labelN.Visible = false;
            labelM.Visible = false;
            buttonCreateMatrix.Visible = false;
        }
        /// <summary>
        /// Метод распределения поставок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void северозападныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vvod())
                return;
            timeBefore = DateTime.Now;
            int a = 0, b = 0; //Счетчики
            bool bow = true; //флаг
            int[] tempPot = new int[dataGridView1.ColumnCount - 1]; //Задание рамера массива для обработки потребителей
            int[] tempPos = new int[dataGridView1.RowCount - 1]; //Задание рамера массива для обработки поставщиков
            //Заполнение рамера массива для обработки поставщиков
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                tempPos[i] = pos[i];
            //Задание рамера массива для обработки потребителей
            for (int i = 0; i < dataGridView1.ColumnCount - 1; i++)
                tempPot[i] = pot[i];
            PL = 0;
            /*Цикл распределения поставок по методу северо-западного угла
             * выполняется до тех пор пока не дойдет до правой нижней ячейки таблицы
            */
            while (bow == true)
            {
                //если "потребители" равны "поставщикам" то считает цену поставки, записывает в PL и переходит на следущую ячейку по диагонали
                if (pot[a] == tempPos[b])
                {
                    PL = PL + (tempPot[a] * m[b, a]);
                    rez[b, a] = tempPot[a];
                    //проверка на конец таблицы
                    if (a == dataGridView1.ColumnCount - 2 && b == dataGridView1.RowCount - 2)
                        break;
                    a++; b++;
                }
                //если "поставщиков" больше "потребителей" то считает цену поставки необходимого кол-ва товара потребителям, записывает в PL и переходит к следущему потребителю
                if (tempPot[a] < tempPos[b])
                {

                    PL = PL + (tempPot[a] * m[b, a]);
                    tempPos[b] = tempPos[b] - tempPot[a];
                    rez[b, a] = tempPot[a];
                    //проверка на конец таблицы
                    if (a == dataGridView1.ColumnCount - 2 && b == dataGridView1.RowCount - 2)
                        break;
                    a++;
                }
                //если "поставщиков" меньше "потребителей" то считает цену поставки оставшегося кол-ва товара от поставщика, записывает в PL и переходит к следущему поставщику
                if (pot[a] > tempPos[b])
                {
                    PL = PL + (tempPos[b] * m[b, a]);
                    tempPot[a] = tempPot[a] - tempPos[b];
                    rez[b, a] = tempPos[b];
                    //проверка на конец таблицы
                    if (a == dataGridView1.ColumnCount - 2 && b == dataGridView1.RowCount - 2)
                        break;
                    b++;
                }
            }
            timeAfter = DateTime.Now;
            vivod();
            оптимизацияToolStripMenuItem.Enabled = true;
        }
        /// <summary>
        /// Метод распределения поставок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void лебедевToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vvod())
                return;
            timeBefore = DateTime.Now;
            bool bow = true;
            int[] tempPot = new int[dataGridView1.ColumnCount - 1];  //Задание рамера массива для обработки потребителей
            int[] tempPos = new int[dataGridView1.RowCount - 1];  //Задание рамера массива для обработки поставщиков
            double[] midByRows = new double[dataGridView1.RowCount - 1];  //Задание рамера массива для обработки средних значений по строкам
            double[] midByColums = new double[dataGridView1.ColumnCount - 1];  //Задание рамера массива для обработки средних значений по столбцам
            int sum, full = 0;
            double[,] mid = new double[dataGridView1.RowCount - 1, dataGridView1.ColumnCount - 1]; //Задание рамера двумерного массива для заполнения средними значениями

            //Считаем среднее по строкам
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                tempPos[i] = pos[i];
                full = full + pos[i];
                sum = 0;
                for (int j = 0; j < dataGridView1.ColumnCount - 1; j++)
                    sum = sum + m[i, j];
                midByRows[i] = Convert.ToDouble(sum) / Convert.ToDouble(dataGridView1.ColumnCount - 1);
            }
            //Считаем среднее по столбцам
            for (int i = 0; i < dataGridView1.ColumnCount - 1; i++)
            {
                tempPot[i] = pot[i];
                sum = 0;
                for (int j = 0; j < dataGridView1.RowCount - 1; j++)
                    sum = sum + m[j, i];
                midByColums[i] = Convert.ToDouble(sum) / Convert.ToDouble(dataGridView1.RowCount - 1);
            }
            //Заполняем матрицу средних значений
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount - 1; j++)
                {
                    mid[i, j] = (midByColums[j] + midByRows[i]) - Convert.ToDouble(m[i, j]);
                }
            }
            //метод
            double tempMid; //временная переменная для хранения последнего наибольшего среднего значения
            int xp, yp; //счетчики
            PL = 0; 
            while (bow)
            {
                //Ищем наибольшее среднее значение
                tempMid = mid[0, 0];
                xp = 0; yp = 0;
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount - 1; j++)
                        if (tempMid < mid[i, j] && (tempPot[j] + pos[i]) != 0)
                        {
                            tempMid = mid[i, j];
                            yp = i; xp = j;
                        }
                }
                mid[yp, xp] = 0;
                //Распределяем
                if (tempPot[xp] == tempPos[yp])
                {
                    PL = PL + (tempPot[xp] * m[yp, xp]);
                    rez[yp, xp] = tempPot[xp];
                    full = full - tempPot[xp];
                    tempPot[xp] = 0; tempPos[yp] = 0;
                }
                else if (tempPot[xp] < tempPos[yp])
                {
                    PL = PL + (tempPot[xp] * m[yp, xp]);
                    rez[yp, xp] = tempPot[xp];
                    tempPos[yp] = tempPos[yp] - tempPot[xp];
                    full = full - tempPot[xp];
                    tempPot[xp] = 0;
                }
                else if (tempPot[xp] > tempPos[yp])
                {
                    PL = PL + (tempPos[yp] * m[yp, xp]);
                    rez[yp, xp] = tempPos[yp];
                    tempPot[xp] = tempPot[xp] - tempPos[yp];
                    full = full - tempPos[yp];
                    tempPos[yp] = 0;
                }
                //если все поставки/потребители распределены выходим из цикла
                if (full == 0)
                {
                    bow = false;
                }
            }
            timeAfter = DateTime.Now;
            //выводим
            vivod();
            оптимизацияToolStripMenuItem.Enabled = true;
        }
        /// <summary>
        /// Оптимизация распределения поставок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void оптимизацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timeBefore = DateTime.Now;
            buttonConfimDate.Visible = false;
            bool opt = true;
            while (opt)
            {
                opt = false; //флажок, если неправильный квадрат найден то он становится true и цикл продолжнает искать неправильный квадрат, если квадрат не надйен то выход из цикла (матрица оптимизирована)
                //Цикл поиска распределенных поставок для одного угла квадрата
                for (int i = 0; i < dataGridView1.RowCount - 2; i++)
                    for (int j = 0; j < dataGridView1.ColumnCount - 2; j++)
                        if (rez[i, j] != 0)
                        {
                            //Цикл поиска другой распределенной поставки для другого угла
                            for (int t = i + 1; t < dataGridView1.RowCount - 1; t++)
                                for (int p = j + 1; p < dataGridView1.ColumnCount - 1; p++)
                                    if (rez[t, p] != 0 && (m[i, j] + m[t, p]) > (m[i, p] + m[t, j])) //если квадрат неправильный то оптимизация
                                    {
                                        if (rez[i, j] > rez[t, p])
                                        {
                                            rez[i, j] = rez[i, j] - rez[t, p];
                                            rez[i, p] = rez[i, p] + rez[t, p];
                                            rez[t, j] = rez[t, j] + rez[t, p];
                                            rez[t, p] = 0;
                                            opt = true;
                                        }
                                        else if (rez[i, j] < rez[t, p])
                                        {
                                            rez[t, p] = rez[t, p] - rez[i, j];
                                            rez[i, p] = rez[i, p] + rez[i, j];
                                            rez[t, j] = rez[t, j] + rez[i, j];
                                            rez[i, j] = 0;
                                            opt = true;
                                        }
                                        else if (rez[i, j] == rez[t, p])
                                        {
                                            rez[t, j] = rez[t, p];
                                            rez[i, p] = rez[t, p];
                                            rez[t, p] = 0;
                                            rez[i, j] = 0;
                                            opt = true;
                                        }
                                    }
                        }
            }

            //считаем оптимизированную таблицу
            PL = 0;
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount - 1; j++)
                    if (rez[i, j] != 0)
                        PL = PL + (m[i, j] * rez[i, j]);
            }
            timeAfter = DateTime.Now;
            vivod();
        }
        /// <summary>
        /// Кнопка создания матрицы с заданными размерами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCreateMatrix_Click(object sender, EventArgs e)
        {
            int x = 0;
            int y = 0;
            labelInfo.Visible = false;
            clearButton.Visible = true;
            северозападныйToolStripMenuItem.Enabled = false;
            лебедевToolStripMenuItem.Enabled = false;
            оптимизацияToolStripMenuItem.Enabled = false;
            вФайлToolStripMenuItem.Enabled = false;
            dataGridView1.ReadOnly = false;
            //Попытка создания матрицы с проверками
            try
            {
                if (!Int32.TryParse(textBox1.Text, out x) || x == 0 || !Int32.TryParse(textBox2.Text, out y) || y == 0)
                {
                    labelInfo.Visible = true;
                    labelInfo.ForeColor = System.Drawing.Color.Red;
                    labelInfo.Text = Convert.ToString("Некорректный размер матрицы!");
                }
                if (x > 100 || y > 100 || x<1 || y<1)
                {
                    labelInfo.Visible = true;
                    labelInfo.ForeColor = System.Drawing.Color.Red;
                    labelInfo.Text = Convert.ToString("Некорректный размер матрицы!");
                }
                else
                {
                    dataGridView1.Visible = true;
                    buttonConfimDate.Visible = true;
                    dataGridView1.ColumnCount = int.Parse(textBox1.Text) + 1;
                    dataGridView1.RowCount = int.Parse(textBox2.Text) + 1;
                    styleTable();
                }
            }
            catch
            {
                dataGridView1.Visible = false;
                buttonConfimDate.Visible = false;
                labelInfo.Visible = true;
                labelInfo.ForeColor = System.Drawing.Color.Red;
                labelInfo.Text = Convert.ToString("Некорректный размер матрицы!");
            }
        }
        /// <summary>
        /// Задание стиля таблицы
        /// </summary>
        private void styleTable()
        {
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
                dataGridView1.Columns[i].Width = 50;
            dataGridView1.Rows[0].DefaultCellStyle.BackColor = System.Drawing.Color.LightBlue;
            dataGridView1.Columns[0].DefaultCellStyle.BackColor = System.Drawing.Color.LightBlue;
            dataGridView1.Rows[0].Cells[0].Style.BackColor = System.Drawing.Color.SteelBlue;
            dataGridView1.Rows[0].Cells[0].Style.ForeColor = System.Drawing.Color.White;
            dataGridView1.Rows[0].Cells[0].ReadOnly = true;
            dataGridView1.Rows[0].Cells[0].Value = "i/j";
        }
        private void buttonConfimDate_Click(object sender, EventArgs e)
        {
            vvod();
        }

        private void оДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
    "На вход программе подается  M  пунктов " +
    "производства (поставщиков) и N пунктов " +
    "потребления (потребителей) однородного продукта.\n" +
    "Далее заполняются в ячейки матрицы величины:\n" +
    "a_i  - объем производства i-го поставщика,  i=1..m;\n" +
    "b_j  - объем потребления j-го потребителя, i=1..n;\n" +
    "c_ij- стоимость перевозки единицы продукта от i-го " +
    "поставщика к  j-му потребителю. \n\n" +
    "Ограничения на входную информацию:\n" +
    "1) Значения m и n должны быть в границах " +
    "от 1 до 100 (обязательно целые), не " +
    "содержащие иных, кроме цифр, символов;\n" +
    "2) Значения a_i,b_j,c_ij должны быть в границах " +
    "от 1 до 2147483647 (обязательно целые), не " +
    "содержащие иных, кроме цифр, символов;\n" +
    "3) Суммарный объем тавара, имеющийся у поставщиков " +
    "должен быть равен суммарному колличеству товара " +
    "необходимому потрубителю."
 , "О данных");
        }

        private void оМетодахРешенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
    "Метод квадратов.\n" +
    "Пусть получен некоторый первоначальный план поставок. " +
    "Назовем квадратом 4 клетки, стоящие в углах такого " +
    "прямоугольника, в котором хотя бы по одной диагонали стоят " +
    "2 положительные поставки. В оптимальной матрице не " +
    "существует неправильных квадратов, у которых сумма двух " +
    "стоимостей, стоящих в клетках с положительными поставками " +
    "по одной диагонали больше суммы стоимостей по другой " +
    "диагонали (соответственно, у правильных - наоборот). " +
    "Любой неправильный квадрат необходимо превратить в " +
    "правильный следующим образом: вычитаем из поставок, " +
    "стоящих по одной диагонали, соответствующих левой " +
    "части неравенства и прибавляем к точкам другой диагонали."
, "О методах");
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
    "ПС разработано на платформе Visual Studio 2013 и " +
    "в среде выполнения .NET Framework 4.5.\n" +
    "1. Количество строк кода – 1025;\n" +
    "2. количество модулей – 48;\n" +
    "3. количество обрабатываемых переменных – 78;\n" +
    "4. используются следующие типы данных – " +
    "   int, bool, double, int[,], " +
    "   OpenFileDialog, SaveFileDialog," +
    "   ComboBox, Button, DataGridView, " +
    "   string, DialogResult, MessageBoxButtons, " +
    "   object, EventArgs, Stream,  StreamWriter, Exception, " +
    "   DataGridViewCellEventArgs, DataGridViewCellPaintingEventArgs, " +
    "   SolidBrush, DataGridViewEditingControlShowingEventArgs, " +
    "   TextBox, Keys, Color;\n" +
    "5. длительность решения задачи – 0,007 с;\n" +
    "6. время отклика – 0,00012;\n" +
    "7. трудозатраты: изучение поставленной задачи – 5 час, " +
    "   проектирование – 10 часов, кодирование – 12 часов, " +
    "   тестирование – 25 часов, документирование – 20 часов, \n" +
    "8. количество участников разработки – 4.\n" +
    "9. итого: 216 часов.\n"
, "О Программе");
        }

        private void dataGridViewRez_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //Кнопка очистки заданный значений матрицы
        private void clearButton_Click(object sender, EventArgs e)
        {
            labelInfo.Visible = false;
            buttonConfimDate.Visible = false;
            dataGridView1.Rows.Clear();
            textBox1.Clear();
            textBox2.Clear();
        }
        //Кнопка вывода на экран исходной матрицы
        private void наЭкранToolStripMenuItem_Click(object sender, EventArgs e)
        {
       
            labelInfo.Visible = false;
            buttonCreateMatrix.Visible = false;
            buttonConfimDate.Visible = false;
            textBox1.Visible = true;
            textBox1.ReadOnly = true;
            textBox2.Visible = true;
            textBox2.ReadOnly = true;
            labelN.Visible = true;
            labelM.Visible = true;
            dataGridViewRez.Visible = false;
            dataGridView1.Visible = true;
            clearButton.Visible = false;
        }





    }
}
