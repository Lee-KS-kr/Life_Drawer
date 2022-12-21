using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
    public class LevelJudge : MonoBehaviour
    {
        [SerializeField] private WaterFallJudge _waterFallJudge;
        
        [SerializeField] private PenInside _penInside;
        [SerializeField] private UIManager _uiManager;

        private void Start()
        {
            _penInside.gameSuccessAction -= SetGameSuccess;
            _penInside.gameSuccessAction += SetGameSuccess;
            _penInside.gameFailedAction -= SetGameFailed;
            _penInside.gameFailedAction += SetGameFailed;
            
            _waterFallJudge.gameFailedAction -= SetGameFailed;
            _waterFallJudge.gameFailedAction += SetGameFailed;
        }

        private void SetGameSuccess()
        {
            _uiManager.SetSuccess();
        }

        private void SetGameFailed()
        {
            _uiManager.SetFailed();
        }

        public void SetPenInsideCount(int count)
        {
            _penInside.SetIncludeCount(count);
        }
    }
}