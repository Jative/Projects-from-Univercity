#include <vector>
#include <stdio.h>
#ifndef test_hpp
#define test_hpp

using namespace std;

typedef struct
{
    int hours;
    int minute;
    int seconds;
} timestuct;

void add_struct(vector<timestuct> &mass, timestuct comch);
void change_struct(vector<timestuct> &mass, int i);
void delete_struct(vector<timestuct> &mass, int i);
void show(vector<timestuct> &mass);
void compare(vector<timestuct> &mass, int i, int j);

#endif
