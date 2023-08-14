#include <iostream>
#include <conio.h>
#include <windows.h>
#include <tlhelp32.h>
#include <iomanip>
using namespace std;

void processList() {
	HANDLE ProcSnap = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);

	PROCESSENTRY32W pe = { 0 };
	pe.dwSize = sizeof(pe);
	if (Process32FirstW(ProcSnap, &pe)) {
		do {
			wcout << L"ID: " << pe.th32ProcessID
				<< L" - process name: " << pe.szExeFile << endl;
		} while (Process32NextW(ProcSnap, &pe));
	}
}
void coutAddress(DWORD address) {
	cout << hex << "0x" << setfill('0') << setw(8) << address << setfill(' ') << dec;
}

void getMapProcess(DWORD id) {
	HANDLE hHandle = OpenProcess(PROCESS_QUERY_INFORMATION, FALSE, id);
	MEMORY_BASIC_INFORMATION mbi;

	DWORD finAddress = 0x7FFFFFFF;
	for (DWORD bAddress = 0; bAddress < 0x7FFFFFFF; bAddress += mbi.RegionSize) {
		VirtualQueryEx(hHandle, (void*)bAddress, &mbi, sizeof(mbi));
		cout << "\nТекущий адрес: "; coutAddress(bAddress); cout << "\tБазовый адрес области: ";
		coutAddress((DWORD)mbi.BaseAddress);
		cout << "\nРазмер области:"; cout << hex << "0x" << mbi.RegionSize << dec;
		if (mbi.State != MEM_FREE) {
			cout << "\nБазовый адрес выделенной области: "; coutAddress((DWORD)mbi.AllocationBase);
		}

		switch (mbi.State) {
		case MEM_FREE:
			cout << "\nState: Free\n";
			break;
		case MEM_RESERVE:
			cout << "\nState: Reserve;\tType: ";
			switch (mbi.Type) {
			case MEM_IMAGE:
				cout << "Image";
				break;
			case MEM_MAPPED:
				cout << "Mapped";
				break;
			case MEM_PRIVATE:
				cout << "Private";
				break;
			default:
				cout << "Unknown";
				break;
			}
			cout << "\n";
			break;
		case MEM_COMMIT:
			cout << "\nState: Reserve;\tType: ";
			switch (mbi.Type) {
			case MEM_IMAGE:
				cout << "Image";
				break;
			case MEM_MAPPED:
				cout << "Mapped";
				break;
			case MEM_PRIVATE:
				cout << "Private";
				break;
			default:
				cout << "Unknown";
				break;
			}
			break;
			cout << "\nProtect: ";
			switch (mbi.Protect) {
			case PAGE_EXECUTE:
				cout << "Execute";
				break;
			case PAGE_NOACCESS:
				cout << "No access";
				break;
			case PAGE_READONLY:
				cout << "Read only";
				break;
			case PAGE_READWRITE:
				cout << "Read/write";
				break;
			case PAGE_WRITECOPY:
				cout << "Writecopy";
				break;
			default:
				cout << "Unknown";
				break;
			}
			break;
			cout << "\n";
		default:
			cout << "\nState: Unknown\n";
			break;
		}

		if (tolower(_getch()) == 'q')
			break;
	}
}

void main()
{
	setlocale(LC_ALL, "rus");
	int id;
	system("color F0");
	system("cls");
	processList();
	cout << "Введите ID:";
	cin >> id;
	if (id)
		getMapProcess(id);
	cout << "Карта процесса успешно построена" << endl;
	system("pause");
}
