using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlendShapeController : MonoBehaviour
{
    [SerializeField] protected SkinnedMeshRenderer SkinnedMeshRenderer;
    public int BlendShapeIndex;

    public virtual void SetBlendShapeValue(float value)
    {
        SkinnedMeshRenderer.SetBlendShapeWeight(BlendShapeIndex, value);
    }
}
