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
            if (currEnd._typeCell == TypeCell.delete)
            {
                DeleteItemCell(currStart, inventory, vec, rectStart);
                return;
            }
            else if (currEnd._typeCell == TypeCell.cell && indexStart != indexEnd)
            {
                ExchangeCell(inventory, indexStart, indexEnd, rectStart, vec);
            }
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
