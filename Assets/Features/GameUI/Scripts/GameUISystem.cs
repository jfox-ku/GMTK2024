    using System;
    using System.Collections.Generic;
    using NaughtyAttributes;
    using ScriptableObjectArchitecture;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "GameUISystem", menuName = "Systems/GameUISystem")]
    public class GameUISystem : ScriptableObject, IInit, ICleanUp
    {
        [Header("Variables")]
        [HorizontalLine(color: EColor.Green)]
        public string UIParentName = "GameUI";
        public List<GameObject> GameUIPrefabs;

        [Header("Events Listen")]
        [HorizontalLine(color: EColor.Blue)]
        public StringGameEvent ShowUI;

        public GameEvent OnGameStart;
        public GameEvent OnGameLose;
        public GameEvent OnGamePlay;
        
        [Header("Private Variables")]
        [HorizontalLine(color: EColor.Red)]
        [ShowNonSerializedField]
        private Transform _uiParent;
        private Dictionary<string, GameUI> UIs = new Dictionary<string, GameUI>();
        
        private string _lastShownUI = "";
        
        public void Init()
        {
            _uiParent = GameObject.Find(UIParentName).transform;
            SpawnFromPrefab();
            RegisterFromScene();
            
            foreach (var ui in UIs.Values)
            {
                ui.Hide(false);
            }
            
            ShowUI.AddListener(OnShowUIHandler);
            OnGameStart.AddListener(OnGameStartHandler);
            OnGamePlay.AddListener(OnGamePlayHandler);
            OnGameLose.AddListener(OnGameLoseHandler);
            if (UIs.Count == 0) return;
            UIs[GameUI.UINames[0]].Show();
        }

        private void OnGameLoseHandler()
        {
            OnShowUIHandler("GameOver");
        }

        private void OnGamePlayHandler()
        {
            OnShowUIHandler("Gameplay");
        }

        private void OnGameStartHandler()
        {
            OnShowUIHandler("MainMenu");
        }

        private void OnShowUIHandler(string obj)
        {
            if (UIs.ContainsKey(obj))
            {
                UIs[obj].Show();
                if (UIs.ContainsKey(_lastShownUI))
                {
                    UIs[_lastShownUI].Hide();
                }

                _lastShownUI = obj;
            }
            else
            {
                throw new Exception("UI Not Found: " + obj);
            }
        }

        private void RegisterFromScene()
        {
            var gameUIs = GameObject.FindObjectsOfType<GameUI>();
            foreach (var ui in gameUIs)
            {
                if (!UIs.ContainsKey(ui.Name))
                {
                    UIs.Add(ui.Name, ui);
                }
                else
                {
                    throw new Exception("Duplicate UI Name: " + ui.Name);
                }
            }
        }

        private void SpawnFromPrefab()
        {
            foreach (var fab in GameUIPrefabs)
            {
                GameObject.Instantiate(fab, _uiParent);
            }
        }

        public void CleanUp()
        {
            ShowUI.RemoveListener(OnShowUIHandler);
            UIs.Clear();
        }
        
        
        
        
    }
}