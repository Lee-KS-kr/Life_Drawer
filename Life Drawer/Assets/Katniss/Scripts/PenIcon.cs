using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Katniss
{
    public class PenIcon : MonoBehaviour
    {
        private Vector3 pos;
        [SerializeField] private Image penImage;

        private void Start()
        {
            penImage.enabled = false;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                penImage.enabled = true;
            }
            if (Input.GetMouseButton(0))
            {
                pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z -= Camera.main.transform.position.z;
                gameObject.transform.position = pos;
            }
            if (Input.GetMouseButtonUp(0))
            {
                penImage.enabled = false;
            }
        }
    }
}