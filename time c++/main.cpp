#include <iostream>
#include <vector>
#include "test.hpp"

using namespace std;


int main() {
    int i, j, command;
    vector<timestuct> complexmas;
    
    cout << "Список команд:" << endl;
    cout << "1: Добавить время" << endl;
    cout << "2: Изменить время по индексу" << endl;
    cout << "3: Удалить время по индексу" << endl;
    cout << "4: Вывод всех структур" << endl;
    cout << "5: Найти разность времени по индексам" << endl;
    cout << "0: Выход из программы" << endl;
    
    while (true) {
        cout << endl;
        cout<<"Введите команду: ";
        cin >> command;
        
        if (command == 1) {
            timestuct new_struct;
            cout << "Введите Часы: ";
            cin >> new_struct.hours;
            cout << "Введите Минуты: ";
            cin >> new_struct.minute;
            cout << "Введите Секунды: ";
            cin >> new_struct.seconds;
            
            add_struct(complexmas, new_struct);
        }
        
        else if (command == 2) {
            cout << "Введите индекс: ";
            cin >> i;
            
            change_struct(complexmas, i);
        }
        
        else if (command == 3) {
            cout << "Введите индекс: ";
            cin >> i;
            
            delete_struct(complexmas, i);
        }
        
        else if (command == 4) {
            show(complexmas);
        }
        
        else if (command == 5) {
            cout << "Введите индекс первого времени: ";
            cin >> i;
            cout << "Введите индекс второго времени: ";
            cin >> j;
            
            compare(complexmas, i, j);
        }
        
        else if (command == 0) {
            cout << "Программа завершена" << endl;
            break;
        }
        
        else {
            cout << "Неизвестная команда" << endl;
        }
    }


    return 0;
}
