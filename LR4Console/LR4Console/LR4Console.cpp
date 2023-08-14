#include <iostream>
#include <windows.h>
#include <Shlwapi.h>
#pragma comment(lib, "Shlwapi.lib")
#include <tlhelp32.h>


BOOL ListProcessModules(DWORD dwPID);
BOOL ListProcessThreads(DWORD dwOwnerPID);

void PrintProcInfo()
{
    // Выводит данные о текущем процессе
    char filepath[256];
    HMODULE moduleHandle;
    GetModuleFileNameA(NULL, filepath, 256);
    moduleHandle = GetModuleHandleA(NULL);
    LPCSTR filename = PathFindFileNameA(filepath);
    // Вывод
    std::cout << "Имя файла: " << filename << "\n";
    std::cout << "Путь к файлу: " << filepath << "\n";
    std::cout << "Дескриптор файла: " << moduleHandle << "\n";
}

void PrintProcInfo(HMODULE moduleHandle)
{
    // Выводит данные по дескриптору модуля
    char filepath[256];
    GetModuleFileNameA(moduleHandle, filepath, 256);
    LPCSTR filename = PathFindFileNameA(filepath);

    // Вывод
    std::cout << "Имя файла: " << filename << "\n";
    std::cout << "Путь к файлу: " << filepath << "\n";
    std::cout << "Дескриптор файла: " << moduleHandle << "\n";
}

void PrintProcInfo(LPSTR moduleName)
{
    // Выводит данные по имени модуля
    char filepath[256];
    HMODULE moduleHandle;
    moduleHandle = GetModuleHandleA(moduleName);
    GetModuleFileNameA(moduleHandle, filepath, 256);
    LPCSTR filename = PathFindFileNameA(filepath);

    // Вывод
    std::cout << "Имя файла: " << filename << "\n";
    std::cout << "Путь к файлу: " << filepath << "\n";
    std::cout << "Дескриптор файла: " << moduleHandle << "\n";
}

int main()
{
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);
    // Пункт 1, принимаются дескриптор, имя модуля или полное имя, выводятся два других элемента
    const char* moduleName_1 = "Shlwapi.dll";
    const char* moduleName_2 = "kernel32.dll";
    HMODULE moduleHandle_2 = GetModuleHandleA(moduleName_2);
    PrintProcInfo();
    std::cout << "\n";
    PrintProcInfo((LPSTR)moduleName_1);
    std::cout << "\n";
    PrintProcInfo(moduleHandle_2);
    std::cout << "\n" << "\n";

    // Пункт 2

    DWORD currentProcID = GetCurrentProcessId();
    HANDLE currentProcHandle = GetCurrentProcess();
    HANDLE copyHandle;
    DuplicateHandle(currentProcHandle, currentProcHandle, currentProcHandle, &copyHandle, PROCESS_ALL_ACCESS, false, 0);
    HANDLE currentProcHandle_1 = OpenProcess(PROCESS_ALL_ACCESS, false, currentProcID);

    // Вывод
    std::cout << "ID: " << currentProcID << "\n";
    std::cout << "Псевдо-дескриптор:  " << currentProcHandle << "\n";
    std::cout << "Дескриптор, полученный с помощью функции DuplicateHandle: " << copyHandle << "\n";
    std::cout << "Дескриптор, полученный с помощью функции OpenProcess: " << currentProcHandle_1 << "\n";
    std::cout << "Закрытие дескриптора, полученного с помощью функции OpenProcess: " << currentProcHandle_1 << "\n";
    std::cout << "Закрытие дескриптора, полученного с помощью функции DuplicateHandle: " << copyHandle << "\n";

    CloseHandle(currentProcHandle_1);
    CloseHandle(copyHandle);
    system("pause");

    // Пункт 3
    PROCESSENTRY32 pe;
    HANDLE tl32snap = INVALID_HANDLE_VALUE;
    tl32snap = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
    if (tl32snap == INVALID_HANDLE_VALUE)
        exit(1);

    pe.dwSize = sizeof(PROCESSENTRY32);
    if (!Process32First(tl32snap, &pe))
    {
        exit(1);
    }

    do 
    {
        std::cout << "ID: " << pe.th32ProcessID << "\n" ;
        std::cout << "Кол-во потоков: " << pe.cntThreads << "\n";
        std::cout << "ID родительского процесса: " << pe.th32ParentProcessID << "\n";
        ListProcessModules(pe.th32ProcessID);
        ListProcessThreads(pe.th32ProcessID);

    } while (Process32Next(tl32snap, &pe));

    CloseHandle(tl32snap);

    system("pause");
}


BOOL ListProcessModules(DWORD dwPID)
{
    HANDLE hModuleSnap = INVALID_HANDLE_VALUE;
    MODULEENTRY32 me32;
    hModuleSnap = CreateToolhelp32Snapshot(TH32CS_SNAPMODULE, dwPID);
    if (hModuleSnap == INVALID_HANDLE_VALUE)
        return(FALSE);

    me32.dwSize = sizeof(MODULEENTRY32);
    if (!Module32First(hModuleSnap, &me32))
    {
        CloseHandle(hModuleSnap);
        return(FALSE);
    }
    
    do
    {
        std::wcout << "Имя модуля: " << me32.szModule << "\n";
        std::wcout << "Исполняемый файл: " << me32.szExePath << "\n";
        std::cout << "ID: " << me32.th32ProcessID << "\n";
        std::cout << "Кол-во ссылок (g): " << me32.GlblcntUsage << "\n";
        std::cout << "Кол-во ссылок (p): " << me32.ProccntUsage << "\n";
        std::cout << "Базовый адрес: " << (DWORD)me32.modBaseAddr << "\n";
        std::cout << "Базовый размер: " << me32.modBaseSize << "\n";
        std::cout << "\n";

    } while (Module32Next(hModuleSnap, &me32));

    CloseHandle(hModuleSnap);
    return(TRUE);
}

BOOL ListProcessThreads(DWORD dwOwnerPID)
{
    HANDLE hThreadSnap = INVALID_HANDLE_VALUE;
    THREADENTRY32 te32; 
    hThreadSnap = CreateToolhelp32Snapshot(TH32CS_SNAPTHREAD, 0);
    if (hThreadSnap == INVALID_HANDLE_VALUE)
        return(FALSE);

    te32.dwSize = sizeof(THREADENTRY32);
    if (!Thread32First(hThreadSnap, &te32))
    {
        CloseHandle(hThreadSnap);
        return(FALSE);
    }

    do
    {
        if (te32.th32OwnerProcessID == dwOwnerPID)
        {
            std::cout << "ID потока: " << te32.th32ThreadID << "\n";
            std::cout << "Базовый приоритет: " << te32.tpBasePri << "\n";
            std::cout << "Дельта приоритет: " << te32.tpDeltaPri << "\n";
            std::cout << "\n";
        }
    } while (Thread32Next(hThreadSnap, &te32));

    CloseHandle(hThreadSnap);
    return(TRUE);
}
