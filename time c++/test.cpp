#include "test.hpp"
#include <vector>
#include <iostream>

using namespace std;

void add_struct(vector<timestuct> &mass, timestuct comch){
    mass.push_back(comch);
}

void change_struct (vector<timestuct> &mass, int i){
    if (mass.size() > i) {
        cout << "Такой элемент есть в массиве" << endl;
        cout << "Введите Часы: ";
        cin >> mass[i].hours;
        cout << "Введите Минуты: ";
        cin >> mass[i].minute;
        cout << "Введите Секунды: ";
        cin >> mass[i].seconds;
        cout << "Запись произведена успешно" << endl;
    }
    else {
        cout << "Элемент не найден в массиве" << endl;
    }
}

void delete_struct (vector<timestuct> &mass, int i) {
    if (mass.size() > i) {
        cout << "Такой элемент есть в массиве" << endl;
        auto iter = mass.cbegin(); // указатель на первый элемент
        mass.erase(iter + i);
        cout << "Удалён успешно" << endl;
    }
    else {
        cout << "Элемент не найден в массиве" << endl;
    }
}

void show (vector<timestuct> &mass) {
    for (int i = 0; i < mass.size(); i++) {
        cout << i << " = " << mass[i].hours << ":" << mass[i].minute << ":" << mass[i].seconds << endl;
    }
}

void compare(vector<timestuct> &mass, int i, int j){
    if (mass.size() > i && mass.size() > j) {
        int summ = (mass[i].hours - mass[j].hours) * 3600 + (mass[i].minute - mass[j].minute) * 60 + (mass[i].seconds - mass[j].seconds);
        cout << "Ответ: " << abs(summ) << " сек." << endl;
    }
    else {
        cout << "Элемент не найден в массиве" << endl;
    }
}