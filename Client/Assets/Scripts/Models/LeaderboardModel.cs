using System;
using System.Collections.Generic;

[Serializable]
public class LeaderboardModel
{
    private PlayerResultModel[] _playerResultModels;

    public LeaderboardModel(LeaderboardDto leaderboard)
    {
        _playerResultModels = leaderboard.leaderboard;
    }

    public IEnumerable<PlayerResultModel> GetResults()
    {
        return _playerResultModels;
    }
}

