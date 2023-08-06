namespace ConsoleTestTask.Solutions
{
    using System;
    using System.Numerics;
    using ConsoleTestTask.Util;

    internal class Matrix
    {
        private static Random random = new Random();

        public static void GetMainDiagonalSum()
        {
            int[,] matrix = CreateMatrix();
            PrintMatrix(matrix);

            BigInteger diagonalSum = BigInteger.Zero;
            int diagonalLength = matrix.GetLength(0) > matrix.GetLength(1) ? matrix.GetLength(1) : matrix.GetLength(0);
            for (int i = 0; i < diagonalLength; i++)
            {
                diagonalSum += matrix[i, i];
            }
            Console.WriteLine($"Сумма главной диагонали: {diagonalSum}.");
            InputHandler.PauseForAnyKey();
        }
        public static void GetSumOfNumbersMultipleOfThree()
        {
            int[,] matrix = CreateMatrix();
            PrintMatrix(matrix);

            BigInteger resultSum = BigInteger.Zero;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] % 3 == 0)
                    {
                        resultSum += matrix[i, j];
                    }
                }
            }
            Console.WriteLine($"Сумма чисел, кратных 3: {resultSum}.");
            InputHandler.PauseForAnyKey();
        }

        private static void PrintMatrix(int[,] matrix)
        {
            Console.WriteLine($"Матрица {matrix.GetLength(0)},{matrix.GetLength(1)}");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.WriteLine("[");
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($" {matrix[i, j]} ");
                }
                Console.WriteLine("]");
            }
        }

        private static int[,] CreateMatrix()
        {
            Console.WriteLine("Введите размерность матрицы m,n");
            int m = InputHandler.GetInt("m: ", 1, int.MaxValue);
            int n = InputHandler.GetInt("n: ", 1, int.MaxValue);
            int[,] matrix = new int[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = random.Next();
                }
            }
            return matrix;
        }
    }
}