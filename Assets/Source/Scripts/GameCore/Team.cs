using System.Collections.Generic;
using Source.Scripts.GameCore.UnitLogic;
using UnityEngine;

namespace Source.Scripts.GameCore
{
    public class Team
    {
        private readonly IEnumerable<Tower> _towers;
        private readonly IEnumerable<Unit> _units;

        public Team(IEnumerable<Tower> towers, IEnumerable<Unit> units)
        {
            _towers = towers;
            _units = units;
        }

        public void Initialize(Team enemyTeam)
        {
            foreach (Unit unit in _units) 
                unit.Construct(enemyTeam);
        }

        public bool TryGetNearestUnit(in Vector3 currentPosition, out Unit unit, out float distance)
        {
            unit = GetNearest(currentPosition, _units, out distance);
            return unit;
        }

        public Tower GetNearestTower(in Vector3 currentPosition) => 
            GetNearest(currentPosition, _towers, out float distance);

        private static T GetNearest<T>(in Vector3 currentPosition, IEnumerable<T> objects, out float distance) where T : ITarget
        {
            ITarget nearestTarget = null;
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