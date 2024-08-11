using System.Collections.Generic;
using NaughtyAttributes;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace DefaultNamespace.Spawner
{
    [CreateAssetMenu(fileName = "LevelSystem", menuName = "LevelSystem")]
    public class LevelSystem : ScriptableObject, IInit, ICleanUp
    {
        [Expandable]
        public IntVariable CurrentLevel;
        public List<LevelData> Levels;
        
        public ObjectGameEvent OnLevelChanged;
        public ObjectGameEvent OnLevelCompleted;

        public GameStateGameEvent OnGameStateChanged;
        
        private LevelData CurrentLevelData => Levels[CurrentLevel.Value % Levels.Count];
        
        public void Init()
        {
            OnGameStateChanged.AddListener(OnGameStateChangedHandler);
            CurrentLevel.Value = PlayerPrefs.GetInt("CurrentLevel",0);
        }

        private void OnGameStateChangedHandler(GameState state)
        {
            if (state.IsGamePlayingState())
            {
                OnLevelChanged.Raise(CurrentLevelData);
            }
            else if (state.IsGameWinState())
            {
                OnLevelCompleted.Raise(CurrentLevelData);
                CurrentLevel.Value++;
            }
            else if (state.IsGameLoseState())
            {
                OnLevelCompleted.Raise(CurrentLevelData);
            }
        }

        public void CleanUp()
        {
            OnGameStateChanged.RemoveListener(OnGameStateChangedHandler);
            PlayerPrefs.SetInt("CurrentLevel",CurrentLevel.Value);
        }
    }
}