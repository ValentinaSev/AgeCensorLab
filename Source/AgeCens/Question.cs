using System;

namespace AgeCens
{
    /// <summary>
    /// Класс для хранения одного вопроса, и его возрастного рейтинга
    /// </summary>
    class Question
    {
        /// <summary>
        /// Текст вопроса
        /// </summary>
        public String quest { get; private set; } 
        /// <summary>
        /// Возрастной рейтинг вопросаа
        /// </summary>
        public Age_Rate rate { get; private set; }
        /// <summary>
        /// Номер вопроса
        /// </summary>
        public int numQuest { get; private set; }
        
        public Question(String qt, Age_Rate r, int nq)
        {
            this.quest = qt;
            this.rate = r;
            this.numQuest = nq;
        }
        
    }
}
