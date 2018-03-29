using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeCens
{
    /// <summary>
    /// Класс, для хранения блока вопросов
    /// </summary>
    class QuestBlock
    {
        /// <summary>
        /// Имя блока
        /// </summary>
        public String nameBlock { get; private set; }
        /// <summary>
        /// Список вопросов в блоке
        /// </summary>
        private List<Question> questions;
        /// <summary>
        /// Номер блока
        /// </summary>
        public int numBlock { get; private set; }

        
        public QuestBlock(String nBlock, int nmBlock)
        {
            this.nameBlock = nBlock;
            this.numBlock = nmBlock;
            questions = new List<Question>();
        }
        /// <summary>
        /// Добавление вопроса в блок
        /// </summary>
        /// <param name="qt">Добавляемый вопрос</param>
        public void AddQuest(Question qt)
        {
            questions.Add(qt);
        }
        /// <summary>
        /// Получение списка вопросов в блоке
        /// </summary>
        /// <returns>Список вопросов</returns>
        public List<Question> GetQuests()
        {
            return questions;
        }
        
    }
}
