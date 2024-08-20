using System;
using System.Collections;
using System.Drawing;
using DefaultNamespace;
using NaughtyAttributes;
using ScriptableObjectArchitecture;
using UnityEngine;
using Cinemachine;
using Color = UnityEngine.Color;

namespace Features.Slime
{
    [CreateAssetMenu(menuName = "Systems/SlimeControllerSystem")]
    public class SlimeControllerSystem: ScriptableObject, IInit, ICleanUp
    {
        [HorizontalLine(2, EColor.Green)]
        public GameObject SlimePrefab;
        public Vector3Variable SpawnPosition;
        
        public IntVariable MoveDirection;
        [Expandable]
        public FloatVariable MoveSpeed;
        
        [HorizontalLine(2, EColor.Blue)]
        public float MaxHoldToJump;
        public float MinHoldToJump;
        public BoolVariable JumpKey;
        [Expandable]
        public FloatVariable JumpKeyHoldDuration;
        [Expandable]
        public FloatVariable JumpStrength;
        public float BaseJumpStrength;
        [Expandable]
        public Vector3Variable JumpAngle;
        public BoolVariable IsGrounded;

        public FloatVariable EaterSize;
        public BoolVariable IsPaused;
        
        [HorizontalLine(2, EColor.Blue)]
        public GameEvent GamePlayingEvent;
        
        private CinemachineVirtualCamera _cinemachineCamera;
        private SlimeMono _slimeMono;
        private int _lastMoveDirection;
        private Vector3 _targetVelocity;
        
        public void Init()
        {
            JumpStrength.Value = BaseJumpStrength;
            GamePlayingEvent.AddListener(OnGamePlayingStateHandler);
            EaterSize.AddListener(OnSizeChange);
        }
        

        private void OnGamePlayingStateHandler()
        {
            _slimeMono = FindFirstObjectByType<SlimeMono>();
            _cinemachineCamera = FindObjectOfType<CinemachineVirtualCamera>();
            
            if (_cinemachineCamera != null)
            {
                _cinemachineCamera.Follow = _slimeMono.transform;
                _cinemachineCamera.LookAt = _slimeMono.transform;
            }

            GameRoot.CoroutineRunner.StopCoroutine(SlimeControlRoutine());
            GameRoot.CoroutineRunner.StartCoroutine(SlimeControlRoutine());
        }
        
       

        void OnSizeChange(float size)
        {
            if (_slimeMono == null || _slimeMono.SlimeRB == null) return;
            var edge = size; //(float)Math.Cbrt(size);
            _slimeMono.SetSlimeScale(edge * Vector3.one);
        }

        private IEnumerator SlimeControlRoutine()
        {
            _slimeMono.SlimeRB.useGravity = true;
            
            while (true)
            {
                if (_slimeMono == null)
                {
                    _slimeMono = FindFirstObjectByType<SlimeMono>();
                    yield return null;
                }
                if (IsPaused.Value) yield return null;
                
                if (MoveDirection.Value != 0)
                {
                    _lastMoveDirection = MoveDirection.Value;
                }
                
                _slimeMono.SetModelDirection(MoveDirection.Value,0.2f);
                
                if(IsGrounded.Value == false)
                {
                    JumpKeyHoldDuration.Value = 0;
                }
                
                if (!JumpKey.Value && IsGrounded.Value)
                {
                    var vel = _slimeMono.SlimeRB.velocity;
                    vel[0] = MoveDirection.Value * MoveSpeed.Value * Mathf.Sqrt(EaterSize.Value);
                    _slimeMono.SlimeRB.velocity = vel;
                }
                
                Jump();

                StationaryCheck();
                
                yield return null;
            }

            yield return null;
        }


        private float stationaryTimer = 0;
        private void StationaryCheck()
        {
            if (IsGrounded.Value == false && _slimeMono.SlimeRB.velocity.magnitude < 0.1f)
            {
                stationaryTimer += Time.deltaTime;
            }
            else
            {
                stationaryTimer = 0;
            }

            if (stationaryTimer > 2)
            {
                IsGrounded.Value = true;
            }
            
        }

        private void Jump()
        {
            if(IsGrounded.Value == false) return;
            
            var jumpDirDebug = new Vector3(JumpAngle.Value.x * _lastMoveDirection, JumpAngle.Value.y, JumpAngle.Value.z).normalized;
            Debug.DrawRay(_slimeMono.transform.position, jumpDirDebug * 2, Color.red);
            
            
            var shouldJump = 
                (JumpKeyHoldDuration.Value > MinHoldToJump && JumpKeyHoldDuration.Value < MaxHoldToJump && !JumpKey.Value) || 
                (JumpKeyHoldDuration.Value > MaxHoldToJump && JumpKey.Value);
            
            if(shouldJump == false) return;
            
            var jumpDir = new Vector3(JumpAngle.Value.x * _lastMoveDirection, JumpAngle.Value.y, JumpAngle.Value.z).normalized;
            _slimeMono.SlimeRB.AddForce(jumpDir * JumpStrength.Value * Mathf.Sqrt(EaterSize) * Mathf.Clamp(JumpKeyHoldDuration.Value,MinHoldToJump,MaxHoldToJump), ForceMode.VelocityChange);
            JumpKey.Value = false;
            JumpKeyHoldDuration.Value = 0;
            IsGrounded.Value = false;
            stationaryTimer = 0;
        }


        public void CleanUp()
        {
            EaterSize.Value = 1f;
        }
    }
}