using DefaultNamespace;
using NaughtyAttributes;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Features.KeyInput
{
    [CreateAssetMenu(menuName = "Systems/KeyInputSystem")]
    public class KeyInputSystem : ScriptableObject, IInit, IExecute
    {
        public KeyCode SlimeGrowKey;
        public KeyCode SlimeShrinkKey;
        
        public KeyCode Left;
        public KeyCode Right;
        public KeyCode Up;

        [HorizontalLine(2, EColor.Blue)]
        public BoolVariable SlimeGrow;
        public BoolVariable SlimeShrink;
        public IntVariable MoveDirection;
        public BoolVariable JumpKey;
        public FloatVariable JumpKeyHoldDuration;
       
        
        public void Init()
        {
            SlimeGrow.Value = false;
            SlimeShrink.Value = false;
            MoveDirection.Value = 0;
            JumpKey.Value = false;
            JumpKeyHoldDuration.Value = 0;
        }

        public void Execute()
        {
            if (Input.GetKey(Up))
            {
                if(!JumpKey.Value) JumpKeyHoldDuration.Value = 0;
                JumpKey.Value = true;
                JumpKeyHoldDuration.Value += Time.deltaTime;
            }
            else
            {
                JumpKey.Value = false;
            }
            
            if (Input.GetKey(Left))
            {
                MoveDirection.Value = -1;
            }
            else if (Input.GetKey(Right))
            {
                MoveDirection.Value = 1;
            }
            else
            {
                MoveDirection.Value = 0;
            }
            
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
           
        }
    }
}