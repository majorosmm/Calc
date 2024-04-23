using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace calcv2
{
    public partial class MainWindow : Window
    {
        string result;
        string Ans;
        double parsedResult;


        public MainWindow()
        {
            InitializeComponent();
        }
        private void HandleError(Exception ex)
        {
            if (ex is System.Data.SyntaxErrorException ||
                ex is System.OverflowException ||
                ex is System.Data.EvaluateException ||
                ex is System.ArgumentNullException)
            {
                Error.Content = "Invalid expression";
            }
            else
            {
                Error.Content = ex.Message;
            }

            panel.Clear();
        }
        // Szám gombra kattintás eseménykezelő
        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            panel.Text += (string)btn.Content;
            Error.Content = ""; // Hibaüzenet törlése
        }

        // Művelet gombra kattintás eseménykezelő
        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            panel.Text += (string)btn.Content;
            Error.Content = ""; // Hibaüzenet törlése
        }

        // Egyenlőségjel gombra kattintás eseménykezelő
        private void Equal_Click(object sender, RoutedEventArgs e)
{
    try
    {
        result = Eval(panel.Text); // Kifejezés kiértékelése

        // Eredmény kezelése
        if (result == "∞")
        {
            Error.Content = "Can't divide by 0"; // Nullával való osztás ellenőrzése
            result = "";
            panel.Text = "";
        }
        else
        {
            if (double.TryParse(result, out parsedResult))
            {
                panel.Text = Math.Round(parsedResult, 2).ToString(); // Eredmény kerekítése
            }
            else
            {
                HandleError(new Exception("Invalid expression")); // Érvénytelen kifejezés esetén hibaüzenet
            }
        }
    }
    catch (Exception ex)
    {
        HandleError(ex);
    }
}

        // Kifejezés kiértékelése
        private string Eval(string expression)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            return Convert.ToString(table.Compute(expression, ""));
        }

        // 'C' gombra kattintás eseménykezelő
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            panel.Clear(); // Panel tartalmának törlése
            Error.Content = ""; // Hibaüzenet törlése
        }

        // 'Ans' gombra kattintás eseménykezelő
        private void Ans_Click(object sender, RoutedEventArgs e)
        {
            Ans = result;
            panel.Text += Ans; // Utolsó eredmény hozzáadása a panelhez
        }

        // 'Del' gombra kattintás eseménykezelő
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (panel.Text.Length > 0)
            {
                panel.Text = panel.Text.Substring(0, panel.Text.Length - 1); // Utolsó karakter törlése
            }
        }

        // Gombra mutató egér belépés eseménykezelő
        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.Background = Brushes.Gray; // Gomb háttérszínének megváltoztatása
        }

        // Gombra mutató egér elhagyás eseménykezelő
        private void Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.Background = Brushes.White; // Gomb háttérszínének visszaállítása
        }

        private void Negate_Click(object sender, RoutedEventArgs e)
        {
            

            try
            {
                panel.Text = Convert.ToString(int.Parse(panel.Text) * (-1));
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }


        }
    }
}
