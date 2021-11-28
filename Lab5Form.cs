using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Symbolics;

namespace ProgLab1
{
    public partial class Lab5Form : Form
    {
        public Lab5Form()
        {
            InitializeComponent();
        }

        async public void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (Control control in this.Controls)
                {
                    if (control is TextBox && !control.Enabled)
                    {
                        control.Text = "";
                    }
                }

                double.TryParse(textBoxA.Text, out double aBord);
                double.TryParse(textBoxB.Text, out double bBord);
                double.TryParse(textBoxE.Text, out double esp);
                Expression func = Infix.ParseOrThrow(textBoxF.Text);

                if (esp < 0)
                {
                    throw new Exception();
                }
                await Task.Run(() => LineDraw(aBord, bBord, esp, func));

                int round = Round(esp);

                if (checkBoxTrapezium.Checked)
                {
                    int splits = TrapeziumMethod.OptimalSplits(aBord, bBord, esp, func);
                    double square = TrapeziumMethod.Calculation(aBord, bBord, splits, func);
                    trapeziumAnswerBox.Text = Math.Round(square, round).ToString() + "; " + splits.ToString();
                    TrapeziumSplits(aBord, bBord, splits, func);
                }

                if (checkBoxRectangle.Checked)
                {
                    int splits = RectangleMethod.OptimalSplits(aBord, bBord, esp, func);
                    double square = RectangleMethod.Calculation(aBord, bBord, splits, func);
                    rectangleAnswerBox.Text = Math.Round(square, round).ToString() + "; " + splits.ToString();
                    RectangleSplits(aBord, bBord, splits, func);
                }

                if (checkBoxSimpson.Checked)
                {
                    int splits = SimpsonMethod.OptimalSplits(aBord, bBord, esp, func);
                    double square = SimpsonMethod.Calculation(aBord, bBord, splits, func);
                    simpsonAnswerBox.Text = Math.Round(square, round).ToString() + "; " + splits.ToString();
                    SimpsonSplits(aBord, bBord, splits, func);
                }
            }

            catch
            {
                if (textBoxA.Text == "" || textBoxB.Text == "" || textBoxE.Text == "" || textBoxF.Text == "")
                {
                    MessageBox.Show("Пустые поля недопустимы");
                }
                else if (double.Parse(textBoxE.Text) < 0)
                {
                    MessageBox.Show("Точность не может быть меньше 0");
                }
                else if (double.Parse(textBoxA.Text) >= double.Parse(textBoxB.Text))
                {
                    MessageBox.Show("Параметр a должен быть меньше параметра b");
                }
                else
                {
                    MessageBox.Show("Некорректно задана вычисляемая функция");
                }
            }
        }

        private int Round(double esp)
        {
            int round = 0;
            double espValue = esp;
            while (espValue < 1)
            {
                round += 1;
                espValue *= 10;
            }
            return round;
        }

        public void TrapeziumSplits(double aBord, double bBord, int splits, Expression func)
        {
            
        }

        public void RectangleSplits(double aBord, double bBord, int splits, Expression func)
        {
            
        }

        public void SimpsonSplits(double aBord, double bBord, int splits, Expression func)
        {
            
        }


        private void LineDraw(double aBord, double bBord, double esp, Expression func)
        {

        }

        static public double FuncValue(double point, Expression func)
        {
            Dictionary<string, FloatingPoint> x = new Dictionary<string, FloatingPoint>()
            {
                { "x", point }
            };
            return Evaluate.Evaluate(x, func).RealValue;
        }

        private void Params_KeyPress(object sender, KeyPressEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (char.IsDigit(e.KeyChar) || (e.KeyChar == ',' && textBox.Text.Contains(",") == false) || (e.KeyChar == '-' && textBox.Text == "") || (e.KeyChar == (char)Keys.Back))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            double.TryParse(textBoxA.Text, out double aBord);
            double.TryParse(textBoxB.Text, out double bBord);
            double.TryParse(textBoxE.Text, out double esp);
            Expression func = Infix.ParseOrThrow(textBoxF.Text);
            int round = Round(esp);


            if (trapeziumAnswerBox.Text != "")
            {
                int splits = int.Parse(trapeziumAnswerBox.Text.Substring(trapeziumAnswerBox.Text.IndexOf("; ") + 1));
                double square;
                if (splits / 2 > 3)
                {
                    splits /= 2;
                    square = TrapeziumMethod.Calculation(aBord, bBord, splits, func);
                    trapeziumAnswerBox.Text = Math.Round(square, round).ToString() + "; " + splits.ToString();
                    TrapeziumSplits(aBord, bBord, splits, func);
                }
            }

            if (rectangleAnswerBox.Text != "")
            {
                int splits = int.Parse(rectangleAnswerBox.Text.Substring(rectangleAnswerBox.Text.IndexOf("; ") + 1));
                double square;
                if (splits / 2 > 3)
                {
                    splits /= 2;
                    square = RectangleMethod.Calculation(aBord, bBord, splits, func);
                    rectangleAnswerBox.Text = Math.Round(square, round).ToString() + "; " + splits.ToString();
                    RectangleSplits(aBord, bBord, splits, func);
                }
            }

            if (simpsonAnswerBox.Text != "")
            {
                int splits = int.Parse(simpsonAnswerBox.Text.Substring(simpsonAnswerBox.Text.IndexOf("; ") + 1));
                double square;
                if (splits / 2 > 3)
                {
                    splits /= 2;
                    square = SimpsonMethod.Calculation(aBord, bBord, splits, func);
                    simpsonAnswerBox.Text = Math.Round(square, round).ToString() + "; " + splits.ToString();
                    SimpsonSplits(aBord, bBord, splits, func);
                }
            }
        }

        private void forwardBtn_Click(object sender, EventArgs e)
        {
            double.TryParse(textBoxA.Text, out double aBord);
            double.TryParse(textBoxB.Text, out double bBord);
            double.TryParse(textBoxE.Text, out double esp);
            Expression func = Infix.ParseOrThrow(textBoxF.Text);
            int round = Round(esp);

            if (trapeziumAnswerBox.Text != "")
            {
                int optimalSplits = TrapeziumMethod.OptimalSplits(aBord, bBord, esp, func);
                int splits = int.Parse(trapeziumAnswerBox.Text.Substring(trapeziumAnswerBox.Text.IndexOf("; ") + 1));
                double square;
                if (splits * 2 <= optimalSplits)
                {
                    splits *= 2;
                    square = TrapeziumMethod.Calculation(aBord, bBord, splits, func);
                    trapeziumAnswerBox.Text = Math.Round(square, round).ToString() + "; " + splits.ToString();
                    TrapeziumSplits(aBord, bBord, splits, func);
                }
            }

            if (rectangleAnswerBox.Text != "")
            {
                int optimalSplits = RectangleMethod.OptimalSplits(aBord, bBord, esp, func);
                int splits = int.Parse(rectangleAnswerBox.Text.Substring(rectangleAnswerBox.Text.IndexOf("; ") + 1));
                double square;
                if (splits * 2 <= optimalSplits)
                {
                    splits *= 2;
                    square = RectangleMethod.Calculation(aBord, bBord, splits, func);
                    rectangleAnswerBox.Text = Math.Round(square, round).ToString() + "; " + splits.ToString();
                    RectangleSplits(aBord, bBord, splits, func);
                }
            }

            if (simpsonAnswerBox.Text != "")
            {
                int optimalSplits = SimpsonMethod.OptimalSplits(aBord, bBord, esp, func);
                int splits = int.Parse(simpsonAnswerBox.Text.Substring(simpsonAnswerBox.Text.IndexOf("; ") + 1));
                double square;
                if (splits * 2 <= optimalSplits)
                {
                    splits *= 2;
                    square = SimpsonMethod.Calculation(aBord, bBord, splits, func);
                    simpsonAnswerBox.Text = Math.Round(square, round).ToString() + "; " + splits.ToString();
                    SimpsonSplits(aBord, bBord, splits, func);
                }
            }
        }
    }
}
