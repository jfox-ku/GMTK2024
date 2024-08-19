using System;
using UnityEngine;

namespace Features.Eat
{
    public class Eater : MonoBehaviour
    {
        public float Size;
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
            if (edible.Size <= Size)
            {
                Size += edible.Size * 0.1f;
                edible.EatenBy(this);
            }  
        }
    }
}