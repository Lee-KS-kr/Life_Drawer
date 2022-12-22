using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mizu
{
    public class LevelJudge : MonoBehaviour
    {
        [SerializeField] private PenInside _penInside;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private FallDown _fallDown;

        private GameObject[] _thisLevelInclude;
        [SerializeField] private List<GameObject> _foundInclude = new List<GameObject>();

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _penInside.gameSuccessAction -= SetGameSuccess;
            _penInside.gameSuccessAction += SetGameSuccess;
            _penInside.gameFailedAction -= SetGameFailed;
            _penInside.gameFailedAction += SetGameFailed;
            _penInside.foundObjectAction -= GetObjects;
            _penInside.foundObjectAction += GetObjects;

            _uiManager.LevelsUI.includeCountAction -= SetPenInsideCount;
            _uiManager.LevelsUI.includeCountAction += SetPenInsideCount;
        }

        private void SetGameSuccess()
        {
            Debug.Log("su");
            _uiManager.SetSuccess();
        }

        private void SetGameFailed()
        {
            Debug.Log("fa");
            CompareObjects();
            _uiManager.SetFailed();
        }

        private void GetObjects(List<GameObject> list)
        {
            _foundInclude = list;
        }

        private void CompareObjects()
        {
            foreach(var obj in _thisLevelInclude)
            {
                if (_foundInclude.Contains(obj) == false)
                    _fallDown.SetFall(obj);
            }

            _fallDown.StartFall();
        }

        public void SetPenInsideCount(int count)
        {
            _thisLevelInclude = GameObject.FindGameObjectsWithTag("Include");

            _penInside.SetIncludeCount(count);
        }
    }
}