using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellsId : MonoBehaviour
{
    [SerializeField]
    private Image linkImage;

    [SerializeField, ReadOnly]
    private ushort id;
    public ushort GetId() { return (id); }

    public void SetIdAndImage(ushort _id)
    {
        id = _id;
        CellData cellData = GridDatas.Instance.GetCellsByData(id);
        linkImage.sprite = cellData.sprite;
        linkImage.color = cellData.color;
    }
}
