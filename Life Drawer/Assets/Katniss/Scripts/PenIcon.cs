using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katniss
{
    public class PenIcon : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("aa");
                gameObject.SetActive(true);
            }
            if (Input.GetMouseButton(0))
            {
                gameObject.transform.position = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            }
            if (Input.GetMouseButtonUp(0))
            {
                gameObject.SetActive(false);
            }
        }
    }
}