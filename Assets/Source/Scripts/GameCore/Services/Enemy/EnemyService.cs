﻿using System.Collections.Generic;
using Source.Scripts.GameCore.UnitLogic;

namespace Source.Scripts.GameCore.Services.Enemy
{
    public class EnemyService : IEnemyService
    {
        public Team Team { get; } = new();

        public void Initialize(Team playerTeam, IReadOnlyList<Tower> selfTowers, IReadOnlyList<Unit> selfUnits)
        {
            foreach (Tower tower in selfTowers)
            {
                tower.Construct();
                Team.Add(tower);
            }

            foreach (Unit unit in selfUnits)
            {
                unit.Construct(playerTeam);
                Team.Add(unit);
            } 
        }
    }
}