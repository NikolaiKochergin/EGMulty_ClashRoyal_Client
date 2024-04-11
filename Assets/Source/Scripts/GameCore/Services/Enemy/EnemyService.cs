namespace Source.Scripts.GameCore.Services.Enemy
{
    public class EnemyService : IEnemyService
    {
        public EnemyService(Team enemyTeam) => 
            Team = enemyTeam;

        public Team Team { get; }

        public void Initialize(Team playerTeam) => 
            Team.Initialize(playerTeam);
    }
}