#include <iostream>

int main()
{
	std::cout << "Enter the length of array: ";
	int n;
	std::cin >> n;
	short int a[100];
	short int b[100] = { 0 };
	
	for (int i = 0; i < n; ++i)
	{
		std::cin >> a[i];
	}

	std::cout << std::endl;

	_asm
	{
		mov esi, 0; ����� �������
		mov ecx, n; �������� �������� �����

		L : ; ����� ������ �����
			;xor ax, ax; �������� ������� ax
			mov ax, a[esi]; ���������� � ������� ������� �������
			mov bx, 1101101101101101b; ���������� � ������� �����, � �������� ������ ������ ��� �������
			and ax, bx; �������� ������ ������ ��� �� 0
			mov b[esi], ax; ���������� � ������ ��������� ��������

			; �������������� �������, ����� ��������� �� 1, ���� 2 ����������
			inc esi
			inc esi
		loop L
	};
	
	std::cout << "Result\n";
	for (int i = 0; i < n; ++i)
	{
		std::cout << b[i] << std::endl;
	}
	return 0;
}

/*
������  �����  �����.
����������  ����  ��������  ��  ������  ������,
������� ������ ����� �� �������� �� 0.
*/