using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
    public class WaterFallJudge : MonoBehaviour
    {
        [SerializeField] private Pen _drawingPen;
        [SerializeField] private PolygonCollider2D _polygonCollider;
        [SerializeField] private Camera _cam;

        private int bloodLayer;
        private int includeLayer;

        public Action gameFailedAction;

        private void Start()
        {
            bloodLayer = LayerMask.NameToLayer("Blood Particle");
            includeLayer = LayerMask.NameToLayer("Include");

            _cam = Camera.main;

            _drawingPen.endDrawingAction -= OnEndDrawing;
            _drawingPen.endDrawingAction += OnEndDrawing;
        }

        private void OnEndDrawing(List<Vector2> points)
        {
            _polygonCollider.SetPath(0, points);

            Vector2[] gameScreen = new Vector2[4];
            gameScreen[0] = _cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
            gameScreen[1] = _cam.ScreenToWorldPoint(new Vector3(720, 0, 0));
            gameScreen[2] = _cam.ScreenToWorldPoint(new Vector3(720, 1280, 0));
            gameScreen[3] = _cam.ScreenToWorldPoint(new Vector3(0, 1280, 0));

            _polygonCollider.SetPath(1, gameScreen);
        }

        //private void OnTriggerEnter2D(Collider2D col)
        //{
        //    Debug.Log(col.gameObject.name);
        //    if (col.gameObject.layer == bloodLayer || col.gameObject.layer == includeLayer)
        //    {
        //        gameFailedAction?.Invoke();
        //        Debug.Log($"{col.gameObject.name} + GAME FAILED");
        //    }
        //}
    }
}