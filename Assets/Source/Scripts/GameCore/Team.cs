using System.Collections.Generic;
using Source.Scripts.GameCore.UnitLogic;
using UnityEngine;

namespace Source.Scripts.GameCore
{
    public class Team
    {
        private readonly List<Tower> _towers = new List<Tower>();
        private readonly List<Unit> _units = new List<Unit>();

        public void Add(Tower tower) => 
            AddObjectToList(_towers, tower);

        public void Add(Unit unit) => 
            AddObjectToList(_units, unit);

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

        private static void AddObjectToList<T>(ICollection<T> list, T obj) where T : IDamageable
        {
            list.Add(obj);
            obj.Health.Died += RemoveAndUnsubscribe;
            return;

            void RemoveAndUnsubscribe()
            {
                RemoveObjectFromList(list, obj);
                obj.Health.Died -= RemoveAndUnsubscribe;
            }
        }

        private static void RemoveObjectFromList<T>(ICollection<T> list, T obj)
        {
            if (list.Contains(obj))
                list.Remove(obj);
        }
    }
}