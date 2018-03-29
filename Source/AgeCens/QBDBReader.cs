using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeCens
{
    /// <summary>
    /// Класс считывания файла с опросом
    /// </summary>
    class QBDBReader
    {
        /// <summary>
        /// Путь до файла опроса
        /// </summary>
        private String path;
        /// <summary>
        /// Содержимое файла
        /// </summary>
        private List<String> lines;
        /// <summary>
        /// Блоки вопросов в файле
        /// </summary>
        private List<QuestBlock> blocks;
        /// <summary>
        /// Количество блоков вопросов в файле
        /// </summary>
        public int countBlocks
        {
            get
            {
                return blocks.Count;
            }
            set { }
        }
        /// <summary>
        /// Путь до папки с опросами
        /// </summary>
        private static String dirQuest = @"QuestionsDB\";
        /// <summary>
        /// Расширение файла опроса
        /// </summary>
        private static String fileExt = @"*.qbdb";
        /// <summary>
        /// Текущий опрос
        /// </summary>
        private static String currFile;

        private QBDBReader(String ph)
        {
            this.path = ph;
            this.lines = File.ReadAllLines(path).ToList<String>(); ///Получение содержимого выбранного файла
            ClearEmptyAndCommentLines();
            ReadQBDB();

        }
        /// <summary>
        /// Получение имен блоков вопросов в файле
        /// </summary>
        /// <returns>Список имен блоков вопросов</returns>
        public List<String> GetNameBlocks()
        {
            List<String> names = new List<String>();
            foreach(QuestBlock qb in blocks)
            {
                names.Add(qb.nameBlock);
            }
            return names;
        }
        /// <summary>
        /// Получение всех вопросов в заданном блоке
        /// </summary>
        /// <param name="n">Номер блока</param>
        /// <returns>Список вопрос в блоке</returns>
        public List<Question> GetQuestsBlock(int n)
        {
            if (n >= blocks.Count) return null;
            return blocks[n].GetQuests();
        }
        /// <summary>
        /// Удалить блок из списка
        /// </summary>
        /// <param name="n">Номер удаляемого блока</param>
        public void DeleteBlock(int n)
        {
            blocks.RemoveAt(n);
        }
        /// <summary>
        /// Парсинг файла. Получение блоков вопросов, и самих вопросов.
        /// </summary>
        private void ReadQBDB()
        {
            blocks = new List<QuestBlock>(); //Создание списка блоков
            foreach (String line in lines)
            {
                if (line.IndexOf(']') == -1 || line.IndexOf('[') == -1)
                    continue;
                int end_sign_pos = line.IndexOf(']');
                
                String sign = line.Substring(1, end_sign_pos-1);
                int count = sign.Split('.').Length - 1; //Определение фрагмента опроса, если точек нет, то блок, иначе вопрос

                if (count == 0) //Если блок
                {
                    QuestBlock qb = new QuestBlock(line.Substring(end_sign_pos+1,line.Length-end_sign_pos-1)
                                                   ,Convert.ToInt32(sign)); //Создание нового блока
                    blocks.Add(qb); //Добавление блока в список
                }
                if (count == 1) //Если вопрос
                {
                    int pos = sign.IndexOf('.');
                    int pos_rate_start = line.IndexOf('{');
                    int pos_rate_end = line.IndexOf('}');
                    if (pos == -1 || pos_rate_start == -1 || pos_rate_end == -1)
                        continue;
                    int length_rate = pos_rate_end - pos_rate_start+1;
                    int numBlock = Convert.ToInt32(sign.Substring(0, pos)); //Определение номера блока, к которому принадлежит вопрос
                    //Определение номера вопроса.
                    int numQuest = Convert.ToInt32(sign.Substring(pos + 1, sign.Length - pos - 1));
                    //Определение возрастного рейтинга вопроса
                    String rate = line.Substring(pos_rate_start+1, pos_rate_end - pos_rate_start-1);
                    //Получение текста вопроса
                    String textQuest = line.Substring(end_sign_pos + 1, 
                        line.Length - end_sign_pos - 1 - length_rate);
                    if (rate == "") //Если рейтинг не был указан в файле
                        rate = "0";
                    try
                    {
                        //Создание нового вопроса
                        Question quest = new Question(textQuest, (Age_Rate)Convert.ToInt32(rate), numQuest);
                        //Нахождение нужного блока, для добавления в него вопроса
                        foreach (QuestBlock qb in blocks)
                        {
                            if (qb.numBlock == numBlock)
                                qb.AddQuest(quest);//Добавление вопроса в блок.
                        }
                    }
                    catch (FormatException)
                    {

                    }
                    
                }
            }
        }
        /// <summary>
        /// Удаление пустых строк, комментариев, неликвидных строк, названия опроса.
        /// </summary>
        private void ClearEmptyAndCommentLines()
        {
            for(int i = 0; i < lines.Count; i++)
            {
                String line = lines[i].Trim(' ');
                if (line.Length < 3 || line[0] == '&' || line[0] == '#' ||
                   (line[0]!='[' && line.IndexOf(']') != -1))
                {
                    lines.RemoveAt(i);
                    i--;
                }
                    
            }
        }
        /// <summary>
        /// Статический метод, для создания экземпляра класса QBDBReader и выбора опроса для прохождения.
        /// </summary>
        /// <returns>Экземпляр класса QBDRBReader, с содержимым файла</returns>
        public static QBDBReader GetAllBlocks()
        {
            QBDBReader qbl = null;
            try
            {
                String[] filesBlock = Directory.GetFiles(dirQuest, fileExt); //Получение списка файлов *.qbdb
                if(filesBlock.Length == 0) //Если файлов не найдено
                {
                    return null;
                }
                String[] blockNames = GetBlockNames(filesBlock);//Получение имен опросов
                int selFile;
                if (1 == filesBlock.Length) //Если опрос всего один, отметить его как выбранный
                {
                    selFile = 0;
                }
                else //Иначе предоставить выбор пользователю
                {
                    selFile = GetSelectCurrFile(blockNames); //Выбор пользователем проходимого опроса.
                }
                if (selFile == (int)Errors.ERREXIT)
                    return null;
                currFile = filesBlock[selFile];
                qbl = ReadFileBlock(currFile); //Считывание файла
                return qbl;
            }
            catch (IOException)
            {
                return null;
            }
        }
        /// <summary>
        /// Выбор пользователем проходимого опроса
        /// </summary>
        /// <param name="blockNames">Список опросов</param>
        /// <returns>Выбранный пользоватем опрос</returns>
        private static int GetSelectCurrFile(String[] blockNames)
        {
            while (true)
            {
                int selFile = SelectCurrFile(blockNames);  //Выбор пользователем опроса
                if (Program.TestErr(selFile)) //Проверка введенных данных
                    return selFile;
                else
                    return (int)Errors.ERREXIT;
            }
        }
        /// <summary>
        /// Выбор пользователем опросаа
        /// </summary>
        /// <param name="blockNames">Список опросов</param>
        /// <returns>Введеный пользователем номер</returns>
        private static int SelectCurrFile(String[] blockNames)
        {
            Console.WriteLine("Найдено несколько файлов опроса.");
            Console.WriteLine("Выберите какой опрос будете проходить.");
            Console.WriteLine("Введите q для выхода.");
            for (int i = 0; i < blockNames.Length; i++)
            {
                Console.Write('[' + i.ToString() + ']');
                Console.WriteLine(blockNames[i]);
            }
            String str = Console.ReadLine(); //Считывание введенного пользователем числа
            if (str.Equals("q")) return (int)Errors.ERREXIT;
            int res = (int)Errors.ERRFX;
            try
            {
                res = Convert.ToInt32(str);
                if (res > blockNames.Length)
                {
                    return (int)Errors.ERRAI;
                }
            }
            catch (FormatException)
            {
                return (int)Errors.ERRFX;
            }
            return res;

        }
        /// <summary>
        /// Создание экземпляра класса QBDBReader
        /// </summary>
        /// <param name="reader_path">Путь до файла</param>
        /// <returns>Инициализированный земпляр класса QBDBReader</returns>
        private static QBDBReader ReadFileBlock(String reader_path)
        {
            return new QBDBReader(reader_path);
        }
        /// <summary>
        /// Получение имен опросов
        /// </summary>
        /// <param name="paths">Пути до файлов опросов</param>
        /// <returns>Список имен опросов</returns>
        private static String[] GetBlockNames(String[] paths)
        {
            String[] names = new String[paths.Length];
            for (int i = 0; i < paths.Length; i++)
            {
                foreach (String line in File.ReadAllLines(paths[i]))
                {
                    if (line.Length > 3 && line[0] == '&')
                    {
                        names[i] = line.Substring(1);//Получение имени опроса
                        break;
                    }
                }
            }
            return names;
        }
    }
}
