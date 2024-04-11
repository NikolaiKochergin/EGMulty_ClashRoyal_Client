using Source.Scripts.GameCore;
using Source.Scripts.GameCore.MapLogic;
using Source.Scripts.GameCore.Services.Enemy;
using Source.Scripts.GameCore.Services.Player;
using UnityEngine;

namespace Source.Scripts.Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private MapInfo _mapInfo;
        
        private IPlayerService _player;
        private IEnemyService _enemy;

        private void Awake()
        {
            _player = new PlayerService(new Team(_mapInfo.PlayerTowers, _mapInfo.PlayerUnits));
            _enemy = new EnemyService(new Team(_mapInfo.EnemyTowers, _mapInfo.EnemyUnits));
            
            _player.Initialize(_enemy.Team);
            _enemy.Initialize(_player.Team);
        }
    }
}