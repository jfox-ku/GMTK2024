    using System;
    using System.Collections.Generic;
    using NaughtyAttributes;
    using ScriptableObjectArchitecture;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "GameUISystem", menuName = "Game/GameUISystem")]
    public class GameUISystem : ScriptableObject, IInit, ICleanUp
    {
        [Header("Variables")]
        [HorizontalLine(color: EColor.Green)]
        public string UIParentName = "GameUI";
        public List<GameObject> GameUIPrefabs;

        [Header("Events Listen")]
        [HorizontalLine(color: EColor.Blue)]
        public StringGameEvent ShowUI;
        
        [Header("Private Variables")]
        [HorizontalLine(color: EColor.Red)]
        [ShowNonSerializedField]
        private Transform _uiParent;
        private Dictionary<string, GameUI> UIs = new Dictionary<string, GameUI>();
        
        public void Init()
        {
            _uiParent = GameObject.Find(UIParentName).transform;
            SpawnFromPrefab();
            RegisterFromScene();
            ShowUI.AddListener(OnShowUIHandler);

            if (UIs.Count == 0) return;
            UIs[GameUI.UINames[0]].Show();
        }

        private void OnShowUIHandler(string obj)
        {
            if (UIs.ContainsKey(obj))
            {
                UIs[obj].Show();
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