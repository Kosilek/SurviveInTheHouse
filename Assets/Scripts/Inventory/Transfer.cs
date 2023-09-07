using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Transfer
{
    public void StartTranfser(Transform startCell, int cellCount, float distance, int indexStart, List<GameObject> cellInventory)
    {
        int indexEnd = -1;

        for (int i = 0; i < cellCount; i++)
        {
            if (Vector3.Distance(startCell.position, Inventory.Instance.Get_GetComponentCell(i).transform.position) <= distance)
            {
                if (indexStart != i)
                {
                    indexEnd = i;
                    Debug.Log(indexEnd);
                }
            }
        }

        SwapCells(indexStart, indexEnd, cellInventory);
    }

    private void SwapCells(int indexStart, int indexEnd, List<GameObject> cellInventory)
    {
        Vector2 vec = Inventory.Instance.Get_CellPosition(indexStart);
        Inventory inventory = Inventory.Instance;

        GameObject cellStart = inventory.Get_GetComponentCell(indexStart);
        RectTransform rectStart = cellStart.GetComponent<RectTransform>();
        CurrentItem currStart = cellStart.GetComponent<CurrentItem>();

        GameObject cellEnd;
        CurrentItem currEnd = new CurrentItem();
        if (indexEnd >= 0)
        {
            cellEnd = inventory.Get_GetComponentCell(indexEnd);
            currEnd = cellEnd.GetComponent<CurrentItem>();
        }

        if (indexEnd == -1)
        {
            ReturnCell(rectStart, vec);
            return;
        }
        else if (indexEnd >= 0)
        {
            Item itemStart = inventory.Get_ComponentItem(indexStart);
            switch (currEnd._typeCell)
            {
                case TypeCell.delete:
                    DeleteItemCell(currStart, inventory, vec, rectStart);
                    break;

                case TypeCell.cell:
                    if (indexStart != indexEnd)
                        ExchangeCell(inventory, indexStart, indexEnd, rectStart, vec);
                    break;

                case TypeCell.weapon:
                    UpdateItemPlayer(inventory, indexStart, indexEnd, rectStart, vec, TypeItem.weapon, itemStart);
                    break;

                case TypeCell.ammo:
                    UpdateItemPlayer(inventory, indexStart, indexEnd, rectStart, vec, TypeItem.ammo, itemStart);
                    break;

                case TypeCell.helmet:
                    UpdateItemPlayer(inventory, indexStart, indexEnd, rectStart, vec, TypeItem.helmet, itemStart);
                    break;

                case TypeCell.bib:
                    UpdateItemPlayer(inventory, indexStart, indexEnd, rectStart, vec, TypeItem.bib, itemStart);
                    break;

                case TypeCell.trousers:
                    UpdateItemPlayer(inventory, indexStart, indexEnd, rectStart, vec, TypeItem.trousers, itemStart);
                    break;

                case TypeCell.boots:
                    UpdateItemPlayer(inventory, indexStart, indexEnd, rectStart, vec, TypeItem.boots, itemStart);
                    break;
            }
        /*    if (currEnd._typeCell != TypeCell.delete && currEnd._typeCell != TypeCell.delete)
            {
                Item itemStart = inventory.Get_ComponentItem(indexStart);
                switch (currStart._typeCell)
                {
                    case TypeCell.weapon:
                        UpdateItemPlayer(inventory, indexStart, indexEnd, rectStart, vec, TypeItem.weapon, itemStart);
                        break;
                    case TypeCell.ammo:
                        UpdateItemPlayer(inventory, indexStart, indexEnd, rectStart, vec, TypeItem.ammo, itemStart);
                        break;
                    case TypeCell.helmet:
                        UpdateItemPlayer(inventory, indexStart, indexEnd, rectStart, vec, TypeItem.helmet, itemStart);
                        break;
                    case TypeCell.bib:
                        UpdateItemPlayer(inventory, indexStart, indexEnd, rectStart, vec, TypeItem.bib, itemStart);
                        break;
                    case TypeCell.trousers:
                        UpdateItemPlayer(inventory, indexStart, indexEnd, rectStart, vec, TypeItem.trousers, itemStart);
                        break;
                    case TypeCell.boots:
                        UpdateItemPlayer(inventory, indexStart, indexEnd, rectStart, vec, TypeItem.boots, itemStart);
                        break;
                }
            }
            else
            {
                switch (currEnd._typeCell)
                {
                    case TypeCell.delete:
                        DeleteItemCell(currStart, inventory, vec, rectStart);
                        break;
                    case TypeCell.cell:
                        if (indexStart != indexEnd)
                            ExchangeCell(inventory, indexStart, indexEnd, rectStart, vec);
                        break;
                }
            }*/
        }
    }

    private void UpdateItemPlayer(Inventory inventory, int indexStart, int indexEnd, RectTransform rectStart, Vector2 vec, TypeItem typeItem, Item itemStart)
    {
        if (itemStart._itemType == typeItem)
        {
            ExchangeCell(inventory, indexStart, indexEnd, rectStart, vec);
        }
        else if (itemStart._itemType != typeItem)
        {
            ReturnCell(rectStart, vec);
        }
    }
    private void ReturnCell(RectTransform rectStart, Vector2 vec)
    {
        rectStart.anchoredPosition = new Vector2(vec.x, vec.y);   
    }

    private void DeleteItemCell(CurrentItem currStart, Inventory inventory, Vector2 vec, RectTransform rectStart)
    {
        currStart.DeleteItemInventory();
        UpdateDisplayInventory(inventory, rectStart, vec);
    }

    private void ExchangeCell(Inventory inventory, int indexStart, int indexEnd, RectTransform rectStart, Vector2 vec)
    {
        inventory.ValueExchange(indexStart, indexEnd);
        UpdateDisplayInventory(inventory, rectStart, vec);
    }

    private void UpdateDisplayInventory(Inventory inventory, RectTransform rectStart, Vector2 vec)
    {
        inventory.DisplayItem();
        ReturnCell(rectStart, vec);
    }
}
