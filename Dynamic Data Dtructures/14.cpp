#include "14.h"
#include <string>
#include <iostream>
using namespace std;

void mopr(INFO*& BegQ)
{
	INFO* p = BegQ;
	int mas[12];
	for (int i = 0; i < 12; i++) mas[i] = 0;
	while (p)
	{
		if (p->date.substr(6) == "2020")
		{
			mas[stoi(p->date.substr(3, 2)) - 1] += 1;
		}
		p = p->next;
	}
	for (int i = 0; i < 12; i++)
	{
		int j = i + 1;
		cout << "Номер месяца: " << j << endl;
		cout << "Количество: " << mas[i] << endl;
		cout << endl;
	}
}

void vivo(AVAR*& top)
{
	AVAR* p = top;
	while (p)
	{
		cout << "Место: " << p->place << endl;
		cout << "Средняя цена: " << p->srzn << endl;
		p = p->next;
	}
}

void dell2(AVAR*& top)
{
	while (1)
	{
		AVAR* p = top;
		if (!p) return;
		top = top->next;
		delete p;
	}

}

void srzn(INFO*& BegQ, AVAR*& top)
{
	INFO* p = BegQ;
	string strs[30];
	int n = 0;
	if (!BegQ)
	{
		cout << "Очередь пуста" << endl;
		return;
	}
	while (p)
	{
		bool b = 1;
		for (int i = 0; i < n; i++) if (strs[i] == p->place) b = 0;
		if (b)
		{
			strs[n] = p->place;
			n++;
		}
		p = p->next;
	}
	for (int i = 0; i < n; i++)
	{
		AVAR* p1 = new AVAR;
		p = BegQ;
		double sum = 0;
		int j = 0;
		while (p)
		{
			if (p->place == strs[i])
			{
				sum += p->cost;
				j += 1;
			}
			p = p->next;
		}
		p1->place = strs[i];
		p1->srzn = sum / j;
		p1->next = top;
		top = p1;
	}
}

void mpnt(INFO*& BegQ)
{
	INFO* p = BegQ;
	string strs[30];
	int n = 0;
	if (!BegQ)
	{
		cout << "Очередь пуста" << endl;
		return;
	}
	while (p)
	{
		if (stoi(p->date.substr(6)) == 2019 && stoi(p->date.substr(3, 2)) <= 6 && stoi(p->date.substr(3, 2)) > 3)
		{
			bool b = 1;
			for (int i = 0; i < n; i++) if (strs[i] == p->place) b = 0;
			if (b)
			{
				strs[n] = p->place;
				n++;
			}
		}
		p = p->next;
	}
	if (n == 0)
	{
		cout << "Подходящих эл-ов нет" << endl;
		return;
	}
	for (int i = 0; i < n; i++)
	{
		p = BegQ;
		cout << strs[i] << " - ";
		while (p)
		{
			if (stoi(p->date.substr(6)) == 2019 && stoi(p->date.substr(3, 2)) <= 6 && stoi(p->date.substr(3, 2)) > 3 && p->place == strs[i]) cout << p->name << '\t';
			p = p->next;
		}
		cout << endl;
	}
	cout << endl;
}

void vivo1(INFO*& BegQ)
{
	INFO* p = BegQ;
	int i = 0;
	if (!BegQ)
	{
		cout << "Очередь пуста" << endl;
		return;
	}
	while (p)
	{
		cout << "Номер в очереди: " << i++ << endl;
		cout << "Название: " << p->name << endl;
		cout << "Место: " << p->place << endl;
		cout << "Цена: " << p->cost << endl;
		cout << "Дата: " << p->date << endl;
		p = p->next;
	}
}

void dell(INFO*& BegQ, INFO*& EndQ, int& n)
{
	INFO* p = BegQ;
	if (!EndQ)
	{
		cout << "Очередь пуста" << endl;
		return;
	}
	BegQ = BegQ->next;
	delete p;
	if (!BegQ) EndQ = NULL;
	n--;
	cout << endl;
}

bool prove(string data)
{
	string num = "1234567890";
	string s;
	if (data.find('.') != string::npos)
	{
		s = data.substr(0, data.find('.'));
		if (size(s) != 2 || num.find(s[0]) == string::npos || num.find(s[1]) == string::npos) return 0;
		if (stoi(s) > 31 || stoi(s) < 0) return 0;
		data = data.substr(data.find('.') + 1);
	}
	else return 0;
	if (data.find('.') != string::npos)
	{
		s = data.substr(0, data.find('.'));
		if (size(s) != 2 || num.find(s[0]) == string::npos || num.find(s[1]) == string::npos) return 0;
		if (stoi(s) > 12 || stoi(s) < 0) return 0;
		data = data.substr(data.find('.') + 1);
	}
	else return 0;
	if (size(data) != 4 || num.find(data[0]) == string::npos || num.find(data[1]) == string::npos || num.find(data[2]) == string::npos || num.find(data[3]) == string::npos) return 0;
	if (stoi(data) > 2021 || stoi(data) < 2000) return 0;
	return 1;
}

void dobav(INFO*& BegQ, INFO*& EndQ, int& n)
{
	INFO* p = new INFO;
	p->num = n;
	cout << "Введите название: ";
	cin >> p->name;
	cout << endl;
	cout << "Введите место покупки: ";
	cin >> p->place;
	cout << endl;
	cout << "Введите цену: ";
	cin >> p->cost;
	cout << endl;
	cout << "Введите дату покупки: ";
	cin >> p->date;
	cout << endl;
	p->next = NULL;
	if (p->cost < 0 || !(prove(p->date)))
	{
		cout << "Неверные данные" << endl;
		p = NULL;
		delete p;
		return;
	}
	if (!EndQ) BegQ = p;
	else EndQ->next = p;
	EndQ = p;
	n++;
	cout << endl;
	p = NULL;
	delete p;
}

void izme(INFO*& BegQ, INFO*& EndQ, int n)
{
	if (n != 0)
	{
		int nn;
		INFO* p = new INFO;
		cout << "Введите номер структуры для редактирования: ";
		cin >> nn;
		cout << endl;
		if (nn <= n)
		{
			p->num = n;
			cout << "Введите название: ";
			cin >> p->name;
			cout << endl;
			cout << "Введите место покупки: ";
			cin >> p->place;
			cout << endl;
			cout << "Введите цену: ";
			cin >> p->cost;
			cout << endl;
			cout << "Введите дату покупки: ";
			cin >> p->date;
			cout << endl;
			if (p->cost < 0 || !(prove(p->date))) cout << "Неверные данные" << endl;
			else
			{
				INFO* p2 = new INFO;
				p2 = BegQ;
				if (nn == 0)
				{
					if (n == 1)
					{
						BegQ->num = p->num;
						BegQ->name = p->name;
						BegQ->place = p->place;
						BegQ->cost = p->cost;
						BegQ->date = p->date;
						EndQ = BegQ;
						cout << endl;
					}
					else
					{
						BegQ->num = p->num;
						BegQ->name = p->name;
						BegQ->place = p->place;
						BegQ->cost = p->cost;
						BegQ->date = p->date;
						cout << endl;
					}
				}
				else
				{
					for (int i = 1; i < n; i++)
					{
						p2 = p2->next;
						if (i == nn)
						{
							p2->num = p->num;
							p2->name = p->name;
							p2->place = p->place;
							p2->cost = p->cost;
							p2->date = p->date;
							break;
						}

					}

					cout << endl;
				}
				p2 = NULL;
				delete p2;
			}
		}
		else cout << "Такого элемента нет" << endl;
		p = NULL;
		delete p;
	}
	else cout << "Нет значений для изменения" << endl;
}