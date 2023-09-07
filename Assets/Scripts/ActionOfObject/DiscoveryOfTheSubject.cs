using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscoveryOfTheSubject : MonoBehaviour
{
   // public Animator openChest;

    [SerializeField]
    private bool open;

    private Animator anim;

    [Foldout("anim cntr")]
    [SerializeField] private string openA = "Open";
    [Foldout("anim cntr")]
    [SerializeField] private string closeA = "Close";

    [SerializeField] Button button;
    [SerializeField] private float timeBlockButton;

    private void Start()
    {
        SetValue();
    }

    #region Start
    private void SetValue()
    {
        anim = GetComponent<Animator>();
        button = GetComponent<Button>();
    }

    public void OpenOrClose()
    {
        switch (open)
        {
            case true:
                Close();
                open = false;
                break;
            case false:
                Open();
                open = true;
                break;
        }
    }

    private void Open()
    {
        anim.SetTrigger(openA);
    }

    private void Close()
    {
        anim.SetTrigger(closeA);
    }
    #endregion
}
