using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using Rhino.Geometry;

namespace GraphModel
{

    public class KrigeInterpolation
    {
        private double nugget;
        private double sill;
        private double range;
        private Matrix<double> covarianceMatrix;

        public KrigeInterpolation(double nugget, double sill, double range)
        {
            this.nugget = nugget;
            this.sill = sill;
            this.range = range;
        }

        public List<double> PerformInterpolation(List<Point3d> points)
        {
            int n = points.Count;
            Matrix<double> semivariogramMatrix = Matrix<double>.Build.Dense(n, n);

            // 计算半方差矩阵
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    double distance = Math.Sqrt(Math.Pow(points[i].X - points[j].X, 2) + Math.Pow(points[i].Y - points[j].Y, 2));
                    double semivariance = Semivariance(distance);
                    semivariogramMatrix[i, j] = semivariance;
                }
            }

            // 构建克里金插值矩阵
            Matrix<double> krigingMatrix = Matrix<double>.Build.Dense(n + 1, n + 1);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    krigingMatrix[i, j] = semivariogramMatrix[i, j];
                }
                krigingMatrix[i, n] = 1;
                krigingMatrix[n, i] = 1;
            }
            krigingMatrix[n, n] = 0;

            // 计算权重
            Vector<double> weights = krigingMatrix.Solve(Vector<double>.Build.Dense(n + 1, 1));
            List<double> heights = new List<double>();

            // 执行插值并进行分级
            foreach (Point3d point in points)
            {
                double interpolatedHeight = 0;
                for (int i = 0; i < n; i++)
                {
                    double distance = Math.Sqrt(Math.Pow(point.X - points[i].X, 2) + Math.Pow(point.Y - points[i].Y, 2));
                    double semivariance = Semivariance(distance);
                    interpolatedHeight += weights[i] * (points[i].Z - weights[n] + weights[n] * point.Z) * semivariance;
                }

                // 进行高度分级，根据具体需求编写分级逻辑
                // ...

                // 输出插值结果和分级结果
                Console.WriteLine($"Point ({point.X}, {point.Y}) - Interpolated Height: {interpolatedHeight}, Grade: ...");
                heights.Add(interpolatedHeight);

            }
            return StandardDeviationClassification(heights, 5);
        }


        private List<double> StandardDeviationClassification(List<double> heights, int numClasses)
        {
            List<double> classifications = new List<double>();

            // 计算高度数据的平均值
            double mean = heights.Average();

            // 计算高度数据的标准差
            double stdDev = CalculateStandardDeviation(heights, mean);

            // 计算每个分级的标准差范围
            double stdDevRange = stdDev / numClasses;

            // 对每个高度值进行分级
            foreach (double height in heights)
            {
                // 计算高度值与平均值的标准差倍数
                double deviation = (height - mean) / stdDev;

                // 计算当前高度值所在的分级
                int classification = (int)Math.Floor(deviation / stdDevRange);

                // 将分级添加到结果列表中
                classifications.Add(classification);
            }

            return classifications;
        }

        private double CalculateStandardDeviation(List<double> heights, double mean)
        {
            double sumOfSquaredDifferences = 0;

            // 计算每个高度值与平均值的差值的平方和
            foreach (double height in heights)
            {
                double difference = height - mean;
                sumOfSquaredDifferences += difference * difference;
            }

            // 计算平均差值的平方根，即标准差
            double stdDev = Math.Sqrt(sumOfSquaredDifferences / heights.Count);

            return stdDev;
        }
        private double Semivariance(double distance)
        {
            if (distance == 0)
            {
                return 0;
            }
            else if (distance <= range)
            {
                return nugget + sill * (1.5 * (distance / range) - 0.5 * Math.Pow(distance / range, 3));
            }
            else
            {
                return nugget;
            }
        }
    }
  }
