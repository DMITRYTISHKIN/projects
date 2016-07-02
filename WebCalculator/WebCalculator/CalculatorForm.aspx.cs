using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebCalculator
{
    public partial class CalculatorForm : System.Web.UI.Page
    {
        /// <summary>
        /// Первый аргумент - свойство
        /// </summary>
        private double X
        {
            get
            {
                return (Session["x"] != null) ? (double)Session["x"] : 0;
            }
            set
            {
                double x = value;
                Session["x"] = x;
                TextResult.Text = OutputConvertNotation(x);
            } 
        }
        private double Y
        {
            get
            {
                return (Session["y"] != null) ? (double)Session["y"] : 0;
            }
            set
            {
                double y = value;
                Session["y"] = y;
            }
        }
        private int Notation
        {
            get
            {
                return (Session["notation"] != null) ? (int)Session["notation"] : 10;
            }
            set
            {
                int notation = value;
                Session["notation"] = notation;
                //отключаем/включаем необходимые числовые кнопки
                ButtonNextNotation.Enabled = notation == 16 ? false : true;
                ButtonPrevNotation.Enabled = notation == 2 ? false : true;
                TextBoxCurrentNotation.Text = notation.ToString();
                for (int i = 1; i < 16; i++)
                    ((Button)MainPanel.FindControl("Button" + i.ToString())).Enabled = (i < notation) ? true : false;
                TextResult.Text = OutputConvertNotation(X);
                TextMemory.Text = OutputConvertNotation(Memory);
            }
        }
        private Operation Op
        {
            get
            {
                return (Session["op"] != null) ? (Operation)Session["op"] : Operation.None;
            }
            set
            {
                Session["op"] = value;
            }
        }
        private bool Flag
        {
            get
            {
                return (Session["flag"] != null) ? (bool)Session["flag"] :false;
            }
            set
            {
                Session["flag"] = value;
            }
        }

        private bool DotMode
        {
            get
            {
                return (Session["dotMode"] != null) ? (bool)Session["dotMode"] : false;
            }
            set
            {
                Session["dotMode"] = value;
            }
        }
        private double Dot
        {
            get
            {
                return (Session["dot"] != null) ? (double)Session["dot"] : 0.1;
            }
            set
            {
                Session["dot"] = value;
            }
        }

        private double Memory
        {
            get
            {
                return (Session["memory"] != null) ? (double)Session["memory"] : 0;
            }
            set
            {
                double memory = value;
                Session["memory"] = memory;
                TextMemory.Text = OutputConvertNotation(memory);
            }
        }
            
        protected void Page_Load(object sender, EventArgs e)
        {
            X = X;
            Notation = Notation;
            DotMode = DotMode;
            Dot = Dot;
            Memory = Memory;

        }
        /// <summary>
        /// Обработчик нажатия на кнопку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonNumber_Click(object sender, EventArgs e)
        {
            try {
                Button b;
                b = (Button)sender;
                string ButtonID = (string)b.ID;
                //Получение номера кнопки в виде строки
                string number = ButtonID.Length < 8 ? ButtonID.Substring(6, 1) : ButtonID.Substring(6, 2);
                int n;
                if (!int.TryParse(number, out n))
                {
                    throw new Exception("Ошибка");
                }
                if (Flag)
                {
                    if (DotMode)
                    {
                        X = Dot * n;
                        Dot /= Notation;
                    }
                    else
                        X = n;

                    Flag = false;
                }
                else
                {
                    if (DotMode)
                    {
                        X += Dot * n;
                        Dot /= Notation;
                    }
                    else
                        X = X * Notation + n;
                }
            }
            catch (Exception ex)
            {
                RLabel.Text = ex.Message;
            }
        }

        protected void ButtonOperation_Click(object sender, EventArgs e)
        {
            try
            {
                Button b;
                b = (Button)sender;
                string ButtonID = (string)b.ID;
                Operation op;
                //Получение операции
                if(!Enum.TryParse<Operation>(ButtonID.Substring(6), out op))
                {
                    throw new Exception("Ошибка");
                }
                Op = op;
                Flag = true;
                Y = X;
                DotMode = false;
                Dot = 0.1;
            }
            catch (Exception ex)
            {
                RLabel.Text = ex.Message;
            }
        }

        protected void buttonResult_Click(object sender, EventArgs e)
        {
            try
            {
                switch (Op)
                {
                    case Operation.Addition:
                        X = X + Y;
                        break;
                    case Operation.Subtraction:
                        X = Y - X;
                        break;
                    case Operation.Division:
                        X = Y / X;
                        break;
                    case Operation.Multiplication:
                        X = X * Y;
                        break;
                }
                Flag = true;
            }
            catch (Exception ex)
            {
                RLabel.Text = ex.Message;
            }
        }
        protected void buttonSqrt_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
               X = Math.Sqrt(Math.Abs(X));
            }
            catch (Exception ex)
            {
                RLabel.Text = ex.Message;
            }
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            try
            {
                X = 0;
                Flag = true;
                DotMode = false;
                Dot = 0.1;
                Op = Operation.None;
            }
            catch (Exception ex)
            {
                RLabel.Text = ex.Message;
            }
        }
        protected void ButtonNextNotation_Click(object sender, EventArgs e)
        {
            try
            {
                Notation++;
            }
            catch (Exception ex)
            {
                RLabel.Text = ex.Message;
            }
        }
        protected void ButtonPrevNotation_Click(object sender, EventArgs e)
        {
            try
            {
                Notation--;
            }
            catch (Exception ex)
            {
                RLabel.Text = ex.Message;
            }
        }
        private string NotationCharacter(double Value)
        {
            if (Value > 9)
                return ((NumberNotation)Value).ToString();
            else return Value.ToString();
        }
        /// <summary>
        /// Функция вывода числа в n-мерной системе счисления
        /// </summary>
        private string OutputConvertNotation(double val)
        {
            if (Notation != 10)
            {
                string notationIntegerPath = "";
                string natovationFractionalPath = "";
                bool znak = false;
                double currentValue = val;
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
                        fractionalPath *= Notation;
                        natovationFractionalPath += NotationCharacter((int)fractionalPath);
                        if (fractionalPath.ToString().Split(',').Length < 2)
                            break;
                        fractionalPath -= (int)fractionalPath;
                    }
                }
                if (Math.Abs(integerPath) >= Notation)
                {
                    long ost;
                    while (true)
                    {
                        ost = integerPath % Notation;
                        integerPath /= Notation;
                        if (integerPath < Notation)
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
                return (new string(notationIntegerPath.ToCharArray().Reverse().ToArray()) + natovationFractionalPath);
            }
            else return val.ToString();
        }
        protected void buttonDotMode_Click(object sender, EventArgs e)
        {
            if (!DotMode)
            {
                Dot = 0.1;
                TextResult.Text += ",";
            }
            DotMode = true;
        }

        protected void ButtonMemoryEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Memory += ((string)((Button)sender).ID.Substring(12, 8) == "Addition") ? X : -X;
            }
            catch (Exception ex)
            {
                RLabel.Text = ex.Message;
            }
        }

        protected void ButtonMemoryReset_Click(object sender, EventArgs e)
        {
            Memory = 0;
        }

        protected void ButtonMemoryOut_Click(object sender, EventArgs e)
        {
            X = Memory;
        }

    }
}