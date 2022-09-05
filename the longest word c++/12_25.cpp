#include <iostream>
#include <fstream>
#include <string>

using namespace std;

int main()
{
    string line, the_biggest_word = "", word = "";
    char symbol, symbols[] = " .,!?;-—";
    int word_length_counter = 0, word_counter = 0;
    bool flag;

    ifstream in;
    in.open("12_25.txt");

    if (in.is_open()) {
        while (getline(in, line))
        {
            for (int i = 0; i < line.length(); i++) {
                symbol = line[i];                                            // Получаю каждый символ в строке

                if (symbol == symbols[0]) {                                  // Если символ - пробел, произвожу операции по оценке полученного слова
                    if (word_length_counter > the_biggest_word.length()) {   // Если новое слово больше самого большого из найденных, то самое большое теперь оно
                        the_biggest_word = word;
                        word_counter = 1;
                    }

                    else if (word == the_biggest_word) {                     // Если новое слово соответствует самому большому, то количество его повторений увеличиваю на 1
                        word_counter++;
                    }

                    word = "";                                               // Удаляю предыдущее слово для записи нового
                    word_length_counter = 0;
                }

                else {                                                       // Если символ не пробел
                    flag = false;

                    for (int j = 1; j < sizeof(symbols); j++) {              // Проверяю, не является ли он спец. символом, и если нет, то добавляю его к слову
                        if (symbol == symbols[j]) {
                            flag = true;
                        }
                    }

                    if (flag) {
                        continue;
                    }

                    word += symbol;
                    word_length_counter++;
                }
            }
            if (word_length_counter > the_biggest_word.length()) {            // Проверка последнего слова в строке
                the_biggest_word = word;
                word_counter = 1;
            }

            else if (word == the_biggest_word) {
                word_counter++;
            }

            word = "";                                                        // Обнуляю значения после проверки строки
            word_length_counter = 0;
        }

        cout << "Самое длинное слово: '" << the_biggest_word << "', оно встречается раз: " << word_counter;
    }

    else {
        cout << "Ошибка чтения" << endl;                                      // Вывожу ошибку, если не удалось открыть файл и заверщаю программу
        return 1;
    }


    return 0;
}