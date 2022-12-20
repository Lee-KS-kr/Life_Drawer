using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Katniss
{
    public class BloodParticle : MonoBehaviour
    {
        private IObjectPool<BloodParticle> bloodParticlePool;

        public void SetPool(IObjectPool<BloodParticle> pool)
        {
            bloodParticlePool = pool;
        }
    }
}