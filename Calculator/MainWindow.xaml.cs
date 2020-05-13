using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string num_prime = "", num_sec = "", LastCalc = "", calcString = "";
        private bool RefreshPrime = false, RefreshLastCalc = false, canEval = false;
        double Result = 0;

        private DataTable dt = new DataTable();

        public MainWindow()
        {
            InitializeComponent();
        }

        private string RunExpression()
        {
            return dt.Compute(calcString.Replace(" ", ""), "").ToString();
        }
        private void AddToExpression(string Entry)
        {
            Console.WriteLine("PRocessing: " + Entry + ", Can Eval: " + canEval);
            Console.WriteLine(calcString);
            Console.WriteLine(LastCalc);
            LastCalc = calcString;
            switch (Entry)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case ".":
                    if (RefreshLastCalc)
                    {
                        RefreshPrime = true;
                        RefreshLastCalc = false;
                        num_prime = "";
                        num_sec = LastCalc;
                        calcString = LastCalc;
                    }
                    if (RefreshPrime)
                    {
                        num_prime = "";
                        RefreshPrime = false;
                    }
                    if (Entry == "." && num_prime.Length >= 1 && num_prime.Contains(".") == false)
                    {
                        num_prime += Entry;
                    }else if(Entry != ".")
                    {
                        num_prime += Entry;
                    }
                    break;
                case "/":
                case "*":
                case "+":
                case "-":
                    if (num_prime.Length != 0)
                    {
                        RefreshLastCalc = false;
                        if (RefreshPrime)
                        {
                            num_prime = "";
                            RefreshPrime = false;
                        }
                        num_sec += num_prime + " " + Entry + " ";
                        calcString += num_prime + Entry;
                        RefreshPrime = true;
                    }
                    break;
                case "Cos":
                    if(num_prime.Length != 0)
                    {
                        Result = Math.Cos(double.Parse(num_prime) * (Math.PI / 180.0));

                        calcString += Result;
                        num_sec += " Cos(" + Result + ") ";
                        num_prime = Result.ToString();
                        RefreshPrime = true;
                    }
                    break;
                case "Sin":
                    if (num_prime.Length != 0)
                    {
                        Result = Math.Sin(double.Parse(num_prime) * (Math.PI / 180.0));

                        calcString += Result;
                        num_sec += " Sin(" + num_prime + ") ";
                        num_prime = Result.ToString();
                        RefreshPrime = true;
                    }
                    break;
                case "Tan":
                    if (num_prime.Length != 0)
                    {
                        Result = Math.Tan(double.Parse(num_prime) * (Math.PI / 180.0));

                        calcString += Result;
                        num_sec += " Tan(" + num_prime + ") ";
                        num_prime = Result.ToString();
                        RefreshPrime = true;
                    }
                    break;
                case "Log":
                    if (num_prime.Length != 0)
                    {
                        Result = Math.Log(double.Parse(num_prime) * (Math.PI / 180.0));

                        calcString += Result;
                        num_sec += " Log(" + num_prime + ") ";
                        num_prime = Result.ToString();
                        RefreshPrime = true;
                    }
                    break;
                case "Fac":
                    if (num_prime.Length != 0)
                    {
                        Result = double.Parse(num_prime);
                        for(double i = Result - 1; i > 0; i--)
                        {
                            Result *= i;
                        }

                        calcString += Result;
                        num_sec += " " + Result + " ";
                        num_prime = Result.ToString();
                        RefreshPrime = true;
                        RefreshLastCalc = true;
                    }
                    break;
                case "Rad":
                    if (num_prime.Length != 0)
                    {
                        Result = Math.Sqrt(double.Parse(num_prime));

                        calcString += Result;
                        num_sec += " " + Result + " ";
                        num_prime = Result.ToString();
                        RefreshPrime = true;
                        RefreshLastCalc = true;
                    }
                    break;
                case "NegPos":
                    if (num_prime.Contains("-"))
                    {
                        num_prime = num_prime.Substring(1, num_prime.Length-1);
                    }
                    else
                    {
                        num_prime = "-" + num_prime;
                    }
                    break;
                Deafult:
                    break;
            }
            UpdateOutputs();
        }

        private void UpdateOutputs()
        {
            output_numbers.Text = num_prime;
            output_numbers_prev.Text = num_sec;
        }
        
        private void Btn_C_Click(object sender, RoutedEventArgs e)
        {
            num_prime = "";
            UpdateOutputs();
        }

        private void Btn_CE_Click(object sender, RoutedEventArgs e)
        {
            num_prime = "";
            num_sec = "";
            calcString = "";
            LastCalc = "";
            UpdateOutputs();
        }

        private void Btn_Equals_Click(object sender, RoutedEventArgs e)
        {
            calcString += num_prime;
            num_prime = RunExpression();
            num_sec = "";
            calcString = "";
            LastCalc = "";
            RefreshPrime = true;
            RefreshLastCalc = false;
            UpdateOutputs();
        }

        private void Btn_divide_Click(object sender, RoutedEventArgs e)
        {
            AddToExpression("/");
        }

        private void Btn_multiple_Click(object sender, RoutedEventArgs e)
        {
            AddToExpression("*");
        }

        private void Btn_plus_Click(object sender, RoutedEventArgs e)
        {
            AddToExpression("+");
        }

        private void Btn_minus_Click(object sender, RoutedEventArgs e)
        {
            AddToExpression("-");
        }

        private void Btn_PERIOD_Click(object sender, RoutedEventArgs e)
        {
            AddToExpression(".");
        }

        private void Btn_0_Click(object sender, RoutedEventArgs e)
        {
            AddToExpression("0");
        }

        private void Btn_1_Click(object sender, RoutedEventArgs e)
        {
            AddToExpression("1");
        }

        private void Btn_2_Click(object sender, RoutedEventArgs e)
        {
            AddToExpression("2");
        }

        private void Btn_3_Click(object sender, RoutedEventArgs e)
        {
            AddToExpression("3");
        }

        private void Btn_4_Click(object sender, RoutedEventArgs e)
        {
            AddToExpression("4");
        }

        private void Btn_5_Click(object sender, RoutedEventArgs e)
        {
            AddToExpression("5");
        }

        private void Btn_6_Click(object sender, RoutedEventArgs e)
        {
            AddToExpression("6");
        }

        private void Btn_7_Click(object sender, RoutedEventArgs e)
        {
            AddToExpression("7");
        }
        private void Btn_8_Click(object sender, RoutedEventArgs e)
        {
            AddToExpression("8");
        }

        private void Btn_9_Click(object sender, RoutedEventArgs e)
        {
            AddToExpression("9");
        }

        private void Btn_cosine_Click(object sender, RoutedEventArgs e)
        {
            AddToExpression("Cos");
        }

        private void Btn_negpos_Click(object sender, RoutedEventArgs e)
        {
            AddToExpression("NegPos");
        }

        private void Btn_Sinus_Click(object sender, RoutedEventArgs e)
        {
            AddToExpression("Sin");
        }

        private void Btn_Tangent_Click(object sender, RoutedEventArgs e)
        {
            AddToExpression("Tan");
        }

        private void Btn_Logarithm_Click(object sender, RoutedEventArgs e)
        {
            AddToExpression("Log");
        }

        private void Btn_Factorial_Click(object sender, RoutedEventArgs e)
        {
            AddToExpression("Fac");
        }

        private void Btn_Radical_Click(object sender, RoutedEventArgs e)
        {
            AddToExpression("Rad");
        }
    }
}
