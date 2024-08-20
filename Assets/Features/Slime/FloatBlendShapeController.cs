using ScriptableObjectArchitecture;
using UnityEngine;

namespace Features.Slime
{
    public class FloatBlendShapeController : BlendShapeController
    {
        public FloatVariable FloatVariable;
        public AnimationCurve BlendShapeCurve;
        public AnimationCurve BlendShapeCurve2;
        public IntVariable IntVariableMoveDirection;
        public FloatVariable FloatVariableJump;
        public BoolVariable IsGrounded;

        [SerializeField] private float speed;
        private float elapsedTime = 0f;
        public float duration = 4f;
        
        
        private void Update()
        {
         
            SetBlendShapeValue(BlendShapeCurve.Evaluate(FloatVariable.Value));
            SetBlendShapeDirection(Mathf.Abs(IntVariableMoveDirection.Value));
            
            if (IsGrounded || IntVariableMoveDirection.Value == 0) 
            {
                SetIdleBlendShapeState(15,20f);
            }
            else
            {
                elapsedTime = 0;
                SetBlendShapeJumpState(FloatVariableJump.Value);
            }
        }

        public override void SetIdleBlendShapeState(int index,float value)
        {
            SkinnedMeshRenderer.SetBlendShapeWeight(index,Mathf.Lerp(0, value, Mathf.PingPong(Time.time * speed, 1f)));
        }

        public override void SetBlendShapeValue(float value)
        {
            SkinnedMeshRenderer.SetBlendShapeWeight(BlendShapeIndex, BlendShapeCurve.Evaluate(value));
        }

        public override void SetBlendShapeDirection(int value)
        {
            SkinnedMeshRenderer.SetBlendShapeWeight(BlendShapeMoveIndex,Mathf.Lerp(0, value *100f, Mathf.PingPong(Time.time * speed, 1f)));
            SkinnedMeshRenderer.SetBlendShapeWeight(BlendShapeMoveIndex + 1,Mathf.Lerp(0, value *100f, Mathf.PingPong(Time.time * speed, 2f)));
            Debug.Log(value);
        }
        
        public override void SetBlendShapeJumpState(float targetValue)
        {
            
            elapsedTime += Time.deltaTime * 10;
            
            float t = elapsedTime / duration; 
            
            float currentWeight = SkinnedMeshRenderer.GetBlendShapeWeight(BlendShapeJumpIndex);
    
           
            float newWeight = Mathf.Lerp(currentWeight, targetValue, t);
    
          
            SkinnedMeshRenderer.SetBlendShapeWeight(BlendShapeJumpIndex, newWeight);
            
            
        }
        

        public void SetBlendShapeMeltToZero(float targetValue)
        {
            elapsedTime += Time.deltaTime * 100f;

            // Calculate the progress (0 to 1)
            float t = elapsedTime / 2f;
          
            SkinnedMeshRenderer.SetBlendShapeWeight(BlendShapeIndex, Mathf.Lerp(targetValue, 0f, t));
            
        }
    }
}