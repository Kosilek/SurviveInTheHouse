using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("MainVariables")]
    public string _nameItem;
    public int _levelItem;
    public int _id;
    public int _countItem;
    public bool _isStackable;
    public string pathIcon;
    public string pathPrefab;
    public int _cost;
    public bool _dropItem;
    public TypeItem _itemType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player_Cntr>())
        {
            Inventory.Instance.PickupTrigger(gameObject, _isStackable, _id, _countItem);
        }
    }
}
public enum TypeItem
{
    weapon,
    ammo,
    helmet,
    bib,
    trousers, 
    boots,
    other
}