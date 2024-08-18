using System;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Features.Slime
{
    public class SlimeMono : MonoBehaviour
    {
        public Rigidbody SlimeRB;
        public Rigidbody HammerRB;
        public HingeJoint Joint;
        
        public Vector3 SlimeScale => SlimeRB.transform.localScale;
        public Vector3 HammerScale => HammerRB.transform.localScale;

        private int massMultiplier = 1;
        private float forceMult;
        
        
        public void SetSlimeScale(Vector3 scale)
        {
            SlimeRB.transform.localScale = scale;
            //SlimeRB.mass = scale.x * massMultiplier;
            forceMult = scale.x;
        }
        public void SetSlimeMass(float mass)
        {
            SlimeRB.mass = mass;
        }

        public void SetGravity(float gravityMult)
        {
            SlimeRB.AddForce(Physics.gravity * gravityMult * SlimeRB.mass);
        }
        
        
        public void SetHammerScale(Vector3 scale)
        {
   
            HammerRB.transform.localScale = scale;
            HammerRB.mass = scale.x * massMultiplier;
        }
        
        public void SetHammerMass(float mass)
        {
            HammerRB.mass = mass;
        }
        
        public void SetJointMotor(float targetVel, float force)
        {
            Joint.useMotor = true;
            Joint.motor = new JointMotor
            {
                targetVelocity = targetVel,
                force = force
            };
        }

    }
}