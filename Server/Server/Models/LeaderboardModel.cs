using System.Collections.Generic;

namespace Server.Models
{
    public class LeaderboardModel
    {
        List<PlayerResultModel> _winsList = new List<PlayerResultModel>();
        private int maxLength = 10;

        public void AddResult(PlayerResultModel result)
        {
            _winsList.Add(result);
            _winsList.Sort();
            if (_winsList.Count > maxLength)
            {
                _winsList.Remove(_winsList[maxLength]);
            }
        }

        //public IEnumerable<PlayerResultModel> GetListResults()
        //{
        //    return _winsList;
        //}

        public LeaderboardDto GetLeaderboardDto()
        {
            var results = new PlayerResultModel[maxLength];
            for (int i = 0; i < _winsList.Count; i++)
            {
                results[i] = _winsList[i];
            }
            return new LeaderboardDto(results);
        }
    }
}
