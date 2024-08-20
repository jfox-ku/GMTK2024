using System;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Features.Eat
{
    public class Eater : MonoBehaviour
    {
        public FloatVariable Size;
        public Vector3 Center => transform.position;
        private void OnCollisionEnter(Collision collision)
        {
            var edible = collision.gameObject.GetComponent<Edible>();
            if (edible != null)
            {
                TryEat(edible);
            }
        }

        private void TryEat(Edible edible)
        {
            if (edible.Size <= Size.Value)
            {
                Size.Value += edible.Size * 0.5f;
                edible.EatenBy(this);
            }  
        }
    }
}