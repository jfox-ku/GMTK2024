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


        public BoolVariable IsPaused;
        
        private CinemachineVirtualCamera _cinemachineCamera;
        private SlimeMono _slimeMono;

        private Vector3 clampedValue;

        [HorizontalLine(2, EColor.Blue)]
        public GameEvent GamePlayingEvent;
        
        
        public void Init()
        {
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
                    _slimeMono.SetSlimeScale(ClampVectorComponents(_slimeMono.SlimeScale,SlimeMinScale,SlimeMaxScale) - Vector3.one * Time.deltaTime);
                    _slimeMono.SetSlimeMass(Mathf.Clamp((float)_slimeMono.SlimeScale.x,(float)SlimeMinScale.Value.x,(float)10));
                }
                
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
                    var velocityLerp = Vector3.Lerp(_slimeMono.SlimeRB.velocity, MoveDirection.Value * Vector3.right * MoveSpeed.Value, 0.25f);
                    _slimeMono.SlimeRB.velocity = velocityLerp;
                }

                
                Jump();
                

                
                yield return null;
            }

            yield return null;
        }

        private void Jump()
        {
            if(IsGrounded.Value == false || JumpKey.Value) return;
            var shouldJump = (JumpKeyHoldDuration.Value > MinHoldToJump && JumpKeyHoldDuration.Value < MaxHoldToJump ) 
                             || JumpKeyHoldDuration.Value > MaxHoldToJump;
            if(shouldJump == false) return;
            
            var jumpDir = new Vector3(JumpAngle.Value.x * _lastMoveDirection, JumpAngle.Value.y, JumpAngle.Value.z).normalized;
            //_slimeMono.SlimeRB.AddForce(jumpDir * JumpStrength.Value * JumpKeyHoldDuration.Value, ForceMode.VelocityChange);
            JumpKey.Value = false;
            JumpKeyHoldDuration.Value = 0;
            IsGrounded.Value = false;
        }


        public void CleanUp()
        {
            
        }
    }
}