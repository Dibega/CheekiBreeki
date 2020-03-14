using System;

namespace Server.Models
{
    public class PlayerResultModel : IComparable<PlayerResultModel>
    {
        public string Id;
        public uint AnswerCount;
        public float GameTime;

        public PlayerResultModel(string id, uint answerCount, float gameTime)
        {
            Id = id;
            AnswerCount = answerCount;
            GameTime = gameTime;
        }

        public int CompareTo(PlayerResultModel resultModel)
        {
            return resultModel.AnswerCount.CompareTo(AnswerCount);
        }
    }
}
