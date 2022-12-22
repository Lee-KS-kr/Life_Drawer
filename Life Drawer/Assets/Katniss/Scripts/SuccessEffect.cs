using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mizu;

namespace Katniss
{
    public class SuccessEffect : MonoBehaviour
    {
        private Vector3 scale;
        [SerializeField] float effectSize = 2f;
        float effectTime = 0.5f;
        float rotSpeed = 5f;

        [SerializeField] private PenInside _penInside;

        private void Start()
        {
            _penInside.gameSuccessAction -= EffectStart;
            _penInside.gameSuccessAction += EffectStart;
        }

        void EffectStart()
        {
            StartCoroutine(livingEffect());
        }

        public IEnumerator livingEffect()
        {
            scale = gameObject.transform.localScale;

            var effectScale = scale;

            for (var time = 0f; time < effectTime * 2; time += Time.deltaTime)
            {
                if (time < effectTime)
                {
                    effectScale.x = scale.x + time * effectSize / effectTime;
                    effectScale.y = scale.y - time * effectSize / effectTime;
                    transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
                }
                else
                {
                    effectScale.x = scale.x + (effectTime * 2 - time) * effectSize / effectTime;
                    effectScale.y = scale.y - (effectTime * 2 - time) * effectSize / effectTime;
                    transform.Rotate(0, 0, -1 * rotSpeed * Time.deltaTime);
                }

                gameObject.transform.localScale = effectScale;

                yield return null;
            }

            gameObject.transform.localScale = scale;
            yield return null;

            for (var time = 0f; time < effectTime * 2; time += Time.deltaTime)
            {
                if (time < effectTime)
                {
                    effectScale.x = scale.x - time * effectSize / effectTime;
                    effectScale.y = scale.y + time * effectSize / effectTime;
                    transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
                }
                else
                {
                    effectScale.x = scale.x - (effectTime * 2 - time) * effectSize / effectTime;
                    effectScale.y = scale.y + (effectTime * 2 - time) * effectSize / effectTime;
                    transform.Rotate(0, 0, -1 * rotSpeed * Time.deltaTime);
                }

                gameObject.transform.localScale = effectScale;

                yield return null;
            }

            gameObject.transform.localScale = scale;
            yield return null;

            StartCoroutine(livingEffect());
        }
    }
}