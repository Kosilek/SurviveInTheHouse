using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    GridLayoutGroup g;
    RectTransform rt;
    RectTransform cRT;

    public Vector3 vec;
    private void Start()
    {
        g = GetComponent<GridLayoutGroup>();
        rt = g.GetComponent<RectTransform>();
        cRT = rt.GetChild(0) as RectTransform;
        vec = cRT.anchoredPosition;
    }
}
