using DefaultNamespace;
using NaughtyAttributes;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Features.KeyInput
{
    [CreateAssetMenu(menuName = "KeyInputSystem")]
    public class KeyInputSystem : ScriptableObject, IInit, IExecute
    {
        public KeyCode SlimeGrowKey;
        public KeyCode SlimeShrinkKey;
        public KeyCode HammerGrowKey;
        public KeyCode HammerShrinkKey;
        public KeyCode AddForceKey;
        
        [HorizontalLine(2, EColor.Blue)]
        public BoolVariable SlimeGrow;
        public BoolVariable SlimeShrink;
        public BoolVariable HammerGrow;
        public BoolVariable HammerShrink;
        public BoolVariable AddForce;
        
        
        public void Init()
        {
            SlimeGrow.Value = false;
            SlimeShrink.Value = false;
            HammerGrow.Value = false;
            HammerShrink.Value = false;
            AddForce.Value = false;
        }

        public void Execute()
        {
            if (Input.GetKey(SlimeGrowKey))
            {
                SlimeGrow.Value = true;
            }
            else if (SlimeGrow.Value)
            {
                SlimeGrow.Value = false;
            }
            
            if (Input.GetKey(SlimeShrinkKey))
            {
                SlimeShrink.Value = true;
            }
            else if (SlimeShrink.Value)
            {
                SlimeShrink.Value = false;
            }
            
            if (Input.GetKey(HammerGrowKey))
            {
                HammerGrow.Value = true;
            }         
            else if (HammerGrow.Value)
            {
                HammerGrow.Value = false;
            }
            
            if (Input.GetKey(HammerShrinkKey))
            {
                HammerShrink.Value = true;
            }         
            else if (HammerShrink.Value)
            {
                HammerShrink.Value = false;
            }
            
            if (Input.GetKey(AddForceKey))
            {
                AddForce.Value = true;
            }               
            else if (AddForce.Value)
            {
                AddForce.Value = false;
            }
        }
    }
}