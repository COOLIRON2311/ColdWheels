using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreator : MonoBehaviour
{
    [SerializeField]
    int maxPlayers = 2;
    [SerializeField]
    int minPlayerNameLength = 1;
    [SerializeField]
    int maxPlayerNameLength = 10;

    public static PlayerCreator Instance { get; private set; }

    public int MaxPlayers { get => maxPlayers; }
    public int MaxPlayerNameLength { get => maxPlayerNameLength; }
    public int MinPlayerNameLength { get => minPlayerNameLength; }
    public int PlayersCount { get; set; }

    List<PlayerInfo> players = new();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void AddPlayer(string name)
    {
        players.Add(new PlayerInfo(name, 0));
    }

    public void RemovePlayer()
    {
        if (players.Count > 0)
        {
            players.RemoveAt(players.Count - 1);
        }
    }

    public void Clear()
    {
        PlayersCount = 0;
        players.Clear();
    }
}
