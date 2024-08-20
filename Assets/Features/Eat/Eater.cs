using System;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Features.Eat
{
    public class Eater : MonoBehaviour
    {
        public static event Action OnEat;
        public FloatVariable Size;
        public Vector3 Center => transform.position;
        private void OnCollisionEnter(Collision collision)
        {
            var edible = collision.gameObject.GetComponent<Edible>();
            if (edible == null)
                edible = collision.gameObject.GetComponentInParent<Edible>();
            if (edible != null)
            {
                TryEat(edible);
            }
        }

        private void TryEat(Edible edible)
        {
            if (edible.Size <= Size.Value)
            {
                Size.Value += edible.Size * 0.25f;
                edible.EatenBy(this);
                OnEat?.Invoke();
            }  
        }
    }
}