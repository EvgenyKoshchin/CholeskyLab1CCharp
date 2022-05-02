using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CholeskyLab1CCharp
{
    class MatrixBuilder
    {
        public static Double[,] transpositionMatrix(Double[, ] matrix)
        {

            int rowsMatrix = MatrixBuilder.getRow(matrix, 0).Length;
            int columnsMatrix = MatrixBuilder.getRow(matrix, 0).Length;
            Double[,] newMatrix = new Double[rowsMatrix, columnsMatrix];

            for (int i = 0; i < rowsMatrix; i++)
            {
                for (int j = 0; j < columnsMatrix; j++)
                {
                    newMatrix[j, i] = matrix[i, j];
                }
            }
            return newMatrix;
        }
        public static Double[,] transpositionMatrixNullable(Double?[,] matrix)
        {

            int rowsMatrix = MatrixBuilder.getRowNullable(matrix, 0).Length;
            int columnsMatrix = MatrixBuilder.getRowNullable(matrix, 0).Length;
            Double[,] newMatrix = new Double[rowsMatrix, columnsMatrix];

            for (int i = 0; i < rowsMatrix; i++)
            {
                for (int j = 0; j < columnsMatrix; j++)
                {
                    newMatrix[j, i] = (double)(matrix[i, j]);
                }
            }
            return newMatrix;
        }
        public static Double[, ] createSymmetricMatrix(int n)
        {

            Double[,] outputMatrix = new Double[n, n];

            int rangeMin = 1;
            int rangeMax = 100;
            Random r = new Random();

            for (int i = 0; i < n; i++)
            {          
                for (int j = i; j < n; j++)
                {
                    double randomValue = r.Next(rangeMin, rangeMax + 1);
                    outputMatrix[i, j] = randomValue;
                }
            }

            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    outputMatrix[i, j] = outputMatrix[j, i];
                }
            }

            return outputMatrix;
        }

        public static Double[] getRow(Double[,] array, int wantedRow)
        {
            int l = array.GetLength(1);
            Double[] row = new Double[l];
            for (int i = 0; i < l; i++)
            {
                row[i] = array[wantedRow, i];
            }
            return row;
        }

        public static Double[] getRowNullable(Double?[,] array, int wantedRow)
        {
            int l = array.GetLength(1);
            Double[] row = new Double[l];
            for (int i = 0; i < l; i++)
            {
                row[i] = (double)(array[wantedRow, i]);
            }
            return row;
        }
    }
}
