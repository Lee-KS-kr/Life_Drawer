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
        [SerializeField] private Animator animator;

        private void Start()
        {
            penImage.enabled = false;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!penImage.enabled)
                    penImage.enabled = true;
                animator.enabled = false;
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