using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.MemoryMappedFiles;
using System.Diagnostics;

namespace WriteMemoryAp
{
    class Message
    {
        private int size = 1;
        private int disableRepeat = 0;

        public void sendMessage(char[] message)
        {
   
            int size = message.Length;
            MemoryMappedFile sharedMemory = MemoryMappedFile.CreateOrOpen("MemoryFile", size * 2 + 4);
            using (MemoryMappedViewAccessor writer = sharedMemory.CreateViewAccessor(0, size * 2 + 4))
            {
                writer.Write(0, size);
                writer.WriteArray<char>(4, message, 0, message.Length);
            }
            Console.WriteLine("Сообщение записано в разделяемую память и отправлено всем активным процессам \n");
            Thread.Sleep(1000);
            Console.WriteLine("----------Введите сообщение----------");
            sendMessage(Console.ReadLine().ToCharArray());
        }

        public void getMessage()
        {
            char[] message;
            try
            {
                MemoryMappedFile sharedMemory = MemoryMappedFile.OpenExisting("MemoryFile");
                using (MemoryMappedViewAccessor reader = sharedMemory.CreateViewAccessor(0, 4, MemoryMappedFileAccess.Read))
                {
                    size = reader.ReadInt32(0);
                }
                using (MemoryMappedViewAccessor reader = sharedMemory.CreateViewAccessor(4, size * 2, MemoryMappedFileAccess.Read))
                {
                    message = new char[size];
                    reader.ReadArray<char>(0, message, 0, size);
                }
                if (disableRepeat == size)
                {
                    Thread.Sleep(500);
                    getMessage();
                }
                else
                {
                    Console.WriteLine("Получено сообщение из MainProcess(SendMessage/LR6Console.exe) :");
                    Console.WriteLine(message);
                    Console.WriteLine("----------Ожидание следующего сообщения----------");
                    disableRepeat = size;
                    Thread.Sleep(500);
                    getMessage();
                }
            }
            catch
            {
                Thread.Sleep(500);
                getMessage();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //SendMessage
            Console.Title = "Основное приложение, отправляющее сообщения";
            Console.SetWindowSize(50, 20);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            using Process process = new Process();
            {
                process.StartInfo.FileName = "LR6Console.exe";
                process.StartInfo.WorkingDirectory = "../GetMessageFirst/";
                process.StartInfo.UseShellExecute = true;
                process.Start();
            };
            process.StartInfo.WorkingDirectory = "../GetMessageSecond/";
            process.Start();

            process.StartInfo.WorkingDirectory = "../GetMessageThird/";
            process.Start();

            Console.WriteLine("----------Введите сообщение----------");
            Message message = new Message();
            message.sendMessage(Console.ReadLine().ToCharArray());

            //GetMessage
            /*Console.Title = "Приложение, принимающее сообщения";
            Console.SetWindowSize(50, 20);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine("----------Ожидается получение сообщения----------");
            Message message = new Message();
            message.getMessage();*/
        }
    }
}