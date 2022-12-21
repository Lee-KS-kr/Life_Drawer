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

        [SerializeField] private bool isInnerLine;
        [SerializeField] private bool isOuterLine;

        private int positionCount = 0;
        private int positionIndex = -1;
        private double angle;

        private Vector3 _startInput;
        private Vector3 _camPosZ;
        private List<Vector2> points;
        private List<Vector2> points2;
        private List<Vector3> meshPoints;
        private List<int> meshTrianglePoints;
        private Vector3 _newLine;
        private Mesh mesh;

        [SerializeField] private Camera _cam;
        [SerializeField] private MeshFilter meshFilter;
        public Action<List<Vector2>> endDrawingAction;

        [Header("Pen Line Settings")]
        private float penLineWidth = 1.0f;
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

            meshPoints = new List<Vector3>();
            meshTrianglePoints = new List<int>();

            _newLine = new Vector3(penLineWidth / 2, penLineWidth / 2, 0);
        }

        private void Start()
        {
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
                {
                    AddPoints(pos);
                }
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

            //Katniss~
            if (isInnerLine)
            {
                angle = Math.Atan2(newPoint.y, newPoint.x);
                newPoint.x -= (penLineWidth / 2 * (float)Math.Cos(angle));
                newPoint.y -= (penLineWidth / 2 * (float)Math.Sin(angle));
            }
            else if (isOuterLine)
            {
                angle = Math.Atan2(newPoint.y, newPoint.x);
                newPoint.x += (penLineWidth / 2 * (float)Math.Cos(angle));
                newPoint.y += (penLineWidth / 2 * (float)Math.Sin(angle));
            }
            //~Katniss

            _lineRenderer.positionCount = positionCount;
            _lineRenderer.SetPosition(positionIndex, newPoint);
            points.Add(newPoint);
            meshPoints.Add((Vector3)newPoint);

            if (positionCount > 2)
            {
                meshTrianglePoints.Add(0);
                meshTrianglePoints.Add(positionIndex - 1);
                meshTrianglePoints.Add(positionIndex);
            }

            newPoint += (Vector2)_newLine;
            points2.Add(newPoint);
        }

        private void EndDrawing()
        {
            canDraw = false;

            _edgeCollider.SetPoints(points);
            endDrawingAction?.Invoke(points);

            if (meshFilter!=null)
            {
                makeMesh();
            }
        }

        private void makeMesh()
        {
            //Katniss~
            mesh = new Mesh();
            mesh.vertices = meshPoints.ToArray();
            mesh.triangles = meshTrianglePoints.ToArray();
            meshFilter.mesh = mesh;
            //~Katniss
        }
    }
}