using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Katniss
{
    public class BloodParticle : MonoBehaviour
    {
        private int bloodLayer;
        private int penLayer;
        private int blindLineLayer;
        private float rayDistance = 100f;

        private LayerMask bloodLayerMask;
        private RaycastHit2D hit;

        [SerializeField] private Rigidbody2D rig;
        [SerializeField] private CircleCollider2D col;

        private IObjectPool<BloodParticle> bloodParticlePool;

        private Vector3[] directions =
        {
            new Vector3(-1, 1), new Vector3(0, 1), new Vector3(1, 1),
            new Vector3(-1, 0),                     new Vector3(1, 0),
            new Vector3(-1, -1), new Vector3(0, -1), new Vector3(1, -1)
        };

        private void Start()
        {
            bloodLayer = LayerMask.NameToLayer("Blood Particle");
            penLayer = LayerMask.NameToLayer("Pen");
            blindLineLayer = LayerMask.NameToLayer("Blind Line");

            bloodLayerMask = 1 << bloodLayer;
        }

        public void SetPool(IObjectPool<BloodParticle> pool)
        {
            bloodParticlePool = pool;
        }

        public bool CheckByRay(int i)
        {
            hit = Physics2D.Raycast(transform.position, transform.position + directions[i], rayDistance, 1 << 21);

            if (hit)
            {
                Debug.Log("blocked");
                return false;
            }
            else
                return true;
        }
    }
}