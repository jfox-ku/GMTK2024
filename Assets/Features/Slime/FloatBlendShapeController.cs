using ScriptableObjectArchitecture;
using UnityEngine;

namespace Features.Slime
{
    public class FloatBlendShapeController : BlendShapeController
    {
        public FloatVariable FloatVariable;
        public AnimationCurve BlendShapeCurve;
        public FloatVariable FloatVariableMoveSpeed;

        [SerializeField] private float speed;
        
        private void Update()
        {
            SetBlendShapeValue(BlendShapeCurve.Evaluate(FloatVariable.Value));
            SetBlendShapeDirection(BlendShapeCurve.Evaluate(FloatVariableMoveSpeed.Value));
        }

        public override void SetBlendShapeValue(float value)
        {
            SkinnedMeshRenderer.SetBlendShapeWeight(BlendShapeIndex, BlendShapeCurve.Evaluate(value));
        }

        public override void SetBlendShapeDirection(float value)
        {
            SkinnedMeshRenderer.SetBlendShapeWeight(BlendShapeMoveIndex,Mathf.Lerp(0, FloatVariableMoveSpeed, Mathf.PingPong(Time.time * speed, 1f)));
        }
    }
}