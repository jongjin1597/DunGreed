using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cBilizyStartPoint : MonoBehaviour
{
    Texture2D _CursorTexture;
    Texture2D _ClickTexture;
    GameObject _EmptyItem;
    void Awake()
    {
        _CursorTexture = Resources.Load<Texture2D>("UI/BasicCursor");
        _ClickTexture = Resources.Load<Texture2D>("UI/BasicCursor");
        _EmptyItem = Resources.Load("Prefabs/Inventory/EmptyItem") as GameObject;
        Player.GetInstance.gameObject.SetActive(true);
        cCameramanager.GetInstance.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, cCameramanager.GetInstance.transform.position.z);
        cUIManager.GetInstance.gameObject.SetActive(true);
        Player.GetInstance.transform.position = this.transform.position;
        Player.GetInstance.MoveMap = false;
        cGameManager.GetInstance.SetBackGruond(BackGroundSound.Bilizy);
        cGameManager.GetInstance.SetCursor(_CursorTexture, _ClickTexture);

        Item item = _EmptyItem.GetComponent<Item>();
        for (int i = 0; i <cInventory.GetInstance._InventorySlot.Count; ++i) 
        {
            cInventory.GetInstance._InventorySlot[i]._item = item;
        }
    }
}
