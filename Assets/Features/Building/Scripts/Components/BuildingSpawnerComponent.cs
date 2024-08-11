using System;
using System.Collections;
using DefaultNamespace;
using Features.BoardUnit;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Features.Building.Scripts.Components
{
    public class BuildingSpawnerComponent : BuildingComponent
    {
        public GameStateVariable GameState;
        public GameStateEvent OnGamePlayingState;
        
        public BoardUnitAsset UnitAsset;
        public float Cooldown;

        private float _cooldownTimer;

        private void Start()
        {
            OnGamePlayingState.AddListener(StartSpawn);
        }

        private void OnDestroy()
        {
            OnGamePlayingState.RemoveListener(StartSpawn);
        }

        public void StartSpawn(GameState gameState)
        {
            // Spawn logic
            StartCoroutine(SpawnRoutine());
        }

        public virtual IEnumerator SpawnRoutine()
        {
            while (GameState.Value is GameStates.GamePlayingState)
            {
                _cooldownTimer+= Time.deltaTime;
                if (_cooldownTimer >= Cooldown)
                {
                    _cooldownTimer = 0;
                    SpawnUnit();
                }
                yield return null;
            }
        }

        protected virtual void SpawnUnit()
        {
            var unit = UnitAsset.CreateInScene();
        }
    }
}