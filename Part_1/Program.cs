using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//1.    Сформировать массив случайных целых чисел (размер  задается пользователем). Вычислить сумму чисел массива и максимальное число в массиве.
//Реализовать  решение  задачи  с  использованием  механизма  задач продолжения.
namespace Part_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bведите размерность массива");
            int n = Convert.ToInt32(Console.ReadLine());
            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Func<Task<int[]>, int[]> func2 = new Func<Task<int[]>, int[]>(GetResultArray);
            Task<int[]> task2 = task1.ContinueWith<int[]>(func2);

            Action<Task<int[]>> action = new Action<Task<int[]>>(PrintResult);
            Task task3 = task2.ContinueWith(action);

            task1.Start();
            Console.ReadKey();
        }

        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = rnd.Next(0, 100);
            }
            return array;
        }
        static int[] GetResultArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int[] result = new int[2];
            int sum = 0;
            int max = 0;
            int z = array.Length;
            for (int i = 0; i < z; i++)
            {
                sum = sum + array[i];
            }
            for (int i = 0; i < z; i++)
            {
                if (max < array[i])
                {
                    max = array[i];
                }
            }
            result[0] = sum;
            result[1] = max;
            Console.WriteLine("Исходный массив");
            for (int i = 0; i < z; i++)
            {
                Console.Write("{0} ", array[i]);
            }
            Console.WriteLine();
            return result;
        }
        static void PrintResult(Task<int[]> task)
        {
            int[] result = task.Result;
            Console.WriteLine();
            Console.WriteLine("Сумма чисел масива = {0}", result[0]);
            Console.WriteLine("Максимальное значение = {0}", result[1]);

        }
    }
}
