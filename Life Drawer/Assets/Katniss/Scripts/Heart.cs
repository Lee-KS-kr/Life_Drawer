using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Katniss
{
    public class Heart : MonoBehaviour
    {
        private int bloodParticlePoolMaxSize = 500;
        private float time = 0f;

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
            for (int i = 0; i < 10; i++)
            {
                bloodParticlePool.Release(CreateNewBloodParticle());
            }

            if (bloodParticlePool.CountInactive < 1)
            {
                bloodParticlePool.Release(CreateNewBloodParticle());
            }

            var tmp = bloodParticlePool.Get();
            tmp.transform.position.Set(0.3f, 0, 0);
            tmp.gameObject.layer = 21;
        }

        private BloodParticle CreateNewBloodParticle()
        {
            BloodParticle bloodParticle = Instantiate(bloodParticlePrefab);
            bloodParticle.SetPool(bloodParticlePool);
            DontDestroyOnLoad(bloodParticle);
            return bloodParticle;
        }

        private void GetBloodParticle(BloodParticle bloodParticle)
        {
            bloodParticle.gameObject.SetActive(true);
            //if (time < 5f)
            //    duplicateBloodParticle(bloodParticle);
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

            time += Time.deltaTime;
        }

        private void StartBeating()
        {
            firstBloodParticle = bloodParticlePool.Get();
            firstBloodParticle.gameObject.transform.position.Set(0, 0, 0);
            duplicateBloodParticle(firstBloodParticle);
        }



        private void duplicateBloodParticle(BloodParticle bloodParticle)
        {
            //if (time > 5f)
            //    return;

            for (int i = 0; i < 8; i++)
            {
                if (bloodParticle.CheckByRay(i))
                {
                    //bloodParticlePool.Get();
                    Debug.Log($"get {i}");
                }
            }
        }
    }
}