using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Katniss
{
    public interface IState
    {
        void OnEnter(Player player);
        void OnExit();

        IEnumerator Update();
    }
}