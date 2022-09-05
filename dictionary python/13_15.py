def create_file():
	file_name = input("Введите название глоссария: ")
	glossary = {}
	file = open(file_name + ".txt", "w")
	file.close()
	print(f"Глоссарий создан и сохранён в файле {file_name}.txt")

	return glossary


def add_term(glossary):
	term = input("Введите термин: ")
	interpretation = input("Введите толкование термина: ")
	glossary[term] = interpretation
	print("Запись в глоссарий произведена успешно")

	return glossary


def del_term(glossary):
	term = input("Введите термин: ")
	
	if term in glossary:
		del glossary[term]
		print(f"Термин '{term}' был удалён из глоссария")

	else:
		print("Данного термина нет в глоссарии")

	return glossary


def show_glossary(glossary):
	if glossary:
		for key in glossary:
			print(f"{key}: {glossary[key]}")

	else:
		print("Глоссарий пуст")


def show_interpretation(glossary):
	term = input("Введите термин: ")

	if term in glossary:
		print(glossary[term])

	else:
		print("Данного термина нет в глоссарии")


def save_file(glossary):
	active_file = input("Введите название файла, в который сохраняете глоссарий без '.txt': ") + ".txt"
	write_text = ""

	for key in glossary:
		write_text += f"{key} : {glossary[key]}\n"

	with open(active_file, "w") as file:
		file.write(write_text)

	print("Запись в файл произведена успешно")


def load_file():
	glossary = {}
	file_name = input("Введите название файла, из которого импортируете глоссарий без '.txt': ") + ".txt"

	try:
		with open(file_name, "r") as file:
			for line in file:
				info = line.split(" : ")
				if len(info) != 2:
					continue
				glossary[info[0]] = info [1]

	except:
		print("Данного файла не существует")

	return glossary


def main():
	glossary = {}

	print("""Список команд:
1: Создать глоссарий
2: Добавить термин в глоссарий
3: Удалить термин из глоссария
4: Вывести весь глоссарий
5: Найти толкование термина
6: Сохранить изменения в файле
7: Загрузить глоссарий из файла
0: Выход из программы""")

	while True:
		print()
		command = input("Введите команду: ")

		if command == "0":
			break

		elif command == "1":
			glossary = create_file()

		elif command == "2":
			glossary = add_term(glossary)

		elif command == "3":
			glossary = del_term(glossary)

		elif command == "4":
			show_glossary(glossary)

		elif command == "5":
			show_interpretation(glossary)

		elif command == "6":
			save_file(glossary)

		elif command == "7":
			glossary = load_file()

		else:
			print("Неизвестная команда")


if __name__ == "__main__":
	main()