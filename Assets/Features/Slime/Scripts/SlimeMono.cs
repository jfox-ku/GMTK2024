using System;
using NaughtyAttributes;
using ScriptableObjectArchitecture;
using UnityEngine;
using Color = System.Drawing.Color;

namespace Features.Slime
{
    public class SlimeMono : MonoBehaviour
    {
        public Rigidbody SlimeRB;
        public Transform Model;
        public Vector3 SlimeScale => SlimeRB.transform.localScale;

        public BoolVariable IsGrounded;
        public FloatVariable SizeEater;
        
        private void Start()
        {
            IsGrounded.Value = true;
        }
        
        [Button()]
        public void UpdateFromSize()
        {
            SetSlimeScale(SizeEater.Value * Vector3.one);
        }

        public void SetModelDirection(int dir, float lerp = 1f)
        {
            var targetDir = dir == 0 ? 180f : dir >= 1 ? 140f : 220f;
            var lerpedDir = Mathf.Lerp(Model.transform.rotation.eulerAngles.y, targetDir, lerp);
            Model.transform.rotation = Quaternion.Euler(0f,lerpedDir,0f);
        }

        public void SetSlimeScale(Vector3 scale)
        {
            SlimeRB.transform.localScale = scale;
        }
        public void SetSlimeMass(float mass)
        {
            SlimeRB.mass = mass;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                var contact = collision.GetContact(0);
                if (contact.thisCollider.CompareTag("SlimeSideCollider"))
                {
                    return;
                }
                
                Debug.Log("Slime is grounded");
                IsGrounded.Value = true;
            }
            
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = UnityEngine.Color.red;
            var left = transform.position + Vector3.left * 2000f;
            var right = transform.position + Vector3.right * 2000f;
            Gizmos.DrawLine(left, right);
        }
    }
}