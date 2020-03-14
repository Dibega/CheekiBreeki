using System;

[Serializable]
public class LeaderboardDto
{
    public PlayerResultModel[] leaderboard;

    public LeaderboardDto(PlayerResultModel[] leaderboard)
    {
        this.leaderboard = leaderboard;
    }
}
