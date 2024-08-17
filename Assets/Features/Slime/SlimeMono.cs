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

        public void SetSlimeScale(Vector3 scale)
        {
            SlimeRB.transform.localScale = scale;
        }
        
        public void SetHammerScale(Vector3 scale)
        {
   
            HammerRB.transform.localScale = scale;
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