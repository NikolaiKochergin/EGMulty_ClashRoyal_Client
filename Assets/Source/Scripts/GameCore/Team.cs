using System;
using System.Collections.Generic;
using Source.Scripts.GameCore.UnitLogic;
using UnityEngine;

namespace Source.Scripts.GameCore
{
    public class Team
    {
        private readonly List<IDamageable> _towers = new List<IDamageable>();
        private readonly List<IDamageable> _walkingUnits = new List<IDamageable>();
        private readonly List<IDamageable> _flyUnits = new List<IDamageable>();

        public void Add(Tower tower) => 
            AddObjectToList(_towers, tower);

        public void Add(UnitBase unit)
        {
            switch (unit.Stats.MoveType)
            {
                case MoveType.Walk:
                    AddObjectToList(_walkingUnits, unit);
                    break;
                case MoveType.Fly:
                    AddObjectToList(_flyUnits, unit);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool TryGetNearestAnyUnit(in Vector3 currentPosition, out IDamageable unit, out float distance)
        {
            TryGetNearestWalkingUnit(currentPosition, out IDamageable walking, out float walkingDistance);
            TryGetNearestFlyUnit(currentPosition, out IDamageable fly, out float flyDistance);

            if (flyDistance < walkingDistance)
            {
                unit = fly;
                distance = flyDistance;
            }
            else
            {
                unit = walking;
                distance = walkingDistance;
            }

            return unit != null;
        }

        public bool TryGetNearestFlyUnit(in Vector3 currentPosition, out IDamageable unit, out float distance)
        {
            unit = GetNearest(currentPosition, _flyUnits, out distance);
            return unit != null;
        }

        public bool TryGetNearestWalkingUnit(in Vector3 currentPosition, out IDamageable unit, out float distance)
        {
            unit = GetNearest(currentPosition, _walkingUnits, out distance);
            return unit != null;
        }

        public bool TryGetNearestTower(in Vector3 currentPosition, out IDamageable tower, out float distance)
        {
            tower = GetNearest(currentPosition, _towers, out distance);
            return tower != null;
        }

        private static IDamageable GetNearest<T>(in Vector3 currentPosition, IEnumerable<T> objects, out float distance) where T : IDamageable
        {
            IDamageable nearestTarget = null;
            distance = float.MaxValue;
            
            foreach (T target in objects)
            {
                if(target.Health.CurrentValue == 0)
                    continue;
                
                float tempDistance = Vector3.Distance(currentPosition, target.Transform.position);
                if(tempDistance > distance)
                    continue;

                nearestTarget = target;
                distance = tempDistance;
            }

            return nearestTarget;
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