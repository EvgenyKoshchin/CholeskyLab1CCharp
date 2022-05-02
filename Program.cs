using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CholeskyLab1CCharp
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] threadsNum = { 2, 4, 6, 8 };

            int[] matrixDimension = { 10, 100, 1000, 3000, 6000 };

            foreach (int i in matrixDimension)
            {

                Console.WriteLine("Размерность матрицы: " + i);

                Double[,] matrixA = MatrixBuilder.createSymmetricMatrix(i);
                Double[] vectorB = MatrixBuilder.getRow(matrixA, 0);

                Stopwatch Watch = new Stopwatch();

                Watch.Start();

                Double[] res1 = SolveByCholesky.solveLinearSystem(matrixA, vectorB);

                Watch.Stop();

                Console.WriteLine("Время выполнения последовательного алгоритма: "
                        + Watch.Elapsed);

                foreach (int j in threadsNum)
                {
                    Watch = new Stopwatch();
                    Watch.Start();
                    Double[] res2 = SolveByCholeskyParallel.solveLinearSystemParallel(matrixA, vectorB, j);
                    Watch.Stop();
                    Console.WriteLine("Время выполнения параллельного алгоритма при " + j + " потоках(е)" + ": "
                            + Watch.Elapsed);
                }
                Console.WriteLine("\n");
            }

            System.Console.ReadKey();
        }
    }
}
