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
                yield return new WaitForSeconds(1f);
                OnGameStart.Raise();
            }

            public override IEnumerator Exit()
            {
                Debug.Log("Exiting GameStartState");
                yield return new WaitForSeconds(1f);
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
                yield return new WaitForSeconds(1f);
                OnGameShop.Raise();
            }

            public override IEnumerator Exit()
            {
                Debug.Log("Exiting GameShopState");
                yield return new WaitForSeconds(1f);
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
                yield return new WaitForSeconds(1f);
                OnGamePlaying.Raise();
            }

            public override IEnumerator Exit()
            {
                Debug.Log("Exiting GamePlayingState");
                yield return new WaitForSeconds(1f);
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
                yield return new WaitForSeconds(1f);
                OnGameOver.Raise();
            }

            public override IEnumerator Exit()
            {
                Debug.Log("Exiting GameOverState");
                yield return new WaitForSeconds(1f);
                yield break;
            }
        }
    }
}