using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
    public class Pen : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        //[SerializeField] private EdgeCollider2D _edgeCollider;
        [SerializeField] private MeshCollider _meshCollider;
        private int positionCount = 0;
        private int positionIndex = -1;

        private Vector2 _startInput;
        private Vector2 _endInput;
        private List<Vector2> points;
        private Mesh _mesh;

        private Camera _cam;

        private bool canDraw = true;
        public bool IsCovered { get; private set; } = false;

        private void Awake()
        {
            positionCount = 0;
            positionIndex = -1;

            _lineRenderer.positionCount = positionCount;
            _lineRenderer.startWidth = 0.1f;
            _lineRenderer.endWidth = 0.1f;

            _mesh = new Mesh();
            points = new List<Vector2>();
        }

        private void Start()
        {
            _cam = Camera.main;
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
                _startInput = _cam.ScreenToWorldPoint(Input.mousePosition);
                AddPoints(_startInput);
            }
            if (Input.GetMouseButton(0))
            {
                var pos = _cam.ScreenToWorldPoint(Input.mousePosition);
                var dist = Vector2.SqrMagnitude(points[positionIndex] - (Vector2)pos);
                var want = Mathf.Pow(0.2f, 2);
                if (dist > want)
                    AddPoints(pos);
            }
            if (Input.GetMouseButtonUp(0))
            {
                _endInput = _cam.ScreenToWorldPoint(Input.mousePosition);
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

            _lineRenderer.BakeMesh(_mesh, true);
            _meshCollider.sharedMesh = _mesh;
        }
    }
}