using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    [SerializeField]
    int maxPlayers = 2;
    [SerializeField]
    int minPlayerNameLength = 1;
    [SerializeField]
    int maxPlayerNameLength = 10;

    public static PlayersManager Instance { get; private set; }

    public int MaxPlayers { get => maxPlayers; }
    public int MaxPlayerNameLength { get => maxPlayerNameLength; }
    public int MinPlayerNameLength { get => minPlayerNameLength; }
    public int PlayersCount { get; set; }

    List<Player> players = new();

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddPlayer(string name)
    {
        players.Add(new Player(name, 0));
    }

    public void RemovePlayer()
    {
        if(players.Count > 0)
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
