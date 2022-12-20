using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Katniss
{
    public class BloodParticle : MonoBehaviour
    {
        //private int blindLineLayer;
        private int penLayer;
        private int bloodLayer;
        private int layerMask;

        [SerializeField] private Rigidbody2D rig;
        [SerializeField] private CircleCollider2D col;

        private IObjectPool<BloodParticle> bloodParticlePool;

        private Vector2[] moveDirs =
        {
            new Vector2(-1, 1), new Vector2(0, 1), new Vector2(1, 1),
            new Vector2(-1, 0),                     new Vector2(1, 0),
            new Vector2(-1, -1), new Vector2(0, -1), new Vector2(1, -1)
        };

        private void Start()
        {
            //blindLineLayer = LayerMask.NameToLayer("Blind Line");
            penLayer = LayerMask.NameToLayer("Pen");
            bloodLayer = LayerMask.NameToLayer("Blood Particle");
            layerMask = 1 << 12;
        }

        public void SetPool(IObjectPool<BloodParticle> pool)
        {
            bloodParticlePool = pool;
        }

        //private void OnCollisionEnter2D(Collision2D collision)
        //{
        //    if (collision.gameObject.layer == penLayer || collision.gameObject.layer == blindLineLayer)
        //    {
        //        rig.isKinematic = true;
        //        //col.isTrigger = true;
        //    }
        //}

        public void FindPath()
        {
            // 8방향으로 레이를 쏴서 나와 같은 것이나 선이 없으면 새로 하나를 만든다
            float distance = col.radius * 5;
            for(int i=0,size = moveDirs.Length; i < size; i++)
            {
                var hit = Physics2D.Raycast((Vector2)transform.position, moveDirs[i], distance, layerMask);
                if (!hit)
                {
                    Debug.Log("bb");
                    continue;
                }
                else
                    Instantiate(gameObject, (Vector2)transform.position + moveDirs[i], Quaternion.identity);

                //if (Physics2D.Raycast((Vector2)transform.position, (Vector2)transform.position + moveDirs[i], distance, layerMask).collider != null)
                //    continue;
                //else
                //    Instantiate(gameObject, (Vector2)transform.position + moveDirs[i], Quaternion.identity);

                //var hit = Physics2D.Raycast((Vector2)transform.position, (Vector2)transform.position + moveDirs[i], distance);
                //Debug.Log(hit.collider.gameObject.name);
                //if (hit)
                //    continue;
                //else
                //    Instantiate(gameObject, (Vector2)transform.position + moveDirs[i], Quaternion.identity);

            }
        }
    }
}