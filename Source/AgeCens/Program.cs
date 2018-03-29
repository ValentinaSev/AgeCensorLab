using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AgeCens
{
    /// <summary>
    /// Список возрастных рейтингов
    /// </summary>
    enum Age_Rate { R_0=0, R_6=6, R_12=12, R_16=16, R_18=18};
    /// <summary>
    /// Список ошибок
    /// </summary>
    enum Errors { ERRFX=-3, ERRAI=-2, ERREXIT=-1};
    class Program
    {
        /// <summary>
        /// Текущий рейтинг
        /// </summary>
        private static Age_Rate currRate = Age_Rate.R_0;
        static void Main(string[] args)
        {
            //Считывание данных выбранного пользователем опроса
            QBDBReader qbdb = QBDBReader.GetAllBlocks();
            Console.WriteLine();
            if(qbdb == null)
            {
                Console.WriteLine("Папка QuestionsDB с опросами не найдена, либо она пустая, либо выбран некорректный опрос.");
                Console.ReadKey();
                return;
            }
            while (qbdb.countBlocks > 0) //Пока не даны ответы на все блоки вопросов
            {
                if (currRate == Age_Rate.R_18) break; //Если текущий рейтинг 18, выход из цикла
                List<String> names = qbdb.GetNameBlocks();  //Получение имен оставшихся блоков вопросов.
                Console.WriteLine();
                Console.WriteLine("---------------------------------------------");
                for (int i = 0; i < names.Count; i++)
                {
                    Console.Write('[' + i.ToString() + ']');
                    Console.WriteLine(names[i]);
                }
                Console.WriteLine("---------------------------------------------");
                String input_line = Console.ReadLine(); //Получение номера блока от пользователя
                int res = ParseInput(input_line,qbdb.countBlocks); //Парсинг введенных пользователем данных
                if (TestErr(res)) //Проверка корректности введенных данных на ошибки
                {
                    if(AnswerOnQuest(qbdb, res)) //Ответ на вопрос пользователем
                        qbdb.DeleteBlock(res);  //Если ответ дан, удалить текущий блок из списка
                }
            }
            Console.Clear();
            Console.WriteLine("Рейтинг - {0}+", Convert.ToString((int)currRate));
            Console.ReadKey();
        }
        /// <summary>
        /// Ответ на вопрос пользователем
        /// </summary>
        /// <param name="reader">Экземпляр QBDBReader с текущим опросом</param>
        /// <param name="nBlock">Текущий номер блока вопросов</param>
        /// <returns>Дан ли ответ пользователем</returns>
        static bool AnswerOnQuest(QBDBReader reader,int nBlock)
        {
            List<Question> quests = reader.GetQuestsBlock(nBlock); //Получение списка вопросов в блоке
            //Вывод вопросов на экран
            Console.WriteLine();
            for (int i = 0; i < quests.Count; i++)
            {
                Console.Write('[' + i.ToString() + ']');
                Console.WriteLine(quests[i].quest);
            }
            String input_line = Console.ReadLine(); //Получение ответа от пользователя
            int res = ParseInput(input_line, quests.Count); //Парсинг введенных пользователем данных
            if (TestErr(res)) //Проверка корректности введенных данных на ошибки
            {
                Console.WriteLine();
                if (currRate < quests[res].rate) //Если рейтинг выбранного ответа больше текущего
                    currRate = quests[res].rate; //Увеличиваем рейтинга
                return true;
            }
            return false;
        }

        /// <summary>
        /// Проверка на ошибки введенных данных
        /// </summary>
        /// <param name="res">Введенные данные</param>
        /// <returns>True - если ошибок нет, False - иначе</returns>
        public static bool TestErr(int res)
        {
            switch (res)
            {
                case (int)Errors.ERRAI: //Если выбранный ответ выходит за границы
                    Console.Clear();
                    Console.WriteLine("Нет опроса\\блока\\вопроса под таким номером.");
                    Console.WriteLine();
                    return false;
                case (int)Errors.ERRFX: //Если введены некорректные символы
                    Console.Clear();
                    Console.WriteLine("Ошибка ввода. Присутствуют посторонние символы");
                    Console.WriteLine();
                    return true;
                case (int)Errors.ERREXIT: //Если пользователь ввел 'q' для выхода
                    Console.WriteLine("Отменено пользователем.");
                    return false;
                default: //Если ошибок нет
                    return true;
            }
        }
        /// <summary>
        /// Парсинг введенных пользователем данных
        /// </summary>
        /// <param name="input">Введенное пользователем значение</param>
        /// <param name="max">Максимальновозможное значение - 1</param>
        /// <returns>Введеное пользователем значение, или номер ошибки</returns>
        static int ParseInput(String input, int max)
        {
            if (input == "q")
                return (int)Errors.ERREXIT;
            int res;
            try
            {
                res = Convert.ToInt32(input);
                if (res < 0 || res >= max)
                    return (int)Errors.ERRAI;
                return res;
            }
            catch (FormatException)
            {
                return (int)Errors.ERRFX;
            }
        }
    }
}
