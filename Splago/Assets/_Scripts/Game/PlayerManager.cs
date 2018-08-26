using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TypeInfoBox("Manage an unique player")]
public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private int index;
    public int GetIndex() { return (index); }
    [SerializeField]
    private string namePlayer;
    public string GetName() { return (namePlayer); }

    [SerializeField]
    private float time = 15f;
    public float GetTime() { return (time); }

	public void Init(int _index)
    {
        index = _index;
        Debug.Log("init player data: " + index);
        namePlayer = ExtRandom.GetRandomName();
    }
}
