using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    [SerializeField]
    int maxPlayers = 2;

    public static PlayersManager Instance { get; private set; }

    public int MaxPlayers { get => maxPlayers; }
    public int PlayersCount { get; set; }

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
}
