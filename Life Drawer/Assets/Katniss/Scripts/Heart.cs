using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Katniss
{
    public class Heart : MonoBehaviour
    {
        private Vector3 bloodParticlePosition = new Vector3(0, 0, 0);

        private int bloodParticlePoolMaxSize = 300;

        [SerializeField] BloodParticle bloodParticlePrefab;

        private IObjectPool<BloodParticle> bloodParticlePool;

        private void Awake()
        {
            bloodParticlePool = new ObjectPool<BloodParticle>(
                CreateNewBloodParticle,
                GetBloodParticle,
                ReleaseBloodParticle,
                DestroyBloodParticle,
                maxSize: bloodParticlePoolMaxSize
                );
        }

        private BloodParticle CreateNewBloodParticle()
        {
            BloodParticle bloodParticle = Instantiate(bloodParticlePrefab);
            bloodParticle.SetPool(bloodParticlePool);
            return bloodParticle;
        }

        private void GetBloodParticle(BloodParticle bloodParticle)
        {
            bloodParticle.gameObject.SetActive(true);

            bloodParticlePosition.x += Random.Range(0f, 0.001f);
            bloodParticlePosition.y += Random.Range(0f, 0.001f);
            bloodParticle.gameObject.transform.position = bloodParticlePosition;
            bloodParticlePosition.Set(0, 0, 0);
        }

        private void ReleaseBloodParticle(BloodParticle bloodParticle)
        {
            bloodParticle.gameObject.SetActive(false);
        }

        private void DestroyBloodParticle(BloodParticle bloodParticle)
        {
            Destroy(bloodParticle.gameObject);
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                for (int i = 0; i < bloodParticlePoolMaxSize; i++)
                {
                    bloodParticlePool.Release(CreateNewBloodParticle());
                }

                StartCoroutine(StartBeating());
            }
        }

        IEnumerator StartBeating()
        {
            for (int i = 0; i < bloodParticlePoolMaxSize; i++)
            {
                bloodParticlePool.Get();
                yield return null;
            }
        }
    }
}