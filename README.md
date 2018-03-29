# Age Censor

Приложение для проведения опросов и определения возрастного рейтинга информационной продукции.

## Требования для запуска приложения

На компьютере должен быть установлен фреймворк Microsoft .NET Framework 4.5.2, или более поздней версии.

## Требования для компиляции приложения

На компьютере должна быть установлена среда разработки Microsoft Visual Studio 2015 или более поздняя версия.

## Инструкция по использованию приложения
Скачать папку Release из репозитория. В ней уже будет 1 опрос, для определения возрастного рейтинга игры.

После запуска приложения, если в папке QuestionsDB находится несколько файлов опросов, будет предоставлен выбор проходимого опроса, или, если опрос один, то сразу появятся блоки вопросов.  
Для выбора опроса\блока вопросов\ответа на вопрос необходимо ввести номер выбранного пункта и нажать клавишу Enter.  
При введении вместо номера буквы q, произойдет выход из приложения.  
Для завершения опроса и получения итогового рейтинга, необходимо ответить на все блоки вопросов, либо если происходит выбор ответа, который сразу характерен для рейтинга 18+.  
Допускается только 1 ответ в 1 блоке вопросов.  
При ответе на вопрос блока, данный блок больше не будет отображаться.  
Для выхода из блока вопросов к списку блоков, достаточно написать любое число, под которым нет ответа на вопрос.  

## Инструкция по созданию опроса

Опрос должен находится в папке QuestionsDB, лежащей в той же папке, что и exe файл-приложения, и иметь расширение .qbdb.
Структура файла:  
* С символа '#' начинаются комментарии.
* Название опроса, которое выводится в программе, должно начинаться с символа &(амперсанд).
* Каждый новый блок или вопрос должны начинаться с новой строки.
* Блок должен начинаться с квадратных скобок, в которых указан номер блока. 
* После блока идут вопросы связанные с этим блоком. Вопрос начинается с квадратных скобок, внутри которых два чисда, разделенных точкой, где первое число - номер блока, к которому относится вопрос, а второе число - номер вопроса внутри этого блока. 
* После описания каждого вопроса, на той же строчке в фигурных скобках должен указываться возрастной рейтинг этого вопроса. Если оставить скобки пустыми, то по умолчанию ему присуждается рейтинг 0+.

## Инструкция по компиляции

1. Скачать исходный код программы из репозитория(папка Source).  
2. Запустить файл *AgeCens.sln*.
3. В Visual Studio нажать *Build -> Build Solution*. Либо нажать F6.

Скомпилированное приложение будет находится по пути: `папка_проекта\bin\выбранная_реализация(по умолчанию Debug)\`

## Ссылки на инструменты

1. [Microsoft Visual Studio](https://www.visualstudio.com/ru/?rr=https%3A%2F%2Fyandex.ru%2Fclck%2Fjsredir%3Fbu%3Duniq15221732018216762318%26from%3Dyandex.ru%253Bsearch%252F%253Bweb%253B%253B%26text%3D%26etext%3D1740.g3W0PPLkGgbwTwxkqm6o9vCx1L8hwTc_wt4s979lOn7DF1XkyjBeWaswv5SlG-mp.165f4e53558dd87ab70dcbba430677b407439c37%26uuid%3D%26state%3DPEtFfuTeVD5kpHnK9lio9T6U0-imFY5IWwl6BSUGTYk4N0pAo4tbW329kW4OUAN4nPO7ab-rOBsB5v7Q-uO9MCs58vnS0bI7vHqFfoXUp9yUH_38lvBEPJ6gWpCk-N4JNooqR7GtlyjpRYApvNj_rQ%2C%2C%26%26cst%3DAiuY0DBWFJ5wM1vcHtsEOFXa8WQP8GvKgaJDYPBMMLpL-Bw1OrbJF3PSAqX6g9EwFcynTx9eiit5pM3fNm7-1df4vWYyepcGJkzDSJWjWcTmt0Z2Myxt1BMdJqcCVO5p3-T8UD9Y3tRtV_Fsz4MA--ZwM8wKurGQD_Qq3Qb7jPNO142TxVm3Qb3rQuB5AXjCmeaFjeqMgK-5YgtxM3nR3e3qO3sDBfd6Ea2YQrjnBo9OWELuM6VFIWABi8gNh05M0-Awqm1WqqjnhTZZs6Uf3lVbCiiz30wD1THokQQNseH7wmgPFQIpG3Ygrm8jlJTmuQi7_6cELyt5x9EbPD1wtClF8MTNv4fpk0fLEhMCYCZTOlWFHaKnKRahNGjzvQl3FxXV95bLeIcJB5yArxL0seNF57H8gHDg1UnxWPOS13G1eTHHQikgj0w0hjacyeQCYBQiGfJqIgslIxsSGB4f9uk_4X9UrqwFmrsV9y-AT8W-LLcVke0owV90uoyeArh9mtmhMnlz6bWMfu3dqRbv-b3hgbrsIhwgbJ1Wv0zAFck-e0YVFwR96d-PQnNX1WZS8Y34WKbQoJQBeiAvt_YBmSh_-j2Nan_xDRnQeVjwY-VQpWLiZ4NdOgB3ipxm1QrIXoyAYdZU0CwwNLWCe7itmp4Ro3XIJy5Lys0qUB1bgWFCV9vL3rPDE_X8xKVVtkM-3p54bFL8kyWehJgRDl4oIXoXDrwvxIXyK7OsQEoUOBP3my0lt-Dk_gqRdxhjrXdJ7y5w_OBV9JQ9QI4G7Sg0FHWFTr-S4WXg-Swykw6Om70CiZvUawZ84t3yTInd7XohekQTzQ-NUP2RzIGkSKJTZ-QWD_q9J_oAyFLMX4lcRk3MlPJrY346f7b4wkTq5JiyHTOeN1RFNCqjenM9eF49arZOu7P2L1qL26V2bGRtmIWoVJkmRJzwEiLxndkrVudJ8r2Ukk9L7UF04IdNbvAqOstC9AYqmnniM0M1QeXI6IVPPh73NZTx48U38MnQI8t2Mrunj-MJPow%2C%26data%3DUlNrNmk5WktYejY4cHFySjRXSWhXUHpoQ2RkcHZMR2diNEVYejJEOTJ3aGpuZ09rMXA4dmo1T1JlQ3I1WjdwUDNJYzFwWU5DVmhOWkFvRExLTEdwRHhYOWs2bExjbFFfMkdsblkyZzZuOHMs%26sign%3Dc43617abd5e4cb35e3ea2600f1af186a%26keyno%3D0%26b64e%3D2%26ref%3DorjY4mGPRjk5boDnW0uvlrrd71vZw9kpjly_ySFdX80%2C%26l10n%3Dru%26cts%3D1522233899144%26mc%3D1.9182958340544893)
2. [Microsoft .NET Framework 4.6](https://www.microsoft.com/ru-ru/download/details.aspx?id=48130)

## Лицензия
Данное ПО распространяется по лицензии MIT. С текстом соглашения можно ознакомиться в файле License.