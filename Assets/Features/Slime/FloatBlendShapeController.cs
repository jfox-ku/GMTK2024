using ScriptableObjectArchitecture;
using UnityEngine;

namespace Features.Slime
{
    public class FloatBlendShapeController : BlendShapeController
    {
        public FloatVariable FloatVariable;
        public AnimationCurve BlendShapeCurve;
        
        private void Update()
        {
            SetBlendShapeValue(BlendShapeCurve.Evaluate(FloatVariable.Value));
        }

        public override void SetBlendShapeValue(float value)
        {
            SkinnedMeshRenderer.SetBlendShapeWeight(BlendShapeIndex, BlendShapeCurve.Evaluate(value));
        }
    }
}