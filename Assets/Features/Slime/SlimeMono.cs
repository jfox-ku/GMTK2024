using System;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Features.Slime
{
    public class SlimeMono : MonoBehaviour
    {
        public Rigidbody SlimeRB;
        public Vector3 SlimeScale => SlimeRB.transform.localScale;

        public BoolVariable IsGrounded;

        private void Start()
        {
            IsGrounded.Value = true;
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
                if(collision.GetContact(0).point.y > SlimeRB.transform.position.y)
                    return;
                Debug.Log("Slime is grounded");
                IsGrounded.Value = true;
            }
            
        }
    }
}