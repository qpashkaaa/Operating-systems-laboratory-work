#include <iostream>
#include <Windows.h>
#include <stdio.h>
#include <ctype.h>

using namespace std;


int main() {
	setlocale(LC_ALL, "Russian");
	HANDLE myFile = CreateFile(TEXT("LR2Console.txt"), GENERIC_READ | GENERIC_WRITE, 0, NULL, OPEN_EXISTING, 0, 0);
	printf("> ���������� ����� = %d", myFile);
	DWORD myFileSize = GetFileSize(myFile, NULL);
	printf("\n> ������ ����� = %d", myFileSize);
	HANDLE myFileMapping = CreateFileMapping(myFile, NULL, PAGE_READWRITE, 0, 0, TEXT("myFileMapping"));
	printf("\n> ���������� ��������� ����������� = %d",  myFileMapping);
	LPVOID myFileMapView = MapViewOfFile(myFileMapping, FILE_MAP_WRITE, 0, 0, 0);
	printf("\n> ������������ ������� ������ = %d", myFileMapView);
	printf("\n");

	printf("\n ----------������������ �������� �������������� ���� �� ��������----------");
	char* myFileMemory = new char[myFileSize];
	CopyMemory(myFileMemory, myFileMapView, myFileSize);
	for (int i = 0; i < myFileSize; i++) {
		for (int j = 0; j < myFileSize - 1; j++) {
			if (int(static_cast<unsigned char>(myFileMemory[j])) > int(static_cast<unsigned char>(myFileMemory[j+1]))) {
				char b = myFileMemory[j]; // ������� �������������� ����������
				myFileMemory[j] = myFileMemory[j + 1]; // ������ �������
				myFileMemory[j + 1] = b; // �������� ���������
			}
		}
	}
	
	CopyMemory(myFileMapView, myFileMemory, myFileSize);
	printf("\n ���� ������� ������������");
	if (!UnmapViewOfFile(myFileMapView))
		printf("\n !������, �� ������� ���������� ������!");
	else
		printf("\n> ������������ ������ ������������ �������");
	if (!CloseHandle(myFileMapping))
		printf("\n !������, �� ������� ������� �������� �����������!");
	else
		printf("\n> �������� ����������� ������� �������");
	if(!CloseHandle(myFile))
		printf("\n !������, �� ������� ������� ����!");
	else
		printf("\n> ���� ������ �������");
	printf("\n\n");
	system("pause");
	return 0;
}
