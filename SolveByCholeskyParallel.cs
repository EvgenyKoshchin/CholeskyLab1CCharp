using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CholeskyLab1CCharp
{
    class SolveByCholeskyParallel
    {
        static void DiagonalElementCalculation(int i, Double[,] a, Double?[,] L)
        {

            lock(L)
            {
                double sum = 0;
                for (int j = 0; j < i; j++)
                {

                    while (L[i, j] == null)
                    {
                        Monitor.Wait(L);
                    }
                    sum += (double)(L[i, j] * L[i, j]);
                }

                L[i, i] = Math.Sqrt(Math.Abs(a[i, i] - sum));
                Monitor.PulseAll(L);
            }
        }

        static void UnderDiagonalElementCalculation(int i, int n, Double[,] a, Double?[,] L)
        {

            lock(L)
            {
                for (int j = i + 1; j < n; j++)
                {
                    double sum = 0.0;
                    for (int p2 = 0; p2 <= i - 1; p2++)
                    {

                        while (L[i, p2] == null
                                && L[j, p2] == null)
                        {
                            Monitor.Wait(L);
                        }
                        sum += (double)(L[i, p2] * L[j, p2]);
                    }

                    while (L[i, i] == null)
                    {
                        Monitor.Wait(L);
                    }

                    L[j, i] = (a[j, i] - sum) / L[i, i];
                    Monitor.PulseAll(L);
                }
            }
        }
        public static Double[] solveLinearSystemParallel(Double[,] matrixA, Double[] vectorB, int threads)
        {

            int matrixDimension = MatrixBuilder.getRow(matrixA, 0).Length;

            Double?[,] matrixL = new Double?[matrixDimension, matrixDimension];

            Double[] vectorX = new Double[matrixDimension];
            Double[] vectorY = new Double[matrixDimension];
            double sum1;
            double sum2;

            ThreadPool.SetMaxThreads(threads, threads);

            for (int i = 0; i < matrixDimension; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback((x) =>
                {
                    DiagonalElementCalculation(i, matrixA, matrixL);
                }));

                ThreadPool.QueueUserWorkItem(new WaitCallback((x) =>
                {
                    UnderDiagonalElementCalculation(i, matrixDimension, matrixA, matrixL);
                }));

            }

            WaitHandle.WaitAll(new ManualResetEvent(false));

            Double[,] transMatrixL = MatrixBuilder.transpositionMatrixNullable(matrixL);

            for (int i = 0; i < matrixDimension; i++)
            {
                sum1 = 0;

                for (int j = 0; j < i; j++)
                {
                    sum1 += (double)(vectorY[j] * matrixL[i, j]);
                }
                vectorY[i] = (double)((vectorB[i] - sum1) / matrixL[i, i]);
            }

            for (int i = matrixDimension - 1; i >= 0; i--)
            {

                sum2 = 0;

                for (int j = matrixDimension - 1; j > i; j--)
                {
                    sum2 += vectorX[j] * transMatrixL[i, j];
                }
                vectorX[i] = (vectorY[i] - sum2) / transMatrixL[i, i];
            }

            return vectorX;
        }
    }
}
