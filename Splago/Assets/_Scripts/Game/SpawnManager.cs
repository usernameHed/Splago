using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : SingletonMono<SpawnManager>
{
    public List<PlayerManager> playerManagers;

    public void Spawn()
    {
        Debug.Log("here spawn players");
    }
}
