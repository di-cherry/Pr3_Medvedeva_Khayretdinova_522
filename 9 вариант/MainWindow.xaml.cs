using System;
using System.Collections.Generic;
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

namespace pr3._2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow_(object sender, RoutedEventArgs e)
        {
            ResetControls();
        }

        private void Linear_OnChecked(object sender, RoutedEventArgs e)
        {
            GridC.Visibility = Visibility.Collapsed;
        }

        private void Quadratic_OnChecked(object sender, RoutedEventArgs e)
        {
            GridC.Visibility = Visibility.Visible;
        }

        private void CalculateButton(object sender, RoutedEventArgs e)
        {
            if (!RadioButtonLinear.IsChecked.HasValue && !RadioButtonQuadratic.IsChecked.HasValue)
            {
                MessageBox.Show("Выберите вид уравнения.");
                return;
            }

            if (!double.TryParse(TextBoxA.Text, out double a))
            {
                ErrorMessage("Некорректное значение для 'a'. Введите число.");
                return;
            }

            if (!double.TryParse(TextBoxB.Text, out double b))
            {
                ErrorMessage("Некорректное значение для 'b'. Введите число.");
                return;
            }

            if (RadioButtonLinear.IsChecked == true)
            {
                SolveLinearEquation(a, b);
            }
            else if (RadioButtonQuadratic.IsChecked == true)
            {
                if (!double.TryParse(TextBoxC.Text, out double c))
                {
                    ErrorMessage("Некорректное значение для 'c'. Введите число.");
                    return;
                }
                SolveQuadraticEquation(a, b, c);
            }
        }
        private void ClearButton(object sender, RoutedEventArgs e)
        {
            ResetControls();
        }

        private void SolveLinearEquation(double a, double b)
        {
            if (a == 0)
            {
                if (b == 0)
                {
                    TextBlockResult.Text = "Бесконечное количество решений (любое x)";
                }
                else
                {
                    TextBlockResult.Text = "Решений нет";
                }
            }
            else
            {
                double x = -b / a;
                TextBlockResult.Text = $"Ответ: x = {x}";
            }
        }

        private void SolveQuadraticEquation(double a, double b, double c)
        {
            if (a == 0)
            {
                TextBlockResult.Text = "Это линейное уравнение (a не может быть равно 0). Решите его как линейное.";
                return;
            }
            
            double discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                TextBlockResult.Text = "Корней нет (комплексные корни)";
            }
            else if (discriminant == 0)
            {
                double x = -b / (2 * a);
                TextBlockResult.Text = $"Ответ: x = {x}";
            }
            else
            {
                double x1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                double x2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                TextBlockResult.Text = $"Ответ: x1 = {x1}, x2 = {x2}";
            }
        }

        private void ErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ResetControls()
        {
            TextBoxA.Text = "";
            TextBoxB.Text = "";
            TextBoxC.Text = "";
            GridC.Visibility = Visibility.Collapsed;
            RadioButtonLinear.IsChecked = false;
            RadioButtonQuadratic.IsChecked = false;
            TextBlockResult.Text = "";
        }
    }
}
