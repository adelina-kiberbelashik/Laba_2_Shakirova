using System.Collections.Generic;

namespace Laba_2_Shakirova.Logic
{
    public static class ComboBoxDataProvider
    {
        public static List<int> GetNValues() =>
            new List<int> { 1, 2, 3, 5, 10, 20, 50, 100 };

        public static List<int> GetKValues() =>
            new List<int> { 1, 2, 3, 5, 10, 20, 50, 100 };

        public static List<string> GetPresets() =>
            new List<string>
            {
                "Стандарт: N=5, K=5, x=1.0, y=1.0",
                "Малые: N=2, K=2, x=0.5, y=0.5",
                "Отрицательные: N=3, K=3, x=-1.0, y=-2.0",
                "Большие: N=50, K=50, x=0.9, y=0.8"
            };
    }
}