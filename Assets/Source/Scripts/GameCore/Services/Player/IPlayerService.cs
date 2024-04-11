namespace Source.Scripts.GameCore.Services.Player
{
    public interface IPlayerService
    {
        void Initialize(Team enemyTeam);
        Team Team { get; }
    }
}