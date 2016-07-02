using System;
using System.Windows.Forms;
using System.Linq;
namespace Calculator
{

    public partial class MainForm : Form
    {
        /// <summary>
        /// Первый аргумент - поле
        /// </summary>
        private double x;
        /// <summary>
        /// Второе аргумент - поле
        /// </summary>
        private double y;
        /// <summary>
        /// Признак ввода нового числа
        /// </summary>
        private bool flag;

        /// <summary>
        /// Текущая система счисления
        /// </summary>
        private int notation = 10;

        /// <summary>
        /// Режим записи десятичной части числа
        /// </summary>
        private bool DotMode;
        private double Dot;

        /// <summary>
        /// Переменная для хранения числа
        /// </summary>
        private double Memory;

        /// <summary>
        /// Определяем значение - цифра это или буква
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        private string NotationCharacter(double Value)
        {
            if (Value > 9)
                return ((NumberNotation)Value).ToString();
            else return Value.ToString();
        }
        /// <summary>
        /// Функция вывода числа в n-мерной системе счисления
        /// </summary>
        private void OutputConvertNotation()
        {
            if (notation != 10)
            {
                string notationIntegerPath = "";
                string natovationFractionalPath = "";
                bool znak = false;
                double currentValue = X;
                string[] digit = currentValue.ToString().Split(',');
                long integerPath = (long)currentValue;

                if (currentValue < 0)
                {
                    znak = true;
                    currentValue = Math.Abs(currentValue);
                }

                if (digit.Length > 1)
                {

                    double fractionalPath = currentValue - (double)integerPath;
                    natovationFractionalPath += ",";
                    int i = 0;
                    while (true && i < 8)
                    {
                        i++;
                        fractionalPath *= notation;
                        natovationFractionalPath += NotationCharacter((int)fractionalPath);
                        if (fractionalPath.ToString().Split(',').Length < 2)
                            break;
                        fractionalPath -= (int)fractionalPath;
                    }
                }
                if (Math.Abs(integerPath) >= notation)
                {
                    long ost;
                    while (true)
                    {
                        ost = integerPath % notation;
                        integerPath /= notation;
                        if (integerPath < notation)
                        {

                            notationIntegerPath += NotationCharacter(ost);
                            notationIntegerPath += NotationCharacter(integerPath);
                            break;
                        }
                        notationIntegerPath += NotationCharacter(ost);
                    }
                }
                else
                {
                    notationIntegerPath += NotationCharacter(integerPath);
                }
                if (znak)
                    notationIntegerPath += "-";
                // переворачиваем notationIntegerPath, так как после перевода в n-мерную сс главная часть идет в обратном порядке
                textInidcator.Text = new string(notationIntegerPath.ToCharArray().Reverse().ToArray()) + natovationFractionalPath;
            }
            else textInidcator.Text = X.ToString();
        }
        /// <summary>
        /// Cистема счисления - свойство
        /// </summary>
        private int Notation
        {
            get
            {
                return notation;
            }
            set
            {
                notation = value;
                //отключаем/включаем необходимые числовые кнопки
                for(int i = 1; i < 16; i++)
                    if(i < notation)
                        (Controls["buttonDigit" + i.ToString()] as Button).Enabled = true;
                    else
                        (Controls["buttonDigit" + i.ToString()] as Button).Enabled = false;
                OutputConvertNotation();
            }
        }
        /// <summary>
        /// Текущая операция
        /// </summary>
        private Operation op;

        /// <summary>
        /// Первый аргумент - свойство
        /// </summary>
        private double X
        {
            get
            {
                return x;
            }
            set
            {
                // сохранить
                x = value;
                // вывести на экран
                OutputConvertNotation();
            }
        }
        private void Clear(){
            X = 0;
            flag = false;
            op = Operation.None;
            Memory = 0;
            Notation = 10;
            DotMode = false;
            Dot = 0.1;
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            // Вначале у нас ноль
            Clear();

        }
        /// <summary>
        /// Обработчкик нажатия цифровой кнопки 
        /// </summary>
        /// <param name="sender">Инициатор события</param>
        /// <param name="e">Параметры</param>
        private void buttonDigit_Click(object sender, EventArgs e)
        {
            try
            {
                // Приведение типа
                Button b = (Button)sender;
                // Получение номера кнопки в виде строки
                string number = (string)b.Tag;
                // Преобразование строки в число
                int n = int.Parse(number);
                // обновить аргумент
                if (flag)
                {
                    if (DotMode)
                    {
                        X = Dot * n;
                        Dot /= Notation;
                    }
                    else
                        X = n;

                    flag = false;
                }
                else
                {
                    if (DotMode)
                    {
                        X += Dot * n;
                        Dot /= Notation;
                    }
                    else
                        X = X * notation + n;
                }
            }
            catch (Exception ex)
            {
                // сюда попадем если будет ошибка
                MessageBox.Show(ex.Message, "Калькулятор", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки плюс
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOperation_Click(object sender, EventArgs e)
        {
            try
            {
                // Приведение типа
                Button b = (Button)sender;
                // Получение операции кнопки в виде строки
                string s = (string)b.Tag;
                // Переход к значению перечислимого типа
                op = (Operation)Enum.Parse(typeof(Operation), s);

                y = X;
                flag = true;
                DotMode = false;
                Dot = 0.1;
            }
            catch (Exception ex)
            {
                // сюда попадем если будет ошибка
                MessageBox.Show(ex.Message, "Калькулятор", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "равно"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonResult_Click(object sender, EventArgs e)
        {
            try
            {
                switch (op)
                {
                    case Operation.Addition:
                        X = X + y;
                        break;
                    case Operation.Subtraction:
                        X = y - X;
                        break;
                    case Operation.Division:
                        X = y / X;
                        break;
                    case Operation.Multiplication:
                        X = X * y;
                        break;
                }
                flag = true;
                DotMode = false;
                Dot = 0.1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Калькулятор", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void buttonOneOperation_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = (Button)sender;
                string s = (string)b.Tag;
                op = (Operation)Enum.Parse(typeof(Operation), s);

                flag = true;
                DotMode = false;
                Dot = 0.1;
                switch (op)
                {
                    case Operation.Sqrt:
                        X = Math.Sqrt(Math.Abs(X));
                        break;
                    case Operation.Clear:
                        X = 0;
                        flag = false;
                        op = Operation.None;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Калькулятор", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem b = (ToolStripMenuItem)sender;
                string s = (string)b.Tag;
                var op = (Menus)Enum.Parse(typeof(Menus), s);

                flag = true;

                switch (op)
                {
                    case Menus.Exit:
                        Application.Exit();
                        break;
                    case Menus.Copy:
                        Clipboard.SetData(DataFormats.Text, (Object)textInidcator.Text);
                        ставитьToolStripMenuItem.Enabled = true;
                        break;
                    case Menus.Paste:
                        string[] digit = Clipboard.GetText(TextDataFormat.Text).Split(',');
                        double number = 0;
                        
                        for (int i = digit[0].Length, j = 0; i > 0; i--, j++)
                        {
                            //По школьному, но практично :)
                            if('A' == digit[0][j])
                                number += 10 * Math.Pow(notation, i - 1);
                            else if('B' == digit[0][j])
                                number += 11 * Math.Pow(notation, i - 1);
                            else if('C' == digit[0][j])
                                number += 12 * Math.Pow(notation, i - 1);
                            else if('D' == digit[0][j])
                                number += 13 * Math.Pow(notation, i - 1);
                            else if('E' == digit[0][j])
                                number += 14 * Math.Pow(notation, i - 1);
                            else if('F' == digit[0][j])
                                number += 15 * Math.Pow(notation, i - 1);
                            else
                                number += (double)(digit[0][j] - '0') * Math.Pow(notation, i - 1);
                        }
                        if (digit.Length == 2)
                        {
                            for (int i = 1; i < digit[1].Length - 1; i++)
                            {
                                if('A' == digit[1][i - 1])
                                    number += 10 / Math.Pow(notation, i);
                                else if('B' == digit[1][i - 1])
                                    number += 11 / Math.Pow(notation, i);
                                else if('C' == digit[1][i - 1])
                                    number += 12 / Math.Pow(notation, i);
                                else if('D' == digit[1][i - 1])
                                    number += 13 / Math.Pow(notation, i);
                                else if('E' == digit[1][i - 1])
                                    number += 14 / Math.Pow(notation, i);
                                else if('F' == digit[1][i - 1])
                                    number += 15 / Math.Pow(notation, i);
                                else
                                    number += (double)(digit[1][i - 1] - '0') / Math.Pow(notation, i);

                            }

                        }
                        X = number;
                        flag = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Калькулятор", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void trackBarNotation_Scroll(object sender, EventArgs e)
        {
            labelCurrentNotation.Text = (trackBarNotation.Value + 2).ToString();
            Notation = trackBarNotation.Value + 2;
        }

        private void buttonDotMode_Click(object sender, EventArgs e)
        {
            Dot = 0.1;
            DotMode = true;
        }

        private void buttonMemory_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = (Button)sender;
                string s = (string)b.Tag;
                var op = (MemoryOperation)Enum.Parse(typeof(MemoryOperation), s);

                flag = true;

                switch (op)
                {
                    case MemoryOperation.AdditionMemory:
                        Memory += X;
                        break;
                    case MemoryOperation.SubtractionMemory:
                        Memory -= X;
                        break;
                    case MemoryOperation.OutMemory:
                        X = Memory;
                        break;
                    case MemoryOperation.RemoveMemory:
                        Memory = 0;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Калькулятор", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }




    }
}
