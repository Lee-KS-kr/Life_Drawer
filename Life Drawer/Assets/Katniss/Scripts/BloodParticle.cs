using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Katniss
{
    public class BloodParticle : MonoBehaviour
    {
        private int penLayer;
        private int blindLineLayer;

        [SerializeField] private Rigidbody2D rig;
        [SerializeField] private Collider2D col;

        private IObjectPool<BloodParticle> bloodParticlePool;

        private void Start()
        {
            penLayer = LayerMask.NameToLayer("Pen");
            blindLineLayer = LayerMask.NameToLayer("Blind Line");
        }

        public void SetPool(IObjectPool<BloodParticle> pool)
        {
            bloodParticlePool = pool;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == penLayer || collision.gameObject.layer == blindLineLayer)
            {
                rig.isKinematic = true;
                //col.isTrigger = true;
            }
        }
    }
}