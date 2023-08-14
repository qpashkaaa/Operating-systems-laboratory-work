using LR5Console;
using System.Collections.Generic;
using System.Threading;

// Точка входа в программу
object locker = new();  // объект-заглушка
List list = new List();

void Print()
{
    while (list.checkFullLoading() == false)
    {
        lock (locker)
        {
            list.appName = $"{Thread.CurrentThread.Name}: ";
            list.addElement();
            Thread.Sleep(100);
        }
    }
}

// запускаем пять потоков
for (int i = 1; i < 6; i++)
{
    Thread myThread = new(Print);
    myThread.Name = $"Приложение {i}";
    myThread.Start();
}
