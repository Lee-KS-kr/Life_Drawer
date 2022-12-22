using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mizu
{
    public class FallDown : MonoBehaviour
    {
        [SerializeField] private GameObject[] _fallObjects;
        [SerializeField] private int fallObjIdx = 0;

        [SerializeField] private AnimationCurve _fallCurve;
        private float elapsedTime = 0f;
        private bool isFall;

        private void Start()
        {
            _fallObjects = new GameObject[10];
        }

        private void Update()
        {
            if (!isFall) return;
            FallObjects();
        }

        public void SetFall(GameObject obj)
        {
            _fallObjects[fallObjIdx] = obj;
            fallObjIdx++;
        }

        public void StartFall()
        {
            isFall = true;

            if (fallObjIdx == 0)
                isFall = false;
        }

        private void FallObjects()
        {
            var yPos = _fallCurve.Evaluate(elapsedTime) * 10;
            for (int i = 0; i < fallObjIdx; i++) 
            {
                var img = _fallObjects[i].GetComponent<Image>();
                img.rectTransform.position += Vector3.down * yPos;
            }

            elapsedTime += Time.deltaTime;
        }
    }
}