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

        private void Start()
        {
            _cam = Camera.main;

            _drawingPen.endDrawingAction = null;
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
    }
}