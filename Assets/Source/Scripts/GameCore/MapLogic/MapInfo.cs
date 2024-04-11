using System.Collections.Generic;
using Source.Scripts.GameCore.UnitLogic;
using UnityEngine;

namespace Source.Scripts.GameCore.MapLogic
{
    public class MapInfo : MonoBehaviour
    {
        [SerializeField] private List<Tower> _playerTowers;
        [SerializeField] private List<Tower> _enemyTowers;
        [SerializeField] private List<Unit> _playerUnits;
        [SerializeField] private List<Unit> _enemyUnits;

        public IReadOnlyList<Tower> PlayerTowers => _playerTowers;
        public IReadOnlyList<Tower> EnemyTowers => _enemyTowers;
        public IReadOnlyList<Unit> PlayerUnits => _playerUnits;
        public IReadOnlyList<Unit> EnemyUnits => _enemyUnits;
    }
}