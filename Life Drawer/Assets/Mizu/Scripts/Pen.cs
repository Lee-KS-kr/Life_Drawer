using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
    public class Pen : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private EdgeCollider2D _edgeCollider;

        private int positionCount = 0;
        private int positionIndex = -1;

        private Vector3 _startInput;
        private Vector3 _camPosZ;
        private List<Vector2> points;
        private List<Vector2> points2;
        private Vector3 _newLine;

        [SerializeField] private Camera _cam;
        public Action<List<Vector2>> endDrawingAction;

        [Header("Pen Line Settings")]
        [Range(0.1f, 1.5f)] [SerializeField] private float penLineWidth = 0.5f;
        [SerializeField] private Material penLineMat;

        private bool canDraw = true;
        public bool IsCovered { get; private set; } = false;

        private void Awake()
        {
            positionCount = 0;
            positionIndex = -1;

            _lineRenderer.positionCount = positionCount;
            _lineRenderer.startWidth = penLineWidth;
            _lineRenderer.endWidth = penLineWidth;
            _lineRenderer.material = penLineMat;

            points = new List<Vector2>();
            points2 = new List<Vector2>();
            _newLine = new Vector3(penLineWidth / 2, penLineWidth / 2, 0);
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
                var _endInput = _cam.ScreenToWorldPoint(Input.mousePosition) - _camPosZ;
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

            newPoint += (Vector2)_newLine;
            points2.Add(newPoint);
        }

        private void EndDrawing()
        {
            canDraw = false;

            _edgeCollider.SetPoints(points);
            //var edge = gameObject.AddComponent<EdgeCollider2D>();
            //edge.SetPoints(points2);
            endDrawingAction?.Invoke(points);
        }
    }
}