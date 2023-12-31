# What It This?
  **Desktop App written in C#, C++. Collection of laboratory works on the subject of operating systems. Department of CAD, TSTU, Tambov**
  >*This desktop application is a collection of laboratory work on the subject of `Operating Systems'. The main tasks of the work are the study of the Win32 API, OS architecture, thread research, InterProcess Communication study, creation of the OS process scheduler emulator. Implemented in order to master the curriculum of the CAD department of TSTU (Tambov).*

## Preview
### Screenshots
_____
![Screenshot_1](https://github.com/qpashkaaa/Operating-systems-laboratory-work/assets/95401099/b2f386ff-8514-4634-bef2-80b1a9aedd9d)
_____
![Screenshot_2](https://github.com/qpashkaaa/Operating-systems-laboratory-work/assets/95401099/b15e9126-b3fc-4883-915e-a6337d1ec78f)
_____
![Screenshot__3](https://github.com/qpashkaaa/Operating-systems-laboratory-work/assets/95401099/e8505d4e-ce9a-4b7e-b7b1-5939d03a9ca4)
_____
![Screenshot__4](https://github.com/qpashkaaa/Operating-systems-laboratory-work/assets/95401099/9866f6bb-35a1-4355-8be0-225a56515a7c)
_____

## Short information about the works
- **Laboratory Work 1**
  >Programming language: C#. In this work, shows the skills of working with the WIN32 API (viewing and editing system information and parameters) are mastered.
- **Laboratory Work 2**
  >Programming language: C++. In this work, shows the skills of working with memory, the File object and file descriptors are given.
- **Laboratory Work 3**
  >Programming language: C#, C++. This work shows the skills of using the GlobalMemoryStatus API object and apps process map PC is compiled.
- **Laboratory Work 4**
  >Programming language: C++. This work shows skills with processes, WIN32API functions, and process descriptors.
- **Laboratory Work 5**
  >Programming language: C#. This work shows the skills of working with threads in C#.
- **Laboratory Work 6**
  >Programming language: C#. This work shows interprocessor interaction inside Windows OS using the MemoryMappedFile object.
- **Laboratory Work 7**
  >Programming language: C#. This program is an emulator of the operating system process scheduler, which implements the principle of priority scheduling with static priorities.

## Features
- **Working with system libraries (WIN 32 API) to get and edit system information.**
```C#
// Определение имени ПК
[DllImport("Kernel32")]
static extern unsafe bool GetComputerName(byte* lpBuffer, long* nSize);

// Определение UserName
[DllImport("advapi32.dll", SetLastError = true)]
static extern bool GetUserName(System.Text.StringBuilder sb, ref Int32 length);
```
```C#
// Определение имени ПК
byte[] buffer = new byte[512];
long sizeNameComputer = buffer.Length;
long* pSize = &sizeNameComputer;
fixed (byte* pBuffer = buffer)
{
  GetComputerName(pBuffer, pSize);
}
System.Text.Encoding textEnc = new System.Text.ASCIIEncoding();
label1.Text = label1.Text + textEnc.GetString(buffer);

// Определение UserName
StringBuilder bufferUserName = new StringBuilder();
int sizeUserName = 512;
GetUserName(bufferUserName, ref sizeUserName);
label2.Text = label2.Text + bufferUserName;
```
- **Working with threads in the C#**
```C#
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

```

## Tech Stack
- **С#**
- **C++**
- **WinForms**

## Requirements
- **NuGet Packages**
  - ```LiveCharts.WinForms.NetCore3```
  - ```System.Data.DataSetExtensions```

## Authors
- [Pavel Roslyakov](https://github.com/qpashkaaa)

## Contacts
- [Portfolio Website](https://portfolio-website-qpashkaaa.vercel.app/)
- [Telegram](https://t.me/qpashkaaa)
- [VK](https://vk.com/qpashkaaa)
- [LinkedIN](https://www.linkedin.com/in/pavel-roslyakov-7b303928b/)

## Year of Development
> *2022*
