using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
    public class Test : MonoBehaviour
    {
        private int layerMask;
        private int penLayer;
        private int bloodLayer;

        [SerializeField] private GameObject _object;
        private Vector2[] moveDirs =
        {
            new Vector2(-1, 1), new Vector2(1, 1), new Vector2(-1, -1),
            new Vector2(1, -1),                     new Vector2(0, 1),
            new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0, -1)
        };

        private void Start()
        {
            penLayer = LayerMask.NameToLayer("Pen");
            bloodLayer = LayerMask.NameToLayer("Blood Particle");
            layerMask = 1 << 12;

            FindPath();
        }

        public void FindPath()
        {
            // 8방향으로 레이를 쏴서 나와 같은 것이나 선이 없으면 새로 하나를 만든다
            float distance = 0.5f * 5;
            for (int i = 0, size = moveDirs.Length; i < size; i++)
            {
                var hit = Physics2D.Raycast((Vector2)transform.position, moveDirs[i], distance, layerMask);
                if (hit)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    continue;
                }
                else
                    Instantiate(_object, (Vector2)transform.position + moveDirs[i], Quaternion.identity);

                //if (Physics2D.Raycast((Vector2)transform.position, (Vector2)transform.position + moveDirs[i], distance, layerMask).collider != null)
                //    continue;
                //else
                //    Instantiate(gameObject, (Vector2)transform.position + moveDirs[i], Quaternion.identity);

                //var hit = Physics2D.Raycast((Vector2)transform.position, (Vector2)transform.position + moveDirs[i], distance);
                //Debug.Log(hit.collider.gameObject.name);
                //if (hit)
                //    continue;
                //else
                //    Instantiate(gameObject, (Vector2)transform.position + moveDirs[i], Quaternion.identity);

            }
        }
    }
}