﻿using System.Collections;
using DefaultNamespace;
using NaughtyAttributes;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Features.Slime
{
    [CreateAssetMenu(menuName = "SlimeControllerSystem")]
    public class SlimeControllerSystem: ScriptableObject, IInit, ICleanUp
    {
        [HorizontalLine(2, EColor.Green)]
        public GameObject SlimePrefab;

        [Expandable]
        public IntVariable SlimeMaxScale;
        [Expandable]
        public IntVariable SlimeMinScale;

        [HorizontalLine(2, EColor.Blue)]
        public float MaxMotorForce;
        public float TargetMotorVelocity;
        public float MotorForceLerpAmount = 0.1f;
        
        [HorizontalLine(2, EColor.Blue)]
        public BoolVariable SlimeGrow;
        public BoolVariable SlimeShrink;
        public BoolVariable HammerGrow;
        public BoolVariable HammerShrink;
        public BoolVariable AddForce;

        public BoolVariable IsPaused;
        
        private SlimeMono _slimeMono;
        
        public void Init()
        {
            _slimeMono = Instantiate(SlimePrefab).GetComponent<SlimeMono>();
            _slimeMono.SetJointMotor(0,0);
            GameRoot.CoroutineRunner.StartCoroutine(SlimeControlRoutine());
        }

        private IEnumerator SlimeControlRoutine()
        {
            while (true)
            {
                if (IsPaused.Value) yield return null;
                
                if(SlimeGrow.Value)
                {
                
                }
                else if(SlimeShrink.Value)
                {
                    _slimeMono.SetSlimeScale(_slimeMono.transform.localScale - Vector3.one * Time.deltaTime);
                }
            
                if(HammerGrow.Value)
                {
                    var clamped = Mathf.Clamp(_slimeMono.HammerScale.x, SlimeMinScale.Value, SlimeMaxScale.Value);
                    _slimeMono.SetHammerScale(_slimeMono.HammerScale + Vector3.one * Time.deltaTime);
                }
                else if(HammerShrink.Value)
                {
                    _slimeMono.transform.localScale -= Vector3.one * Time.deltaTime;
                }
            
                if(AddForce.Value)
                {
                    var hinge = _slimeMono.Joint;
                    var lerp = Mathf.Lerp(hinge.motor.force, MaxMotorForce, MotorForceLerpAmount);
                    _slimeMono.SetJointMotor(MaxMotorForce,lerp);
                }
                else
                {
                    var hinge = _slimeMono.Joint;
                    var lerp = Mathf.Lerp(hinge.motor.force, 0, MotorForceLerpAmount);
                    _slimeMono.SetJointMotor(MaxMotorForce,lerp);
                }

                yield return null;
            }

            yield return null;
        }

        public void CleanUp()
        {
            
        }
    }
}