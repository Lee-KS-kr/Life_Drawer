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

        [SerializeField] private int nowCount = 0;
        [SerializeField]  private int neddInclude = 0;

        private int includeLayer;
        private int dontIncludeLayer;
        
        public Action gameSuccessAction;
        public Action gameFailedAction;

        private void Start()
        {
            includeLayer = LayerMask.NameToLayer("Include");
            dontIncludeLayer = LayerMask.NameToLayer("Dont Include");
            
            _pen.endDrawingAction -= DrawPolygon;
            _pen.endDrawingAction += DrawPolygon;
        }

        private void DrawPolygon(List<Vector2> list)
        {
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

        private void CountInclude()
        {
            nowCount++;
            if(nowCount == neddInclude)
                gameSuccessAction?.Invoke();
        }

        public void SetIncludeCount(int count)
        {
            neddInclude = count;
        }
    }
}