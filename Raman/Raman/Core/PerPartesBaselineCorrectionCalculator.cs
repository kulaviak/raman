using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace Raman.Core
{
    public class PerPartesBaselineCorrectionCalculator
    {
        public List<Point> CorrectBaseline(List<Point> chartPoints, List<Point> correctionPoints, int degree)
        {
            if (correctionPoints.Count == 0)
            {
                throw new AppException("Baseline correction failed. There are no correction points.");
            }
            if (chartPoints.Count == 0)
            {
                throw new AppException("Baseline correction failed. There are no chart points.");
            }
            chartPoints = chartPoints.OrderBy(point => point.X).ToList();
            correctionPoints = correctionPoints.OrderBy(point => point.X).ToList();
            var ret = new List<Point>();

            var correctionStart = correctionPoints[0].X;
            var correctionEnd = correctionPoints[correctionPoints.Count - 1].X;
            
            var chartPointsBeforeFirstCorrectionPoint = chartPoints.Where(point => point.X < correctionStart).ToList();
            ret.AddRange(chartPointsBeforeFirstCorrectionPoint);

            var chartPointsBetweenCorrectionPoints = chartPoints.Where(point => correctionStart  <= point.X && point.X <= correctionEnd).ToList();
            var correctedPoints = DoCorrection(chartPointsBetweenCorrectionPoints, correctionPoints, degree);
            ret.AddRange(correctedPoints);
            
            var chartPointsAfterLastCorrectionPoint = chartPoints.Where(point => point.X > correctionEnd).ToList();
            ret.AddRange(chartPointsAfterLastCorrectionPoint);
            return ret;
        }

        private List<Point> DoCorrection(List<Point> chartPoints, List<Point> correctionPoints, int degree)
        {
            var ret = new List<Point>();
            var batches = SplitList(correctionPoints, degree + 1);
            foreach (var batchPoints in batches)
            {
                var correctionStart = batchPoints[0].X;
                var correctionEnd = batchPoints[batchPoints.Count - 1].X;
                var pointsToBeCorrected = chartPoints.Where(point => correctionStart  <= point.X && point.X <= correctionEnd).ToList();
                var correctedPoints = new PolynomialFitCalculator().GetCorrectedPoints(pointsToBeCorrected, batchPoints, degree);
                ret.AddRange(correctedPoints);
            }
            return ret;
        }
        
        static List<List<Point>> SplitList(List<Point> points, int batchSize)
        {
            var batches = new List<List<Point>>();
            for (int i = 0; i < points.Count; i += batchSize)
            {
                var batch = points.GetRange(i, Math.Min(batchSize, points.Count - i));
                batches.Add(batch);
            }
            return batches;
        }
    }
}