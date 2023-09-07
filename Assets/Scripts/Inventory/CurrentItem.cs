using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class CurrentItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public TypeCell _typeCell;
    [SerializeField]

    #region CommponetnCell

    public GameObject icon;
    public Text txt;

    #endregion

    [HideInInspector]
    public int _index;

    #region TransferValues
    private Transfer transfer = new Transfer();
    [SerializeField]
    private float _distance = 100f;
    private int _indexStart;

    private int _cellCount;

    public bool _checkTranfser;

    private Inventory inventory;
    #endregion

    private void Start()
    {
        SetValues();        
    }

    #region Start
    
    private void SetValues()
    {
        inventory = Inventory.Instance;
        _cellCount = inventory.Get_CellCount();
    }

    #endregion

    #region Transfer
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_typeCell == TypeCell.cell && icon.GetComponent<Image>().enabled)
        {
           
            _checkTranfser = true;
        }
        else if (_typeCell != TypeCell.cell)
        {
            _checkTranfser = false;
        }
        _indexStart = _index;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_checkTranfser)
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_checkTranfser)
            transfer.StartTranfser(transform, _cellCount, _distance, _indexStart, Inventory.Instance.Get_ListItem());
        _checkTranfser = false;
    }
    #endregion

    public void DeleteItemInventory()
    {
        Inventory.Instance.DeleteItem(_index, icon, txt);
    }
}

public enum TypeCell
{
    cell,
    weapon,
    ammo,
    helmet,
    bib,
    trousers,
    boots,
    delete
}

