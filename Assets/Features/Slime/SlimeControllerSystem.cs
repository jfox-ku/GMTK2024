using System;
using System.Collections;
using System.Drawing;
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
        public Vector3Variable SpawnPosition;

        [Expandable]
        public Vector3Variable SlimeMaxScale;
        [Expandable]
        public Vector3Variable SlimeMinScale;

        [HorizontalLine(2, EColor.Blue)]
        public BoolVariable SlimeGrow;
        public BoolVariable SlimeShrink;
        public IntVariable MoveDirection;
        [Expandable]
        public FloatVariable MoveSpeed;
        public FloatVariable RotateLerpAmount;


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
        
        private CinemachineVirtualCamera _cinemachineCamera;
        private SlimeMono _slimeMono;

        private Vector3 clampedValue;

        [HorizontalLine(2, EColor.Blue)]
        public GameEvent GamePlayingEvent;
        
        
        public void Init()
        {
            EaterSize.AddListener(OnSizeChange);
            JumpStrength.Value = BaseJumpStrength;
            GamePlayingEvent.AddListener(OnGamePlayingStateHandler);
        }
        

        private void OnGamePlayingStateHandler()
        {
            _slimeMono = Instantiate(SlimePrefab).GetComponent<SlimeMono>();
            _slimeMono.transform.position = SpawnPosition.Value;
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
        
        private int _lastMoveDirection;
        private Vector3 _targetVelocity;

        void OnSizeChange(float size)
        {
            var edge = (float)Math.Cbrt(size);
            _slimeMono.SetSlimeScale(new Vector3(edge,edge,edge));
        }

        private IEnumerator SlimeControlRoutine()
        {
            while (true)
            {
                if (IsPaused.Value) yield return null;
                
                if (MoveDirection.Value != 0)
                {
                    _lastMoveDirection = MoveDirection.Value;
                }
                
                if(IsGrounded.Value == false)
                {
                    JumpKeyHoldDuration.Value = 0;
                }
                
                if (!JumpKey.Value && IsGrounded.Value)
                {
                    var vel = _slimeMono.SlimeRB.velocity;
                    vel[0] = MoveDirection.Value * MoveSpeed.Value;
                    _slimeMono.SlimeRB.velocity = vel;
                }
                
                Jump();

                //StationaryCheck();
                
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
            var shouldJump = 
                (JumpKeyHoldDuration.Value > MinHoldToJump && JumpKeyHoldDuration.Value < MaxHoldToJump && !JumpKey.Value) || 
                (JumpKeyHoldDuration.Value > MaxHoldToJump && JumpKey.Value);
            
            if(shouldJump == false) return;
            
            var jumpDir = new Vector3(JumpAngle.Value.x * _lastMoveDirection, JumpAngle.Value.y, JumpAngle.Value.z).normalized;
            _slimeMono.SlimeRB.AddForce(jumpDir * JumpStrength.Value * Mathf.Clamp(JumpKeyHoldDuration.Value,MinHoldToJump,MaxHoldToJump), ForceMode.VelocityChange);
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