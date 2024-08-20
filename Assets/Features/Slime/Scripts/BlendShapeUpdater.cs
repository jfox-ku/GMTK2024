using System;
using DG.Tweening;
using Features.Eat;
using NaughtyAttributes;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Features.Slime
{
    public class BlendShapeUpdater : BlendShapeController
    {
        [Header("Move")]
        [HorizontalLine(color: EColor.Green)]
        public IntVariable MoveDirection;

        [Header("Jump")]
        public Rigidbody TargetRB;
        [HorizontalLine(color: EColor.Green)]
        public BoolVariable IsGrounded;
        public FloatVariable JumpKeyHoldDuration;
        public AnimationCurve JumpDurationCurve;
        public AnimationCurve AirTimeCurve;

        private bool RbFallingDown => TargetRB.velocity.y < -0.1f;

        [Header("Eat")]
        [HorizontalLine(color: EColor.Green)]
        
        [Header("EyeIdle")]
        [HorizontalLine(color: EColor.Green)]
        

        private float _jumpBlend;
        private float _airTime;
        private float _lastFrameYVelocity;

        private Tween _bodyIdle;
        private Tween _eyesIdle;
        
        private void Start()
        {
            Eater.OnEat += OnEat;
            IsGrounded.AddListener(OnGrounded);

            _eyesIdle = UpdateWithTween(EyesIdle, 1.5f, 30, -1);
            _bodyIdle = UpdateWithTween(BodyIdle, 3, 100, -1);
        }

        private void LateUpdate()
        {
            _lastFrameYVelocity = TargetRB.velocity.y;
        }

        private Tween _groundedTween;
        private void OnGrounded(bool isGrounded)
        {
            if (isGrounded)
            {
                _groundedTween?.Kill();
                var maxVal = Mathf.Abs(_lastFrameYVelocity) > 8f ? 75f : 30f;
                var dur = Mathf.Abs(_lastFrameYVelocity) > 8f ? 0.35f : 0.2f;
                _groundedTween = UpdateWithTween(Grounded, dur, maxVal, 2);
            }
        }

        private void Grounded(float percent)
        {
            SetBlendShape(SlimeBlendShapes.Melt, percent);
            SetBlendShape(SlimeBlendShapes.Eyes_angry,percent);
        }

        private void BodyIdle(float percent)
        {
            SetBlendShape(SlimeBlendShapes.Noise_h, percent);
            SetBlendShape(SlimeBlendShapes.Noise_v, percent);
        }

        private void EyesIdle(float percent)
        {
            SetBlendShape(SlimeBlendShapes.Eye_left_grow, percent);
            SetBlendShape(SlimeBlendShapes.Eye_right_grow, percent);
            SetBlendShape(SlimeBlendShapes.Eyes_roll1, percent/2f);
        }

        private void OnDestroy()
        {
            Eater.OnEat -= OnEat;
            _groundedTween?.Kill();
            _eatTween?.Kill();
            _bodyIdle?.Kill();
            _eyesIdle?.Kill();
            
        }

        private void Update()
        {
            if (IsGrounded.Value)
            {
                _airTime = 0f;
                LerpBlendShape(SlimeBlendShapes.Fall,0,0.33f);
                LerpBlendShape(SlimeBlendShapes.Jump,0,0.33f);
            }
            else
            {
                _airTime += Time.deltaTime;
                var shape = RbFallingDown ? SlimeBlendShapes.Fall : SlimeBlendShapes.Jump;
                var otherShape = !RbFallingDown ? SlimeBlendShapes.Fall : SlimeBlendShapes.Jump;
                LerpBlendShape(shape, AirTimeCurve.Evaluate(_airTime),0.1f);
                LerpBlendShape(otherShape, 0,0.1f);
            }
            
            if(JumpKeyHoldDuration.Value > 0)
            {
                _jumpBlend = JumpDurationCurve.Evaluate(JumpKeyHoldDuration.Value);
                SetBlendShape(SlimeBlendShapes.Shrink, _jumpBlend);
            }
            else
            {
                _jumpBlend = Mathf.Lerp(_jumpBlend, 0, 0.33f);
                SetBlendShape(SlimeBlendShapes.Shrink, _jumpBlend);
            }
            
        }
        
        private Tween _eatTween;
        public void OnEat()
        {
            _eatTween?.Kill();
            _eatTween = UpdateWithTween(SetEat, 1, 100, 2);

            void SetEat(float percent)
            {
                SetBlendShape(SlimeBlendShapes.Eyes_happy,percent);
            }
        }

        public Tween UpdateWithTween(Action<float> onUpdate, float duration, float maxValue = 100f, int yoyoCount = 1)
        {
            maxValue = Mathf.Clamp(maxValue, 0, 100);
            var target = 0f;
            var tween = DOTween.To(()=> target, x => target = x,maxValue, duration)
                .From(0f)
                .SetLoops(yoyoCount, LoopType.Yoyo)
                .OnUpdate(()=> onUpdate(target));
            return tween;
        }
        
    }
}