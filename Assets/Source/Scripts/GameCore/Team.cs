using System.Collections.Generic;
using Source.Scripts.GameCore.UnitLogic;
using UnityEngine;

namespace Source.Scripts.GameCore
{
    public class Team
    {
        private readonly List<Tower> _towers = new List<Tower>();
        private readonly List<Unit> _units = new List<Unit>();
        
        private Team _enemyTeam;

        public void Initialize(Team enemyTeam) => 
            _enemyTeam = enemyTeam;

        public void Add(Tower tower)
        {
            tower.Construct(this);
            _towers.Add(tower);
        }

        public void Add(Unit unit)
        {
            unit.Construct(this, _enemyTeam);
            _units.Add(unit);
        }

        public void Remove(Tower tower) => 
            _towers.Remove(tower);

        public void Remove(Unit unit) => 
            _units.Remove(unit);

        public bool TryGetNearestUnit(in Vector3 currentPosition, out Unit unit, out float distance)
        {
            unit = GetNearest(currentPosition, _units, out distance);
            return unit;
        }

        public Tower GetNearestTower(in Vector3 currentPosition) => 
            GetNearest(currentPosition, _towers, out float distance);

        private static T GetNearest<T>(in Vector3 currentPosition, IEnumerable<T> objects, out float distance) where T : IDamageable
        {
            IDamageable nearestTarget = null;
            distance = float.MaxValue;
            
            foreach (T target in objects)
            {
                float tempDistance = Vector3.Distance(currentPosition, target.Transform.position);
                if(tempDistance > distance)
                    continue;

                nearestTarget = target;
                distance = tempDistance;
            }

            return (T) nearestTarget;
        }
    }
}