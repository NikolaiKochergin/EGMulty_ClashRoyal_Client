namespace Source.Scripts.GameCore.Services.Enemy
{
    public interface IEnemyService
    {
        void Initialize(Team playerTeam);
        Team Team { get; }
    }
}