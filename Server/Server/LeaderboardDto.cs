using Server.Models;

namespace Server
{
    public class LeaderboardDto
    {
        public PlayerResultModel[] Leaderboard;

        public LeaderboardDto(PlayerResultModel[] leaderboard)
        {
            Leaderboard = leaderboard;
        }
    }
}
