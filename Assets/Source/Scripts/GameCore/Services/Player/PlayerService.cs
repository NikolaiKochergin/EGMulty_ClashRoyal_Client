using System.Collections.Generic;
using Source.Scripts.GameCore.UnitLogic;

namespace Source.Scripts.GameCore.Services.Player
{
    public class PlayerService : IPlayerService
    {
        public Team Team { get; } = new();

        public void Initialize(Team enemyTeam, IReadOnlyList<Tower> selfTowers, IReadOnlyList<Unit> selfUnits)
        {
            foreach (Tower tower in selfTowers)
            {
                tower.Construct();
                Team.Add(tower);
            }

            foreach (Unit unit in selfUnits)
            {
                unit.Construct(enemyTeam);
                Team.Add(unit);
            }
        }
    }
}