using System;

namespace Laba_2_Shakirova.Logic
{
    public class FormulaCalculator
    {
        public int N { get; set; }
        public int K { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public const int MaxIterations = 10_000;

        public FormulaCalculator() { }

        public FormulaCalculator(int n, int k, double x, double y)
        {
            N = n; K = k; X = x; Y = y;
        }

        public bool Validate(out string error)
        {
            error = null;

            if (N < 1 || K < 1)
                error = "N и K должны быть целыми числами >= 1.";
            else if (N > MaxIterations || K > MaxIterations)
                error = $"N и K не должны превышать {MaxIterations}.";
            else if (double.IsNaN(X) || double.IsInfinity(X) ||
                     double.IsNaN(Y) || double.IsInfinity(Y))
                error = "x и y должны быть конечными числами.";

            return error == null;
        }

        public double Calculate()
        {
            double sum = 0.0;

            for (int i = 1; i <= N; i++)
            {
                double xPow = Math.Pow(X, 2 * i);
                double iSq = (double)i * i;

                for (int j = 1; j <= K; j++)
                {
                    double yPow = Math.Pow(Y, 2 * j);
                    double jSq = (double)j * j;

                    double numerator = i * xPow + j * yPow;
                    double denominator = iSq * jSq;

                    sum += numerator / denominator;
                }
            }

            return sum;
        }
    }
}