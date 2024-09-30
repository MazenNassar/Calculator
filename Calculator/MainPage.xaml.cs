using System;
using Microsoft.Maui.Controls;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        string currentInput = "";
        string firstOperand = "";
        string secondOperand = "";
        string operation = "";
        bool isSecondOperand = false;

        public MainPage()
        {
            InitializeComponent();
            currentInput = "0";
            UpdateDisplay(currentInput);
        }

        // Function to update the display
        private void UpdateDisplay(string value)
        {
            if (string.IsNullOrEmpty(currentInput))
                currentInput = "0";
            displayLabel.Text = value;
        }

        // Function to handle numeric button clicks (0-9)
        private void OnNumericButtonClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string value = button.Text;

            // Add digit to the current input
            if (currentInput == "0" || isSecondOperand)
            {
                currentInput = value;
                isSecondOperand = false;
            }
            else
            {
                currentInput += value;
            }
            UpdateDisplay(currentInput);
        }

        // Function to handle operator button clicks (+, -, *, ÷, x², xʸ)
        private void OnOperatorButtonClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string selectedOperation = button.Text;

            if (!isSecondOperand)
            {
                firstOperand = currentInput;
                currentInput = "";
                isSecondOperand = true;
            }

            operation = selectedOperation;
            UpdateDisplay(firstOperand);
        }

        // Square Root Button (√)
        private void OnSquareRootButtonClicked(object sender, EventArgs e)
        {
            double value = Convert.ToDouble(currentInput);
            currentInput = Math.Sqrt(value).ToString();
            UpdateDisplay(currentInput);
        }

        // Cube Root Button (∛)
        private void OnCubeRootButtonClicked(object sender, EventArgs e)
        {
            double value = Convert.ToDouble(currentInput);
            currentInput = Math.Pow(value, 1.0 / 3.0).ToString();
            UpdateDisplay(currentInput);
        }

        // Power of Two Button (x²)
        private void OnPowerOfTwoButtonClicked(object sender, EventArgs e)
        {
            double value = Convert.ToDouble(currentInput);
            currentInput = Math.Pow(value, 2).ToString();
            UpdateDisplay(currentInput);
        }

        // Power of x (xʸ)
        private void OnPowerOfXButtonClicked(object sender, EventArgs e)
        {
            if (!isSecondOperand)
            {
                firstOperand = currentInput;
                currentInput = "";
                operation = "xʸ";
                isSecondOperand = true;
            }
            UpdateDisplay(firstOperand);
        }

        // Division (÷) button
        private void OnDivisionButtonClicked(object sender, EventArgs e)
        {
            OnOperatorButtonClicked(sender, e);
        }

        // Equals Button (=) - Perform the calculation
        private void OnEqualsButtonClicked(object sender, EventArgs e)
        {
            // If we have a valid operation and operand
            if (!isSecondOperand && !string.IsNullOrEmpty(firstOperand) && !string.IsNullOrEmpty(operation))
            {
                secondOperand = currentInput;
                double firstNum = Convert.ToDouble(firstOperand);
                double secondNum = Convert.ToDouble(secondOperand);
                double result = 0;

                switch (operation)
                {
                    case "+":
                        result = firstNum + secondNum;
                        break;
                    case "-":
                        result = firstNum - secondNum;
                        break;
                    case "×":
                        result = firstNum * secondNum;
                        break;
                    case "÷":
                        if (secondNum != 0)
                        {
                            result = firstNum / secondNum;
                        }
                        else
                        {
                            UpdateDisplay("Error"); // Prevent division by zero
                            return;
                        }
                        break;
                    case "xʸ":
                        result = Math.Pow(firstNum, secondNum);
                        break;
                }

                // Show the result and prepare for new operation
                currentInput = result.ToString();
                UpdateDisplay(currentInput);

                // Reset operands and allow continuous calculation with the result
                firstOperand = currentInput;
                secondOperand = "";
                operation = "";
                isSecondOperand = true; // Allow chaining operations
            }
        }

        // Clear Button (AC)
        private void OnClearButtonClicked(object sender, EventArgs e)
        {
            currentInput = "0";
            firstOperand = "";
            secondOperand = "";
            operation = "";
            isSecondOperand = false;
            UpdateDisplay(currentInput);
        }

        // Delete Button (DEL)
        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (currentInput.Length > 1)
            {
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
            }
            else
            {
                currentInput = "0";
            }
            UpdateDisplay(currentInput);
        }

        // Fraction Button (a/b)
        private void OnFractionButtonClicked(object sender, EventArgs e)
        {
            // Toggle between fraction view - for simplicity, divide by 2
            if (!string.IsNullOrEmpty(currentInput))
            {
                double value = Convert.ToDouble(currentInput);
                currentInput = (value / 2).ToString();
                UpdateDisplay(currentInput);
            }
        }

        // Toggle between decimal and fraction view (S<>D)
        private void OnSwitchButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentInput))
            {
                double value = Convert.ToDouble(currentInput);
                currentInput = (1 / value).ToString(); // Example logic: just invert the value for now
                UpdateDisplay(currentInput);
            }
        }
    }
}
