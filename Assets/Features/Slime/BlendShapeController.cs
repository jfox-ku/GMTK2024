using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlendShapeController : MonoBehaviour
{
    [SerializeField] protected SkinnedMeshRenderer SkinnedMeshRenderer;
    public int BlendShapeIndex;
    public int BlendShapeMoveIndex;
    public int BlendShapeJumpIndex;

    public virtual void SetBlendShapeValue(float value)
    {
        SkinnedMeshRenderer.SetBlendShapeWeight(BlendShapeIndex, value);
    }

    public virtual void SetBlendShapeDirection(int value)
    {
        SkinnedMeshRenderer.SetBlendShapeWeight(BlendShapeMoveIndex,value);
    }
    
    public virtual void SetBlendShapeJumpState(float value)
    {
        SkinnedMeshRenderer.SetBlendShapeWeight(BlendShapeJumpIndex,value);
    }
    
    public virtual void SetIdleBlendShapeState(int index,float value)
    {
        SkinnedMeshRenderer.SetBlendShapeWeight(index,value);
    }
    
}
