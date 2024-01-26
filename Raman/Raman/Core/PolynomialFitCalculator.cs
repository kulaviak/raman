using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace Raman.Core
{
    public class PolynomialFitCalculator
    {
        public List<Point> GetCorrectedPoints(List<Point> chartPoints, List<Point> correctionPoints, int degree)
        {
            // Fit a polynomial to the data
            // https://numerics.mathdotnet.com/Regression
            var x = correctionPoints.Select(point => (double) point.X).ToArray();
            var y = correctionPoints.Select(point => (double) point.Y).ToArray();
            var array = Fit.Polynomial(x, y, degree);
            var coefficients = Vector<double>.Build.DenseOfArray(array);

            // Generate baseline using the polynomial
            var baseline = new double[x.Length];
            foreach (var chartPoint in chartPoints)
            {
                baseline[i] = EvaluatePolynomial(coefficients, x[i]);
                    
            }
            
            for (var i = 0; i < x.Length; i++)
            {
            }

            // Subtract the baseline from the original signal
            double[] correctedSignal = new double[y.Length];
            for (int i = 0; i < y.Length; i++)
            {
                correctedSignal[i] = y[i] - baseline[i];
            }

            return correctedSignal;
        }
        
        private List<Point> RemoveBaseline(List<Point> chartPoints, List<Point> baselinePoints)
        {
            throw new NotImplementedException();
        }

        private List<Point> CalculateBaselinePoints(List<Point> correctionPoints, int degree)
        {
            throw new NotImplementedException();
        }
        
        static double[] BaselineCorrection(double[] x, double[] y, int degree)
        {
            // Fit a polynomial to the data
            // https://numerics.mathdotnet.com/Regression
            var array = Fit.Polynomial(x, y, degree);
            var coefficients = Vector<double>.Build.DenseOfArray(array);

            // Generate baseline using the polynomial
            double[] baseline = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                baseline[i] = EvaluatePolynomial(coefficients, x[i]);
            }

            // Subtract the baseline from the original signal
            double[] correctedSignal = new double[y.Length];
            for (int i = 0; i < y.Length; i++)
            {
                correctedSignal[i] = y[i] - baseline[i];
            }

            return correctedSignal;
        }

        static double EvaluatePolynomial(Vector<double> coefficients, double x)
        {
            double result = 0;
            for (int i = 0; i < coefficients.Count; i++)
            {
                result += coefficients[i] * Math.Pow(x, i);
            }
            return result;
        }

    }
}