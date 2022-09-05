#include <iostream>

using namespace std;

int main()
{
    int n, first_symbol, last_symbol;
    bool space_here = false;
    char S[1000000] = "I like to eat dumplings with and without mayonnaise                    ";
    
    cout << "Укажите необходимую длину строки: ";
    cin >> n;
    
    if (n > 1000000 | n < 0) {
        cout << "Программа способна работать с числом символов от 0 до 100000"; // Проверка на принадлежность диапазону
        return 0;
    }

    for (int i = sizeof(S) - 1; i >= 0; i--) {                                  // Определяю где находится конец массива с учётом пробелов
        if (S[i] != ' ' && S[i] != '\0') {
            last_symbol = i;
            break;
        }
    }
    S[last_symbol + 1] = '\0';                                                  // Ограничиваю массив символов
    
    if (n < last_symbol + 1) {
        for (int i = 0; i <= last_symbol; i++) {
            if (S[i] == ' ') {                                                  // Убираю по 1 первому слову, проверяя, не достаточно ли обрезал массив
                first_symbol = i + 1;
                if (last_symbol - first_symbol + 1 <= n) {
                    break;
                }
            }
            if (i == last_symbol) {
                cout << "Были удалены все слова, пожалуйста, укажите большее число символов" << endl;
                return 0;
            }
        }
        
        int j = 0;
        for (int i = first_symbol; i <= last_symbol ; ++i) {                    // Перезаписываю символы с необходимым сдвигом
            S[j++] = S[i];
        }
        
        S[j] = '\0';                                                            // Ограничиваю массив символов после среза
        last_symbol = j - 1;
    }
    
    int k = 0;
    while (S[k] != '\0') {                                                      // Проверяю, остались ли пробелы
        if (S[k] == ' ') {
            space_here = true;
            break;
        }
        k++;
    }
    
    if (!space_here) {                                                          // Если пробелов нет, значит, осталось лишь одно слово, с которым дальше не имеет смысла работать
        cout << "В тексте осталось лишь одно слово: " << S << endl;
        return 0;
    }
    
    
    if (n != last_symbol + 1) {
        int spaces = 1;
        while (n != last_symbol + 1) {
            int space_ind = 0;
            for (int i = 0; i <= last_symbol; i++) {
                if (S[i] == ' ') {                                              // Нахожу место, куда необходимо вставить пробел
                    if (S[i + spaces] != ' ') {
                        space_ind = i + spaces;
                        break;
                    }
                    else {
                        i += spaces;
                    }
                }
            }
            
            if (space_ind){
                last_symbol++;
                for (int i = last_symbol; i >= space_ind; i--) {                // Сдвигаю всё, что справа от вставляемого пробела, вправо
                    S[i+1] = S[i];
                }
                S[space_ind] = ' ';                                             // Записываю пробел
            }
            else {
                spaces++;
            }
        }
    }
    
    cout << S << endl;
    
    
    return 0;
}