﻿using System;
using UnityEngine;

namespace Source.Scripts.StaticData
{
    [Serializable]
    public class UnitStats
    {
        [SerializeField, Min(0)] private float _healthMaxValue = 10;
        [SerializeField, Min(0)] private float _speed = 4f;
        [SerializeField, Min(0)] private float _modelRadius = 1f;
        [Space(10)]
        [SerializeField, Min(0)] private float _startAttackDistance = 1f;
        [SerializeField, Min(0)] private float _stopAttackDistance = 1.5f;
        [SerializeField, Min(0)] private float _startChaseDistance = 5f;
        [SerializeField, Min(0)] private float _stopChaseDistance = 7f;
        
        public float HealthMaxValue => _healthMaxValue;
        public float Speed => _speed;
        public float ModelRadius => _modelRadius;
        public float StartAttackDistance => _startAttackDistance;
        public float StopAttackDistance => _stopAttackDistance;
        public float StartChaseDistance => _startChaseDistance;
        public float StopChaseDistance => _stopChaseDistance;
    }
}