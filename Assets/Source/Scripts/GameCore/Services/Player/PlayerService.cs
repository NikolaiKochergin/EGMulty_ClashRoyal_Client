using System.Collections.Generic;
using Source.Scripts.GameCore.UnitLogic;

namespace Source.Scripts.GameCore.Services.Player
{
    public class PlayerService : IPlayerService
    {
        public Team Team { get; } = new();

        public void Initialize(Team enemyTeam, IReadOnlyList<Tower> selfTowers, IReadOnlyList<Unit> selfUnits)
        {
            Team.Initialize(enemyTeam);
            
            foreach (Tower tower in selfTowers) 
                Team.Add(tower);

            foreach (Unit unit in selfUnits) 
                Team.Add(unit);
        }
    }
}