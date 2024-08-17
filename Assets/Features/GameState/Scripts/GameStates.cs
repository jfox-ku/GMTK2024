using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace DefaultNamespace
{
    public static partial class GameStates
    {
        [Serializable]
        public class GameStartState : GameState
        {
            public GameEvent OnGameStart;

            public override IEnumerator Enter()
            {
                Debug.Log("Entering GameStartState");
                yield return null;
                OnGameStart.Raise();
            }

            public override IEnumerator Exit()
            {
                Debug.Log("Exiting GameStartState");
                yield return null;
                yield break;
            }
        }
        
        [Serializable]
        public class GameShopState : GameState
        {
            public GameEvent OnGameShop;

            public override IEnumerator Enter()
            {
                Debug.Log("Entering GameShopState");
                yield return null;
                OnGameShop.Raise();
            }

            public override IEnumerator Exit()
            {
                Debug.Log("Exiting GameShopState");
                yield return null;
                yield break;
            }
        }
        
        [Serializable]
        public class GamePlayingState : GameState
        {
            public GameEvent OnGamePlaying;

            public override IEnumerator Enter()
            {
                Debug.Log("Entering GamePlayingState");
                yield return null;
                OnGamePlaying.Raise();
            }

            public override IEnumerator Exit()
            {
                Debug.Log("Exiting GamePlayingState");
                yield return null;
                yield break;
            }
        }
        
        [Serializable]
        public class GameOverState : GameState
        {
            public bool IsWin;
            public GameEvent OnGameOver;

            public override IEnumerator Enter()
            {
                Debug.Log("Entering GameOverState");
                yield return null;
                OnGameOver.Raise();
            }

            public override IEnumerator Exit()
            {
                Debug.Log("Exiting GameOverState");
                yield return null;
                yield break;
            }
        }
    }
}