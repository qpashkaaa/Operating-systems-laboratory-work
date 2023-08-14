#include <iostream>
#include <Windows.h>
#include <stdio.h>
#include <ctype.h>

using namespace std;


int main() {
	setlocale(LC_ALL, "Russian");
	HANDLE myFile = CreateFile(TEXT("LR2Console.txt"), GENERIC_READ | GENERIC_WRITE, 0, NULL, OPEN_EXISTING, 0, 0);
	printf("> Дескриптор файла = %d", myFile);
	DWORD myFileSize = GetFileSize(myFile, NULL);
	printf("\n> Размер файла = %d", myFileSize);
	HANDLE myFileMapping = CreateFileMapping(myFile, NULL, PAGE_READWRITE, 0, 0, TEXT("myFileMapping"));
	printf("\n> Дескриптор файлового отображения = %d",  myFileMapping);
	LPVOID myFileMapView = MapViewOfFile(myFileMapping, FILE_MAP_WRITE, 0, 0, 0);
	printf("\n> Отображенная область памяти = %d", myFileMapView);
	printf("\n");

	printf("\n ----------Производится операция упорядочивания букв по алфавиту----------");
	char* myFileMemory = new char[myFileSize];
	CopyMemory(myFileMemory, myFileMapView, myFileSize);
	for (int i = 0; i < myFileSize; i++) {
		for (int j = 0; j < myFileSize - 1; j++) {
			if (int(static_cast<unsigned char>(myFileMemory[j])) > int(static_cast<unsigned char>(myFileMemory[j+1]))) {
				char b = myFileMemory[j]; // создали дополнительную переменную
				myFileMemory[j] = myFileMemory[j + 1]; // меняем местами
				myFileMemory[j + 1] = b; // значения элементов
			}
		}
	}
	
	CopyMemory(myFileMapView, myFileMemory, myFileSize);
	printf("\n Файл успешно отсортирован");
	if (!UnmapViewOfFile(myFileMapView))
		printf("\n !Ошибка, не удалось освободить память!");
	else
		printf("\n> Отображенная память освободилась успешно");
	if (!CloseHandle(myFileMapping))
		printf("\n !Ошибка, не удалось закрыть файловое отображение!");
	else
		printf("\n> Файловое отображение закрыто успешно");
	if(!CloseHandle(myFile))
		printf("\n !Ошибка, не удалось закрыть файл!");
	else
		printf("\n> Файл закрыт успешно");
	printf("\n\n");
	system("pause");
	return 0;
}
