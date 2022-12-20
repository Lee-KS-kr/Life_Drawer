using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
    public class Pen : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private EdgeCollider2D _edgeCollider;
        [Range(0.1f, 1.5f)][SerializeField] private float penLineWidth = 0.5f;
        private int positionCount = 0;
        private int positionIndex = -1;

        private Vector3 _startInput;
        private Vector3 _endInput;
        private Vector3 _camPosZ;
        private List<Vector2> points;

        [SerializeField] private Camera _cam;

        private bool canDraw = true;
        public bool IsCovered { get; private set; } = false;

        private void Awake()
        {
            positionCount = 0;
            positionIndex = -1;

            _lineRenderer.positionCount = positionCount;
            _lineRenderer.startWidth = 1f;
            _lineRenderer.endWidth = 1f;

            points = new List<Vector2>();
        }

        private void Start()
        {
            _cam = Camera.main;
            _camPosZ = _cam.transform.position;
        }

        private void Update()
        {
            if (!canDraw) return;
            OnDrawing();
        }

        private void OnDrawing()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startInput = _cam.ScreenToWorldPoint(Input.mousePosition) - _camPosZ;
                AddPoints(_startInput);
            }
            if (Input.GetMouseButton(0))
            {
                var pos = _cam.ScreenToWorldPoint(Input.mousePosition) - _camPosZ;
                var dist = Vector3.SqrMagnitude(points[positionIndex] - (Vector2)pos);
                var want = Mathf.Pow(0.2f, 2);
                if (dist > want)
                    AddPoints(pos);
            }
            if (Input.GetMouseButtonUp(0))
            {
                _endInput = _cam.ScreenToWorldPoint(Input.mousePosition) - _camPosZ;
                AddPoints(_endInput);

                EndDrawing();
            }
        }

        private void AddPoints(Vector2 newPoint)
        {
            positionCount++;
            positionIndex++;

            _lineRenderer.positionCount = positionCount;
            _lineRenderer.SetPosition(positionIndex, newPoint);
            points.Add(newPoint);
        }

        private void EndDrawing()
        {
            canDraw = false;

            _edgeCollider.SetPoints(points);
        }
    }
}