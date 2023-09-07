using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : Singleton<CanvasManager>
{
    [SerializeField] private List<GameObject> panelGameUIUnActiv = new List<GameObject>();

    private void Start()
    {
        UnActivPanel();
    }

    #region Start

    private void UnActivPanel()
    {
        for (int i = 0; i < panelGameUIUnActiv.Count; i++)
        {
            panelGameUIUnActiv[i].SetActive(false);
        }
    }

    #endregion
}
