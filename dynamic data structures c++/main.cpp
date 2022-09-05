#include <iostream>
#include <windows.h>
#include <string>
#include "14.h"

using namespace std;


int main()
{
	SetConsoleCP(1251);
	SetConsoleOutputCP(1251);
	struct INFO* BegQ = NULL, * EndQ = NULL;
	struct AVAR* top = NULL;
	bool b = 1;
	int n = 0;
	int& ns = n;
	int j;
	cout << "1 - Добавление\n2 - Редактирование\n3 - Удаление\n4 - Вывод на экран\n5 - Места покупки и наименования товаров второго квартала позапрошлого года\n6 - Средняя стоимость покупок, по каждому месту покупки\n7 - Количество покупок за каждый месяц прошлого года\n";
	while (b)
	{
		cout << "Выберете номер действия: ";
		cin >> j;
		cout << endl;
		switch (j)
		{
		case 1: dobav(BegQ, EndQ, ns); break;
		case 2: izme(BegQ, EndQ, n); break;
		case 3: dell(BegQ, EndQ, ns); break;
		case 4: vivo1(BegQ); break;
		case 5: mpnt(BegQ); break;
		case 6:
			dell2(top);
			srzn(BegQ, top);
			vivo(top);
			break;
		case 7: mopr(BegQ); break;
		default: b = 0; break;
		}
	}

}