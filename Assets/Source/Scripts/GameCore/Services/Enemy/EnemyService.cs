using System.Collections.Generic;
using Source.Scripts.GameCore.UnitLogic;

namespace Source.Scripts.GameCore.Services.Enemy
{
    public class EnemyService : IEnemyService
    {
        public Team Team { get; } = new();

        public void Initialize(Team playerTeam, IReadOnlyList<Tower> selfTowers, IReadOnlyList<Unit> selfUnits)
        {
            Team.Initialize(playerTeam);

            foreach (Tower tower in selfTowers) 
                Team.Add(tower);

            foreach (Unit unit in selfUnits) 
                Team.Add(unit);
        }
    }
}