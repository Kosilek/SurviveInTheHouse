using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : Singleton<Inventory>
{
    [SerializeField] private List<GameObject> _cellInventory;
    private List<Item> _item = new List<Item>();
    public List<Vector2> _positionCell = new List<Vector2>();

    private void Start()
    {
        GetEnumerator();
        CreateList();
    }

    #region Start

    private void CreateList()
    {
        for (int i = 0; i < _cellInventory.Count; i++)
        {
            _cellInventory[i].GetComponent<CurrentItem>()._index = i;
            _positionCell.Add(new Vector2(_cellInventory[i].GetComponent<RectTransform>().anchoredPosition.x, _cellInventory[i].GetComponent<RectTransform>().anchoredPosition.y));
            _item.Add(new Item());
            
        }
    }

    public IEnumerator GetEnumerator()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return null;
        }
    }

    #endregion

    #region Instance Action

    public GameObject Get_GetComponentCell(int i)
    {
        return _cellInventory[i];
    }

    public int Get_CellCount()
    {
        return _cellInventory.Count;
    }

    public Vector2 Get_CellPosition(int i)
    {
        return _positionCell[i];
    }

    public List<GameObject> Get_ListItem()
    {
        return _cellInventory;
    }

    public void DisplayItem()
    {
        for (int i = 0; i < _item.Count; i++)
        {
            Transform cell = _cellInventory[i].transform;
            Transform icon = cell.GetChild(1);
            Transform count = icon.GetChild(0);
            Text txt = count.GetComponent<Text>();
            Image img = icon.GetComponent<Image>();
            if (_item[i]._id != 0)
            {
                img.enabled = true;
                img.sprite = Resources.Load<Image>(_item[i].pathIcon).sprite;
                if (_item[i]._countItem == 1)
                {
                    txt.text = null;
                }
                else if (_item[i]._countItem != 1)
                {
                    txt.text = _item[i]._countItem.ToString();
                }
            }
            else if (_item[i]._id == 0)
            {
                img.enabled = false;
                img.sprite = null;
                txt.text = null;
            }
        }
    }

    public void PickupTrigger(GameObject items, bool isStackable, int id, int itemCount)
    {
        if (isStackable)
        {
            AddStackableItem(items, id, itemCount);
        }
        else if (!isStackable)
        {
            AddUnStackableItem(items);
        }
    }
    #region PickupTrigger
    private void AddStackableItem(GameObject items, int id, int itemCount)
    {
        for (int i = 0; i < _item.Count - 9; i++)
        {
            if (_item[i]._id == id)
            {
                _item[i]._countItem += itemCount;
                if (_item[i]._dropItem)
                    Destroy(items);
                return;
            }
        }
        AddUnStackableItem(items);
    }

    private void AddUnStackableItem(GameObject items)
    {
        for (int i = 0; i < _item.Count - 9; i++)
        {
            if (_item[i]._id == 0)
            {
                _item[i] = items.GetComponent<Item>();
                if (_item[i]._dropItem)
                    Destroy(items);
                break;
            }
        }
    }
    #endregion

    public void DeleteItem(int index, GameObject icon, Text txt)
    {
        _item[index]._id = 0;
        icon.GetComponent<Image>().enabled = false;
        icon.GetComponent<Image>().sprite = null;
        txt.text = null;
    }

    public void ValueExchange(int startIndex, int endIndex)
    {
        (_item[startIndex], _item[endIndex]) = (_item[endIndex], _item[startIndex]);
    }

    #endregion
}
