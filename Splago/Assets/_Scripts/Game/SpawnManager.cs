using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : SingletonMono<SpawnManager>
{
    [SerializeField]
    private int numberPlayer = 2;

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
        playerManagers.Clear();

        if (numberPlayer > GameLoop.maxPlayer)
            numberPlayer = GameLoop.maxPlayer;

        for (int i = 0; i < numberPlayer; i++)
        {
            GameObject player = Instantiate(playerPrefabs, parentPlayer);
            PlayerManager playerScript = player.GetComponent<PlayerManager>();

            playerScript.Init(i, (SpellType)i, 2);
            playerManagers.Add(playerScript);
        }
    }

    public List<PlayerManager> Spawn()
    {
        return (playerManagers);
    }
}
