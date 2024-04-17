using System;
using System.Collections.Generic;
using Source.Scripts.GameCore.UnitLogic;
using UnityEngine;

namespace Source.Scripts.GameCore
{
    public class Team
    {
        private readonly List<IDamageable> _towers = new List<IDamageable>();
        private readonly List<IDamageable> _meleeUnits = new List<IDamageable>();
        private readonly List<IDamageable> _flyUnits = new List<IDamageable>();
        private readonly List<IDamageable> _ogrUnits = new List<IDamageable>();

        public void Add(Tower tower) => 
            AddObjectToList(_towers, tower);

        public void Add(UnitBase unit)
        {
            switch (unit)
            {
                case MeleeUnit:
                    AddObjectToList(_meleeUnits, unit);
                    break;
                case FlyUnit:
                    AddObjectToList(_flyUnits, unit);
                    break;
                case OgrUnit:
                    AddObjectToList(_ogrUnits, unit);
                    break;
                default:
                    throw new ArgumentException($"Trying to add unsupported unit type of {unit.GetType()}");
            }
        }

        public bool TryGetNearestUnit(in Vector3 currentPosition, out IDamageable unit)
        {
            unit = GetNearest(currentPosition, _meleeUnits);
            return unit != null;
        }

        public bool TryGetNearestTower(in Vector3 currentPosition, out IDamageable tower)
        {
            tower = GetNearest(currentPosition, _towers);
            return tower != null;
        }

        private static IDamageable GetNearest<T>(in Vector3 currentPosition, IEnumerable<T> objects) where T : IDamageable
        {
            IDamageable nearestTarget = null;
            float distance = float.MaxValue;
            
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