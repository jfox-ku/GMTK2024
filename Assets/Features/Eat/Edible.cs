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
            Gizmos.DrawWireSphere(transform.position, Size);
        }

        public void EatenBy(Eater eater)
        {
            var colliders = transform.GetComponentsInChildren<Collider>();
            foreach (var c in colliders)
            {
                c.enabled = false;
            }

            var eatSeq = DOTween.Sequence();
            eatSeq.Append(transform.DOMove(eater.Center, 0.5f));
            eatSeq.Join(transform.DOScale(Vector3.zero, 0.5f));
            eatSeq.OnComplete(()=>Destroy(gameObject));

        }
    }
}