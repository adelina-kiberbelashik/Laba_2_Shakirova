using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using Laba_2_Shakirova.Logic;

namespace Laba_2_Shakirova
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeComboBoxes();
            ApplyPreset(0);
        }

        private void InitializeComboBoxes()
        {
            NCombo.ItemsSource = ComboBoxDataProvider.GetNValues();
            KCombo.ItemsSource = ComboBoxDataProvider.GetKValues();
            PresetCombo.ItemsSource = ComboBoxDataProvider.GetPresets();

            NCombo.SelectedIndex = 4;
            KCombo.SelectedIndex = 4;
            PresetCombo.SelectedIndex = 0;
        }

        private void PresetCombo_SelectionChanged(object sender,
                                                 System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (PresetCombo.SelectedIndex >= 0)
                ApplyPreset(PresetCombo.SelectedIndex);
        }

        private void ApplyPreset(int index)
        {
            switch (index)
            {
                case 0:
                    NCombo.SelectedItem = 5; KCombo.SelectedItem = 5;
                    XTextBox.Text = "1,0"; YTextBox.Text = "1,0"; break;
                case 1:
                    NCombo.SelectedItem = 2; KCombo.SelectedItem = 2;
                    XTextBox.Text = "0,5"; YTextBox.Text = "0,5"; break;
                case 2:
                    NCombo.SelectedItem = 3; KCombo.SelectedItem = 3;
                    XTextBox.Text = "-1,0"; YTextBox.Text = "-2,0"; break;
                case 3:
                    NCombo.SelectedItem = 50; KCombo.SelectedItem = 50;
                    XTextBox.Text = "0,9"; YTextBox.Text = "0,8"; break;
            }
        }

        private static bool TryParseDouble(string text, out double value)
        {
            text = text?.Trim().Replace(',', '.');
            return double.TryParse(text,
                NumberStyles.Float, CultureInfo.InvariantCulture, out value);
        }

        private void Calc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NCombo.SelectedItem is not int n || KCombo.SelectedItem is not int k)
                {
                    ShowError("Выберите значения N и K из списков.");
                    return;
                }

                if (!TryParseDouble(XTextBox.Text, out double x) ||
                    !TryParseDouble(YTextBox.Text, out double y))
                {
                    ShowError("x и y должны быть числами (например, 1,5 или -2.0).");
                    return;
                }

                var calc = new FormulaCalculator(n, k, x, y);
                if (!calc.Validate(out string error))
                {
                    ShowError("Ошибка: " + error);
                    return;
                }

                double result = calc.Calculate();

                this.Title = $"Ответ: {result:F6}";
                ResultText.Text = result.ToString("F6");

                // Красивый цвет для статуса (неоновый фиолетовый)
                StatusText.Foreground = new SolidColorBrush(Color.FromRgb(224, 86, 253));
                StatusText.Text = $"Параметры: N={n}, K={k}, x={x}, y={y} | итераций: {n * k}";
            }
            catch (Exception ex)
            {
                ShowError("Непредвиденная ошибка: " + ex.Message);
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            NCombo.SelectedIndex = 4;
            KCombo.SelectedIndex = 4;
            XTextBox.Text = "1,0";
            YTextBox.Text = "1,0";
            ResultText.Text = "—";
            StatusText.Foreground = new SolidColorBrush(Color.FromRgb(127, 140, 141)); // Серый цвет обратно
            StatusText.Text = "Параметры сброшены. Нажмите «Считать».";
            this.Title = "Ответ: 0.000000";
        }

        // Вспомогательный метод для вывода ошибок красным
        private void ShowError(string message)
        {
            StatusText.Foreground = Brushes.IndianRed;
            StatusText.Text = message;
        }
    }
}