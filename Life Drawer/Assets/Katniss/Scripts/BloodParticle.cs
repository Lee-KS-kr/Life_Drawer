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
        private int hitCount;
        private float rayDistance;

        private LayerMask layerMask;

        private RaycastHit2D[] hits = new RaycastHit2D[1];

        [SerializeField] private Rigidbody2D rig;
        [SerializeField] private CircleCollider2D col;

        private IObjectPool<BloodParticle> bloodParticlePool;

        private Vector3[] directions =
        {
            new Vector3(-1, 1), new Vector3(0, 1), new Vector3(1, 1),
            new Vector3(-1, 0),                     new Vector3(1, 0),
            new Vector3(-1, -1), new Vector3(0, -1), new Vector3(1, -1)
        };

        private void Awake()
        {
            bloodLayer = LayerMask.NameToLayer("Blood Particle");
            penLayer = LayerMask.NameToLayer("Pen");
            blindLineLayer = LayerMask.NameToLayer("Blind Line");

            layerMask = 1 << bloodLayer | 1 << penLayer | 1 << blindLineLayer;
            rayDistance = col.radius * 4f;
        }

        public void SetPool(IObjectPool<BloodParticle> pool)
        {
            bloodParticlePool = pool;
        }

        public bool CheckByRay(int i)
        {
            hitCount = col.Raycast((Vector2)transform.position + (Vector2)directions[i], hits, rayDistance, layerMask);


            if (hitCount > 0)
            {
                return false;
            }
            else
                return true;
        }

        public void movePos(Vector3 pos, int i)
        {
            transform.position = pos + directions[i];
        }
    }
}