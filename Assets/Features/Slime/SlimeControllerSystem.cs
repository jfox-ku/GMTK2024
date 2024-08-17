using DefaultNamespace;
using NaughtyAttributes;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Features.Slime
{
    [CreateAssetMenu(menuName = "SlimeControllerSystem")]
    public class SlimeControllerSystem: ScriptableObject, IInit, IExecute, IFixedExecute
    {
        [HorizontalLine(2, EColor.Blue)]
        public BoolVariable SlimeGrow;
        public BoolVariable SlimeShrink;
        public BoolVariable HammerGrow;
        public BoolVariable HammerShrink;
        public BoolVariable AddForce;
        
        
        public void Init()
        {
            SlimeGrow.AddListener(OnSlimeGrow);
        }

        private void OnSlimeGrow(bool arg0)
        {
            
        }

        public void Execute()
        {
            
        }

        public void FixedExecute()
        {
            
        }
    }
}