using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "GameStateSystem", menuName = "Game Systems/GameStateSystem")]
    public class GameStateSystem : ScriptableObject, IInit, ICleanUp
    {
        [Header("Variables")]
        [HorizontalLine(color: EColor.Green)]
        public GameStateVariable GameStateReference;
        [Header("State Instances")]
        [HorizontalLine(color: EColor.Green)]
        [BoxGroup()]
        public GameStateInstances StateInstances;

        private bool Lock;
        
        public void Init()
        {
            SetState(StateInstances.GameStartState);
        }

        private void SetStateFromName(string name)
        {
            SetState(StateInstances.GetStateFromName(name));
        }

        public void CleanUp()
        {
            GameStateReference.Value = null;
        }
        
        public void SetState(GameState state)
        {
            if (!Application.isPlaying) return;
            if (GameStateReference.Value != null && state.Name.CompareTo(GameStateReference.Value.Name) == 0) return;
            GameRoot.CoroutineRunner.StartCoroutine(ChangeState(state));
        }
        
        public IEnumerator ChangeState(GameState state)
        {
            if (Lock)
            {
                Debug.LogWarning("State change is locked!");
                yield break;
            }
            Lock = true;
            var oldState = GameStateReference.Value;
            if (oldState != null)
            {
                yield return GameRoot.CoroutineRunner.StartCoroutine(oldState.Exit());
            }
            GameStateReference.Value = state;
            yield return GameRoot.CoroutineRunner.StartCoroutine(GameStateReference.Value.Enter());
            Lock = false;
        }
        
        [Serializable]
        public class GameStateInstances
        {
            public GameStates.GameStartState GameStartState;
            public GameStates.GameShopState GameShopState;
            public GameStates.GamePlayingState GamePlayingState;
            public GameStates.GameOverState GameOverStateWin;
            public GameStates.GameOverState GameOverStateLose;

            public GameState GetStateFromName(string name)
            {
                foreach (var state in AllStates())
                {
                    if(state.Name.CompareTo(name) == 0) return state;
                }

                throw new Exception("GameState not found exception: " + name);
                return null;
            }

            public IEnumerable<GameState> AllStates()
            {
                yield return GameStartState;
                yield return GameShopState;
                yield return GamePlayingState;
                yield return GameOverStateWin;
                yield return GameOverStateLose;
            }

        }
    }
}