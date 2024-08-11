using System;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace DefaultNamespace
{
    public interface IShow
    {
        Tween Show(bool animate = true);
    }
    
    public interface IHide
    {
        Tween Hide(bool animate = true);
    }
    
    public class GameUI : MonoBehaviour, IShow, IHide
    {
        public static readonly List<string> UINames = new List<string>()
        {
            "MainMenu",
            "EditMenu", 
            "Gameplay", 
            "Pause", 
            "RoundOver",
            "GameOver", 
        };
        [Dropdown("UINames")]
        public string Name;
        public CanvasGroup CanvasGroup;
        private bool _isShown;
        
        private void Awake()
        {
            CanvasGroup.alpha = 0;
        }
        
        public Tween Show(bool animate = true)
        {
            var tw = CanvasGroup.DOFade(1f, 0.25f);
            _isShown = true;
            return tw;
        }

        public Tween Hide(bool animate = true)
        {
            var tw = CanvasGroup.DOFade(0f, 0.25f);
            _isShown  = false;
            return tw;
        }

#if UNITY_EDITOR

        [EnableIf("CanShow")]
        [Button()]
        public void ShowTest()
        {
            Show();
        }
        [EnableIf("CanHide")]
        [Button()]
        public void HideTest()
        {
            Hide();
        }

        bool CanShow()
        {
            return !_isShown && Application.isPlaying;
        }
        
        bool CanHide()
        {
            return _isShown && Application.isPlaying;
        }
        
        
#endif
    }
}