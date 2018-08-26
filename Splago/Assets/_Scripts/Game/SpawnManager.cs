using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : SingletonMono<SpawnManager>
{
    [SerializeField]
    private Transform parentPlayer;
    [SerializeField]
    private GameObject playerPrefabs;

    [SerializeField]
    private List<PlayerManager> playerManagers;

    public void Init1v1()
    {
        Debug.Log("init player: 1 v 1");
        parentPlayer.ClearChild();
        for (int i = 0; i < 2; i++)
        {
            GameObject player = Instantiate(playerPrefabs, parentPlayer);
            PlayerManager playerScript = player.GetComponent<PlayerManager>();
            playerScript.Init(i);
            playerManagers.Add(playerScript);
        }
    }

    public List<PlayerManager> Spawn()
    {
        return (playerManagers);
    }
}
