using System.Collections;
using DefaultNamespace;
using NaughtyAttributes;
using ScriptableObjectArchitecture;
using UnityEngine;
using Cinemachine;

namespace Features.Slime
{
    [CreateAssetMenu(menuName = "Systems/SlimeControllerSystem")]
    public class SlimeControllerSystem: ScriptableObject, IInit, ICleanUp
    {
        [HorizontalLine(2, EColor.Green)]
        public GameObject SlimePrefab;

        [Expandable]
        public Vector3Variable SlimeMaxScale;
        [Expandable]
        public Vector3Variable SlimeMinScale;
        [Expandable]
        public Vector3Variable HammerMaxScale;
        [Expandable]
        public Vector3Variable HammerMinScale;
        [Expandable]
        public FloatVariable gravityMultiplier;

        [HorizontalLine(2, EColor.Blue)]
        public float MaxMotorForce;
        public float TargetMotorVelocity;
        public IntVariable MotorDirection;
        private int MotorDirectionValue => (int)Mathf.Sign(MotorDirection.Value);
        
        public float MotorForceLerpAmount = 0.1f;
        
        [HorizontalLine(2, EColor.Blue)]
        public BoolVariable SlimeGrow;
        public BoolVariable SlimeShrink;
        public BoolVariable HammerGrow;
        public BoolVariable HammerShrink;
        public BoolVariable AddForce;

        public BoolVariable IsPaused;
        
        private CinemachineVirtualCamera _cinemachineCamera;
        private SlimeMono _slimeMono;

        private Vector3 clampedValue;

        [HorizontalLine(2, EColor.Blue)]
        public GameEvent GamePlayingEvent;
        
        
        public void Init()
        {
            GamePlayingEvent.AddListener(OnGamePlatingStateHandler);
        }

        private void OnGamePlatingStateHandler()
        {
            _slimeMono = Instantiate(SlimePrefab).GetComponent<SlimeMono>();
            _slimeMono.SetJointMotor(0,0);
            
            _cinemachineCamera = FindObjectOfType<CinemachineVirtualCamera>();
            if (_cinemachineCamera != null)
            {
                _cinemachineCamera.Follow = _slimeMono.transform;
                _cinemachineCamera.LookAt = _slimeMono.transform;
            }

            GameRoot.CoroutineRunner.StartCoroutine(SlimeControlRoutine());
        }

        Vector3 ClampVectorComponents(Vector3 vector, Vector3 minVector, Vector3 maxVector)
        {
            float x = Mathf.Clamp(vector.x, minVector.x, maxVector.x);
            float y = Mathf.Clamp(vector.y, minVector.y, maxVector.y);
            float z = Mathf.Clamp(vector.z, minVector.z, maxVector.z);

            return new Vector3(x, y, z);
        }

        private IEnumerator SlimeControlRoutine()
        {
            while (true)
            {
                
                if (IsPaused.Value) yield return null;
                
                if(SlimeGrow.Value)
                {
                    _slimeMono.SetSlimeScale(ClampVectorComponents(_slimeMono.SlimeScale,SlimeMinScale,SlimeMaxScale) + Vector3.one * Time.deltaTime);
                    _slimeMono.SetSlimeMass(Mathf.Clamp(Mathf.Exp((float)_slimeMono.SlimeScale.x),(float)SlimeMinScale.Value.x,10));
                }
                else if(SlimeShrink.Value)
                {
                    
                    //var clamped = Mathf.Clamp(_slimeMono.SlimeScale.x, SlimeMinScale.Value, SlimeMaxScale.Value) * Vector3.one;
                    //_slimeMono.SetSlimeScale(_slimeMono.SlimeScale - Vector3.one * Time.deltaTime);
                    _slimeMono.SetSlimeScale(ClampVectorComponents(_slimeMono.SlimeScale,SlimeMinScale,SlimeMaxScale) - Vector3.one * Time.deltaTime);
                    _slimeMono.SetSlimeMass(Mathf.Clamp((float)_slimeMono.SlimeScale.x,(float)SlimeMinScale.Value.x,(float)10));
                }
            
                if(HammerGrow.Value)
                {
                    //_slimeMono.SetHammerScale(_slimeMono.HammerScale + Vector3.one * Time.deltaTime);
                    _slimeMono.SetHammerScale(ClampVectorComponents(_slimeMono.HammerScale,HammerMinScale,HammerMaxScale) + Vector3.one * Time.deltaTime);
                }
                else if(HammerShrink.Value)
                {
                    
                    //_slimeMono.SetHammerScale(_slimeMono.HammerScale - Vector3.one * Time.deltaTime);
                    //_slimeMono.transform.localScale -= Vector3.one * Time.deltaTime;
                    _slimeMono.SetHammerScale(ClampVectorComponents(_slimeMono.HammerScale,HammerMinScale,HammerMaxScale) - Vector3.one * Time.deltaTime);
                }

                if (_slimeMono.HammerRB.velocity.magnitude < 0.3f)
                {
                    _slimeMono.SetGravity(gravityMultiplier*0.1f);
                }
                else
                {
                    _slimeMono.SetGravity(gravityMultiplier*0.5f);
                }

                
                if(AddForce.Value)
                {
                    var hinge = _slimeMono.Joint;
                    var lerp = Mathf.Lerp(hinge.motor.force, MaxMotorForce, MotorForceLerpAmount);
                    _slimeMono.SetJointMotor(TargetMotorVelocity * MotorDirectionValue,lerp);
                   
                    
                   
                }
                else
                {
                    var hinge = _slimeMono.Joint;
                    var lerp = Mathf.Lerp(hinge.motor.force, 0, MotorForceLerpAmount);
                    _slimeMono.SetJointMotor(TargetMotorVelocity * MotorDirectionValue,lerp);
                 
                    
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