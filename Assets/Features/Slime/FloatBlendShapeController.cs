using ScriptableObjectArchitecture;
using UnityEngine;

namespace Features.Slime
{
    public class FloatBlendShapeController : BlendShapeController
    {
        public FloatVariable FloatVariable;
        public AnimationCurve BlendShapeCurve;
        public AnimationCurve BlendShapeCurve2;
        public FloatVariable FloatVariableMoveSpeed;
        public FloatVariable FloatVariableJump;
        public BoolVariable IsGrounded;

        [SerializeField] private float speed;
        private float elapsedTime = 0f;
        public float duration = 4f;
        
        
        private void Update()
        {
         
            SetBlendShapeValue(BlendShapeCurve.Evaluate(FloatVariable.Value));
            SetBlendShapeDirection(BlendShapeCurve.Evaluate(FloatVariableMoveSpeed.Value));
            if (IsGrounded)
            {
                SetBlendShapeJumpState(0);
            }
            else
            {
                elapsedTime = 0;
                SetBlendShapeMeltToZero(100f);
                SetBlendShapeJumpState(FloatVariableJump.Value);
            }
        }

        public override void SetBlendShapeValue(float value)
        {
            SkinnedMeshRenderer.SetBlendShapeWeight(BlendShapeIndex, BlendShapeCurve.Evaluate(value));
        }

        public override void SetBlendShapeDirection(float value)
        {
            SkinnedMeshRenderer.SetBlendShapeWeight(BlendShapeMoveIndex,Mathf.Lerp(0, FloatVariableMoveSpeed, Mathf.PingPong(Time.time * speed, 1f)));
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
            elapsedTime += Time.deltaTime * 50;

            // Calculate the progress (0 to 1)
            float t = elapsedTime / 2f;
          
            SkinnedMeshRenderer.SetBlendShapeWeight(BlendShapeIndex, Mathf.Lerp(targetValue, 0f, t));
            
        }
    }
}