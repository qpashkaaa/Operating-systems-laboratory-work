using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR5Console
{
    // Список, который будут заполнять потоки. Список состоит из 5 массивов, заполняются которые от 1 до 10
    // Поток выбирает случайный элемент списка(массив)
    // Как только массив заполнится до 10, работа программы прекращается и выводится тот эл. списка, который быстрее заполнился
    public class List
    {
        public string appName;
        // Элементы списка
        private int[] arr1 = new int[10];
        private int[] arr2 = new int[10];
        private int[] arr3 = new int[10];
        private int[] arr4 = new int[10];
        private int[] arr5 = new int[10];

        // Счётчики для каждого элемента
        private int loading1 = 0;
        private int loading2 = 0;
        private int loading3 = 0;
        private int loading4 = 0;
        private int loading5 = 0;

        private int fullArrID = 0;

        public void addElement()
        {
            Random rnd = new Random();
            int selected = rnd.Next(1, 5);
            if (checkFullLoading() != true)
            {
                Console.WriteLine(appName + " обращается к " + Convert.ToString(selected) + " элементу списка и добавляет значение \n");
                switch (selected)
                {
                    case 1:
                        arr1[loading1] = loading1 + 1;
                        loading1 += 1;
                        Console.WriteLine("1. " + string.Join(" ", arr1) + "\n");
                        break;
                    case 2:
                        arr2[loading2] = loading2 + 1;
                        loading2 += 1;
                        Console.WriteLine("2. " + string.Join(" ", arr2) + "\n");
                        break;
                    case 3:
                        arr3[loading3] = loading3 + 1;
                        loading3 += 1;
                        Console.WriteLine("3. " + string.Join(" ", arr3) + "\n");
                        break;
                    case 4:
                        arr4[loading4] = loading4 + 1;
                        loading4 += 1;
                        Console.WriteLine("4. " + string.Join(" ", arr4) + "\n");
                        break;
                    case 5:
                        arr5[loading5] = loading5 + 1;
                        loading5 += 1;
                        Console.WriteLine("5. " + string.Join(" ", arr5) + "\n");
                        break;
                    default:
                        Console.WriteLine("Случайно выбранное число вышло за пределы кол-ва элементов списка");
                        break;
                }
            }
            else
            {
                Console.ReadKey();
            }
        }

        public bool checkFullLoading()
        {
            if (loading1 == 10)
            {
                fullArrID = 1;
                return true;
            }
            else if (loading2 == 10)
            {
                fullArrID = 2;
                return true;
            }
            else if (loading3 == 10)
            {
                fullArrID = 3;
                return true;
            }
            else if (loading4 == 10)
            {
                fullArrID = 4;
                return true;
            }
            else if (loading5 == 10)
            {
                fullArrID = 5;
                return true;
            }
            return false;
        }
        public void getAllArrays()
        {
            Console.WriteLine(fullArrID + "-й элемент списка заполнен числами от 1 до 10, работа программы прекращается");
            Console.WriteLine("Выводятся все элементы списка: ");
            Console.WriteLine("1. " + string.Join(" ", arr1));
            Console.WriteLine("2. " + string.Join(" ", arr2));
            Console.WriteLine("3. " + string.Join(" ", arr3));
            Console.WriteLine("4. " + string.Join(" ", arr4));
            Console.WriteLine("5. " + string.Join(" ", arr5));
        }
       
    }
}
