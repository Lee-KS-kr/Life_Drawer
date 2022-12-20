using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Katniss
{
    public class Player : MonoBehaviour
    {
        private IState currentState;

        private void Start()
        {
            SetState(new Ready());
        }

        public void SetState(IState nextState)
        {
            if (currentState != null)
            {
                currentState.OnExit();
            }

            currentState = nextState;
            currentState.OnEnter(this);
            StartCoroutine(currentState.Update());
        }
    }
}