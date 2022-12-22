using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
    public class PenInside : MonoBehaviour
    {
        [SerializeField] private Pen _pen;
        [SerializeField] private PolygonCollider2D _polygonCollider;

        [SerializeField] private int nowCount;
        [SerializeField] private int needInclude;
        [SerializeField] private int bloodCount;

        private int includeLayer;
        private int dontIncludeLayer;
        private int bloodLayer;

        private bool isAllIncluded = false;
        private bool isBloodOut = false;
        private bool isBloodStart = false;

        private float bleedingTime = 0f;

        public Action gameSuccessAction;
        public Action gameFailedAction;

        private void Awake()
        {
            nowCount = 0;
            needInclude = 0;
        }

        private void Start()
        {
            includeLayer = LayerMask.NameToLayer("Include");
            dontIncludeLayer = LayerMask.NameToLayer("Dont Include");
            bloodLayer = LayerMask.NameToLayer("Blood Particle");
            
            _pen.endDrawingAction -= DrawPolygon;
            _pen.endDrawingAction += DrawPolygon;
        }

        private void Update()
        {
            if (!isBloodStart) return;

            bleedingTime += Time.deltaTime;
            if(bleedingTime > 3f)
            {
                if (isAllIncluded)
                    Success();
            }
        }

        private void DrawPolygon(List<Vector2> list)
        {
            isBloodStart = true;
            gameObject.layer = 0;
            _polygonCollider.SetPath(0, list);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer == includeLayer)
            {
                CountInclude();
            }

            if (col.gameObject.layer == dontIncludeLayer)
            {
                Debug.Log($"{col.gameObject.name} GAME OVER");
                gameFailedAction?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.layer == bloodLayer)
            {
                Failed();
            }
        }

        private void BloodCount()
        {
            bloodCount++;
            if (bloodCount > 1)
                Failed();
        }

        private void CountInclude()
        {
            nowCount++;
            if (nowCount == needInclude)
                isAllIncluded = true;
        }

        public void SetIncludeCount(int count)
        {
            needInclude = count;
        }

        private void Success()
        {
            if (isBloodOut) return;

            gameSuccessAction?.Invoke();
            isBloodStart = false;
        }

        private void Failed()
        {
            if (isBloodOut) return;

            isBloodOut = true;

            gameFailedAction?.Invoke();
            Physics2D.gravity = new Vector2(0, -5000f);
            Debug.Log($"{Physics2D.gravity}");
        }
    }
}