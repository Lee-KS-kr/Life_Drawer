using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Katniss
{
    public class Heart : MonoBehaviour
    {
        private int bloodParticlePoolMaxSize = 10;
        private int count = 0;

        private BloodParticle firstBloodParticle;

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

        private void Start()
        {
            for (int i = 0; i < bloodParticlePoolMaxSize; i++)
            {
                bloodParticlePool.Release(CreateNewBloodParticle());
            }
        }

        private BloodParticle CreateNewBloodParticle()
        {
            BloodParticle bloodParticle = Instantiate(bloodParticlePrefab);
            bloodParticle.SetPool(bloodParticlePool);
            //DontDestroyOnLoad(bloodParticle);
            return bloodParticle;
        }

        private void GetBloodParticle(BloodParticle bloodParticle)
        {
            if (bloodParticle == null)
                return;

            count++;
            bloodParticle.gameObject.SetActive(true);
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
                StartBeating();
            }
        }

        private void StartBeating()
        {
            firstBloodParticle = bloodParticlePool.Get();
            StartCoroutine(duplicateBloodParticle(firstBloodParticle));
        }

        IEnumerator duplicateBloodParticle(BloodParticle bloodParticle)
        {
            yield return null;

            for (int i = 0; i < 8; i++)
            {
                if (bloodParticlePoolMaxSize <= count)
                    yield break;

                if (bloodParticle.CheckByRay(i))
                {
                    var nextBloodParticle = bloodParticlePool.Get();
                    nextBloodParticle.movePos(bloodParticle.transform.position, i);
                    StartCoroutine(duplicateBloodParticle(nextBloodParticle));
                }
                yield return null;
            }
        }
    }
}