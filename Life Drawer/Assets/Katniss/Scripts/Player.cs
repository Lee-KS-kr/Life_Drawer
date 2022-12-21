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
                Debug.Log(currentState);
                currentState.OnExit();
            }

            if(currentState == nextState) return;
            
            currentState = nextState;
            Debug.Log(currentState);
            currentState.OnEnter(this);
            StartCoroutine(currentState.Update());
        }
    }
}