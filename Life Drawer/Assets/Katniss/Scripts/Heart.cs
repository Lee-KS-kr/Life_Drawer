using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Katniss
{
    public class Heart : MonoBehaviour
    {
        private int bloodParticlePoolMaxSize = 300;
        private int count = 0;
        private float size;

        private BloodParticle firstBloodParticle;

        [SerializeField] BloodParticle bloodParticlePrefab;

        public IObjectPool<BloodParticle> bloodParticlePool;

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
            if (bloodParticlePool.CountInactive <= 0)
            {
                for (int i = 0; i < bloodParticlePoolMaxSize; i++)
                {
                    bloodParticlePool.Release(CreateNewBloodParticle());
                }
            }
        }

        public BloodParticle CreateNewBloodParticle()
        {
            BloodParticle bloodParticle = Instantiate(bloodParticlePrefab);
            bloodParticle.SetPool(bloodParticlePool);
            bloodParticle.transform.position = transform.position;
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

            StartCoroutine(beatingEffect());
        }

        IEnumerator duplicateBloodParticle(BloodParticle bloodParticle)
        {
            yield return null;

            var time = 0f;
            var randomTime = Random.Range(0.1f, 0.3f);

            for (int i = 0; i < 8; i++)
            {
                while (time < randomTime)
                {
                    time += Time.deltaTime;
                    yield return null;
                }

                time = 0f;

                if (bloodParticlePoolMaxSize <= count)
                    yield break;

                if (bloodParticle.CheckByRay(i))
                {
                    var nextBloodParticle = bloodParticlePool.Get();
                    nextBloodParticle.MovePos(bloodParticle.transform.position, i);
                    StartCoroutine(duplicateBloodParticle(nextBloodParticle));
                }
                yield return null;
            }
        }

        IEnumerator beatingEffect()
        {
            var effectSize = 0.15f;
            var effectTime = 0.5f;
            var duration = 0f;

            size = transform.localScale.x;

            for (var time = 0f; duration < 7f; time += Time.deltaTime)
            {
                if (time > effectTime * 1.2f)
                {
                    time = 0;
                    transform.localScale = Vector3.one * size;
                }

                else if (time < effectTime)
                {
                    transform.localScale = Vector3.one * (size + effectSize * time / effectTime);
                }
                else
                {
                    transform.localScale = Vector3.one * (size + effectSize * (1.2f * effectTime - time) / (0.2f * effectTime));
                }

                duration += Time.deltaTime;
                yield return null;
            }
        }
    }
}