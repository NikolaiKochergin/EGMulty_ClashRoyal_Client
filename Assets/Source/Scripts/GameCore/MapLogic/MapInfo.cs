using System.Collections.Generic;
using Source.Scripts.GameCore.UnitLogic;
using UnityEngine;

namespace Source.Scripts.GameCore.MapLogic
{
    public class MapInfo : MonoBehaviour
    {
        [SerializeField] private List<Tower> _playerTowers;
        [SerializeField] private List<Tower> _enemyTowers;
        [SerializeField] private List<UnitBase> _playerUnits;
        [SerializeField] private List<UnitBase> _enemyUnits;

        public IReadOnlyList<Tower> PlayerTowers => _playerTowers;
        public IReadOnlyList<Tower> EnemyTowers => _enemyTowers;
        public IReadOnlyList<UnitBase> PlayerUnits => _playerUnits;
        public IReadOnlyList<UnitBase> EnemyUnits => _enemyUnits;
    }
}