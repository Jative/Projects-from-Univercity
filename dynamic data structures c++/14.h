#pragma once
#include <string>
using namespace std;

struct INFO
{
	int num;
	string name;
	string place;
	double cost;
	string date;
	struct INFO* next;
};

struct AVAR
{
	string place;
	double srzn;
	struct AVAR* next;
};

void mopr(INFO*&);
void vivo(AVAR*&);
void dell2(AVAR*&);
void mpnt(INFO*&);
void srzn(INFO*&, AVAR*&);
bool prove(string);
void dobav(INFO*&, INFO*&, int&);
void izme(INFO*&, INFO*&, int);
void dell(INFO*&, INFO*&, int&);
void vivo1(INFO*&);