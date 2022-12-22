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

        private void DrawPolygon(List<Vector2> list)
        {
            gameObject.layer = 0;
            _polygonCollider.SetPath(0, list);
        }

        private void OnTriggerStay2D(Collider2D col)
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
                BloodCount();
            }
        }

        private void BloodCount()
        {
            bloodCount++;
            if (bloodCount > 3)
                gameFailedAction?.Invoke();
        }

        private void CountInclude()
        {
            nowCount++;
            if(nowCount == needInclude)
                gameSuccessAction?.Invoke();
        }

        public void SetIncludeCount(int count)
        {
            needInclude = count;
        }
    }
}