using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using NaughtyAttributes;
using ScriptableObjectArchitecture;
using UnityEngine;

public class SizeAdjustCamera : MonoBehaviour
{
    public Vector3 BaseOffset;
    public Vector3 OffsetPerSize;
    public FloatVariable SizeEater;
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineTransposer _transposer;
    
    
    void Start()
    {
        _transposer = VirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        SizeEater.AddListener(AdjustForSize);
        UpdateFromSize();
    }

    [Button()]
    public void UpdateFromSize()
    {
        AdjustForSize(SizeEater.Value);
    }

    private Tween _offsetTween;
    private void AdjustForSize(float size)
    {
        _offsetTween?.Kill();
        _offsetTween = DOTween.To(()=>_transposer.m_FollowOffset, x => _transposer.m_FollowOffset = x, BaseOffset + OffsetPerSize * size, 0.5f);
    }
}
