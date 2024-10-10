using System;
using System.Windows.Forms;
using System.Drawing;

namespace CalculatorApp
{
    public partial class Form1 : Form
    {
        // Fields to store calculator state
        double result = 0;
        string operation = "";
        bool isOperationPerformed = false;
        string savedResult = "";

        public Form1()
        {
            InitializeComponent();
        }

        // Digit button click
        private void button_Click(object sender, EventArgs e)
        {
            if ((txtDisplay.Text == "0") || (isOperationPerformed))
                txtDisplay.Clear();

            isOperationPerformed = false;
            Button button = (Button)sender;
            txtDisplay.Text = txtDisplay.Text + button.Text;
        }

        // Operator button click
        private void operator_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (result != 0)
            {
                btnEqual.PerformClick();
                operation = button.Text;
                isOperationPerformed = true;
            }
            else
            {
                operation = button.Text;
                result = Double.Parse(txtDisplay.Text);
                isOperationPerformed = true;
            }
        }

        // Equal button click
        private void btnEqual_Click(object sender, EventArgs e)
        {
            try
            {
                switch (operation)
                {
                    case "+":
                        txtDisplay.Text = (result + Double.Parse(txtDisplay.Text)).ToString();
                        break;
                    case "-":
                        txtDisplay.Text = (result - Double.Parse(txtDisplay.Text)).ToString();
                        break;
                    case "*":
                        txtDisplay.Text = (result * Double.Parse(txtDisplay.Text)).ToString();
                        break;
                    case "/":
                        if (Double.Parse(txtDisplay.Text) == 0)
                        {
                            MessageBox.Show("Cannot divide by zero");
                            txtDisplay.Text = result.ToString();
                            return;
                        }
                        txtDisplay.Text = (result / Double.Parse(txtDisplay.Text)).ToString();
                        break;
                    default:
                        break;
                }

                result = Double.Parse(txtDisplay.Text);
                operation = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Clear button click
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            result = 0;
            operation = "";
        }

        // Save result
        private void btnSaveResult_Click(object sender, EventArgs e)
        {
            savedResult = txtDisplay.Text;
            MessageBox.Show("Result saved!");
        }

        // Load result
        private void btnLoadResult_Click(object sender, EventArgs e)
        {
            if (savedResult != "")
            {
                txtDisplay.Text = savedResult;
                MessageBox.Show("Result loaded!");
            }
            else
            {
                MessageBox.Show("No saved result!");
            }
        }

        // Change color scheme
        private void btnChangeColor_Click(object sender, EventArgs e)
        {
            if (this.BackColor == SystemColors.Control)
            {
                this.BackColor = Color.LightBlue;
            }
            else
            {
                this.BackColor = SystemColors.Control;
            }
        }
    }
}