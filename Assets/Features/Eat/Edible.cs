using System;
using DG.Tweening;
using UnityEngine;

namespace Features.Eat
{
    public class Edible : MonoBehaviour
    {
        public float Size;

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, Vector3.one * Size);
        }

        public void EatenBy(Eater eater)
        {
            var colliders = transform.GetComponentsInChildren<Collider>();
            foreach (var c in colliders)
            {
                c.enabled = false;
            }

            transform.DOMove(eater.Center, 0.5f).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }
    }
}