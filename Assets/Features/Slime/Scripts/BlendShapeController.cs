using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlendShapeController : MonoBehaviour
{
    [SerializeField] protected SkinnedMeshRenderer SkinnedMeshRenderer;

    public enum SlimeBlendShapes
    {
        Eyes_grow = 0,
        Eyes_shrink = 1,
        Eyes_happy = 2,
        Eyes_angry = 3,
        Eyes_roll1 = 4,
        Eyes_roll2 = 5,
        Eye_left_grow = 6,
        Eye_right_grow = 7,
        Jump = 8,
        Fall = 9,
        Shrink = 10,
        Melt = 11,
        Hit_left = 12,
        Hit_right = 13,
        grow_h = 14,
        grow_v = 15,
        Get_legs = 16,
        Get_spikes = 17,
        Evil = 18,
        Noise_h = 19,
        Noise_v = 20,
    }

    public void SetBlendShape(SlimeBlendShapes shape, float value)
    {
        if (!SkinnedMeshRenderer) return;
        SkinnedMeshRenderer.SetBlendShapeWeight((int)shape,value);
    }

    public void LerpBlendShape(SlimeBlendShapes shape, float value, float lerp)
    {
        if (!SkinnedMeshRenderer) return;
        var weight = SkinnedMeshRenderer.GetBlendShapeWeight((int)shape);
        var newValue = Mathf.Lerp(weight, value, lerp);
        SkinnedMeshRenderer.SetBlendShapeWeight((int)shape,newValue);
    }
    
    
}