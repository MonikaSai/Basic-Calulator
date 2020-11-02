using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextDisplay.Text)) return;
            Clipboard.SetText(TextDisplay.Text);

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            previousOperation = Operation.None;
            TextDisplay.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (TextDisplay.Text.Length > 0)
            {
                double d;
                if (!double.TryParse(TextDisplay.Text[TextDisplay.Text.Length - 1].ToString(), out d))
                {
                    previousOperation = Operation.None;
                }

                TextDisplay.Text = TextDisplay.Text.Remove(TextDisplay.Text.Length - 1, 1);
            }
        }

        private void btnDiv_Click(object sender, EventArgs e)
        {
            if (previousOperation != Operation.None)
                PerformCalculation(previousOperation);

            previousOperation = Operation.Div;
            TextDisplay.Text += (sender as Button).Text;
        }

        private void btnMul_Click(object sender, EventArgs e)
        {
            if (previousOperation != Operation.None)
                PerformCalculation(previousOperation);

            previousOperation = Operation.Mul;
            TextDisplay.Text += (sender as Button).Text;
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            if (previousOperation != Operation.None)
                PerformCalculation(previousOperation);

            previousOperation = Operation.Sub;
            TextDisplay.Text += (sender as Button).Text;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (previousOperation != Operation.None)
                PerformCalculation(previousOperation);

            previousOperation = Operation.Add;
            TextDisplay.Text += (sender as Button).Text;
        }

        private void PerformCalculation(Operation previousOperation)
        {
                if (previousOperation == Operation.None)
                    return;
            List<double> lstNums = new List<double>();

            switch (previousOperation)
            {
                case Operation.Add:
                    lstNums = TextDisplay.Text.Split('+').Select(double.Parse).ToList();
                    TextDisplay.Text = (lstNums[0] + lstNums[1]).ToString();
                    break;
                case Operation.Sub:
                    int idx = TextDisplay.Text.LastIndexOf('-'); 
                    if (idx > 0)
                    {
                        var op1 = Convert.ToDouble(TextDisplay.Text.Substring(0, idx));
                        var op2 = Convert.ToDouble(TextDisplay.Text.Substring(idx + 1));
                        TextDisplay.Text = (op1 - op2).ToString();
                    }
                    break;
                case Operation.Mul:
                    lstNums = TextDisplay.Text.Split('*').Select(double.Parse).ToList();
                    TextDisplay.Text = (lstNums[0] * lstNums[1]).ToString();
                    break;
                case Operation.Div:

                    try
                    {
                        lstNums = TextDisplay.Text.Split('/').Select(double.Parse).ToList();
                        TextDisplay.Text = (lstNums[0] / lstNums[1]).ToString();
                    }
                    catch (DivideByZeroException)
                    {
                        TextDisplay.Text = "divideByZero";
                    }
                    break;
                case Operation.None:
                    break;
                default:
                    break;
            }
        }

        private void btnNum_Click(object btn, EventArgs e)
        {
            TextDisplay.Text += (btn as Button).Text;

        }

        enum Operation
        {
            Add,
            Sub,
            Mul,
            Div,
            None
        }
        static Operation previousOperation = Operation.None;

        private void btnRes_Click(object sender, EventArgs e)
        {
            if (previousOperation == Operation.None)
                return;
            else
                PerformCalculation(previousOperation);
        }
    }
}
