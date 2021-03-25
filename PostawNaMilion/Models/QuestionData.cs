using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PostawNaMilion.Models
{
    public class QuestionData : IQuestionData
    {
        public IConfiguration Configuration { get; }
        public readonly string ConnString;
        public QuestionData(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnString = Configuration.GetConnectionString("Database");
        }

        public bool CheckAnswer(string answer, string[] answers, int[] bets)
        {
            int money = 0;
            foreach (var x in bets)
            {
                money += x;
            }
            Player.Amount -= money;
            for(int i = 0; i < answers.Length; i++)
            {
                if(answer == answers[i] && bets[i] > 20000)
                {
                    Player.Amount += bets[i];
                    return true;
                }
            }
            return false;

        }

        public void CreateQuestions()
        {
            QuestionDatas.QuestionsList = new List<Question>();

            using (SqlConnection con = new SqlConnection(ConnString))
            {

                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Questions;", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                Question quest = new Question();
                                quest.QuestionId = Convert.ToInt32(reader["QuestionId"]);
                                quest.QuestionCategory = reader["QuestionCategory"].ToString();
                                quest.QuestionName = reader["QuestionName"].ToString();
                                quest.CorrectAnswer = reader["CorrectAnswer"].ToString();
                                quest.QuestionLevel = Convert.ToInt32(reader["QuestionLevel"]);
                                QuestionDatas.QuestionsList.Add(quest);
                            }
                        }
                    }

                }
                con.Close();
            }

        }

        public List<Answer> GetAnswers(Question question)
        {
            QuestionDatas.Answers = new List<Answer>();
            using (SqlConnection con = new SqlConnection(ConnString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Answers WHERE QuestionId = {question.QuestionId};", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader != null)
                        {
                            while(reader.Read())
                            {
                                Answer answer = new Answer();
                                answer.AnswerId = Convert.ToInt32(reader["AnswerId"]);
                                answer.AnswerName = reader["AnswerName"].ToString();
                                answer.QuestionId = Convert.ToInt32(reader["QuestionId"]);
                                QuestionDatas.Answers.Add(answer);
                            }
                        }
                    }
                }

                con.Close();
            }
            return QuestionDatas.Answers;
        }

        public void GetQuestion(int questLvl)
        {
            var GetQuestions = QuestionDatas.QuestionsList.Where(c => c.QuestionLevel == questLvl);
            QuestionDatas.ActiveQuestions = new List<Question>();
            for (int i = 0; i <= 1; i++)
            {
                var getId = GetQuestions.OrderBy(c => Guid.NewGuid()).FirstOrDefault();
                QuestionDatas.ActiveQuestions.Add(getId);
                QuestionDatas.QuestionsList.Remove(getId);
            }

        }

        public void ResetDatas()
        {
            QuestionDatas.QuestionsList = null;
            QuestionDatas.ActiveQuestions = null;
        }

        public Question SelectQuestion(int questId)
        {
            return QuestionDatas.ActiveQuestions.FirstOrDefault(c => c.QuestionId == questId);
        }

    }
}
