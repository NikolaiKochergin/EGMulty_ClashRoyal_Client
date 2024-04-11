namespace Source.Scripts.GameCore.Services.Player
{
    public class PlayerService : IPlayerService
    {
        public PlayerService(Team playerTeam) => 
            Team = playerTeam;

        public Team Team { get; }

        public void Initialize(Team enemyTeam) => 
            Team.Initialize(enemyTeam);
    }
}