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
            degree = Math.Min(correctionPoints.Count - 1, degree);
            // Fit a polynomial to the data
            // https://numerics.mathdotnet.com/Regression
            var x = correctionPoints.Select(point => (double) point.X).ToArray();
            var y = correctionPoints.Select(point => (double) point.Y).ToArray();
            var array = Fit.Polynomial(x, y, degree);
            var coefficients = Vector<double>.Build.DenseOfArray(array);

            // Generate baseline using the polynomial
            var ret = new List<Point>();
            foreach (var chartPoint in chartPoints)
            {
                var baselineY = EvaluatePolynomial(coefficients, (double) chartPoint.X);
                var baselinePoint = new Point(chartPoint.X, (decimal) baselineY);
                ret.Add(baselinePoint);
            }
            return ret;
        }
        
        private static double EvaluatePolynomial(Vector<double> coefficients, double x)
        {
            double result = 0;
            for (var i = 0; i < coefficients.Count; i++)
            {
                result += coefficients[i] * Math.Pow(x, i);
            }
            return result;
        }
    }
}