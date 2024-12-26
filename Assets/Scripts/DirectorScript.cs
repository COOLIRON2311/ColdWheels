using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DirectorScript : MonoBehaviour
{
    public static DirectorScript Instance { get; private set; }

    [SerializeField] private List<string> maps = new();
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject scoreboardPrefab;

    private Camera camera_;
    private GameObject spawnPointGo;
    
    private int currentMapIdx;
    private int currentPlayerIdx;
    private bool activeGameplay;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void StartGame()
    {
        StartCoroutine(OnGameStarted());
    }
    
    private IEnumerator OnGameStarted()
    {
        currentMapIdx = 0;
        SceneManager.LoadScene(maps[currentMapIdx]);
        yield return null;
        StartCoroutine(OnMapStarted());
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator OnMapStarted()
    {
        print(SceneManager.GetActiveScene().name);
        currentPlayerIdx = 0;
        spawnPointGo = GameObject.FindWithTag("SpawnPoint");
        camera_ = Camera.main;
        StartCoroutine(OnPlayerTurnStarted());
        yield break;
    }

    private IEnumerator OnPlayerTurnStarted()
    {
        Instantiate(playerPrefab, spawnPointGo.transform.position, Quaternion.identity);
        activeGameplay = true;
        yield break;
    }

    private IEnumerator ProcessCamera(float duration = 1.0f)
    {
        var startPosition = camera_.transform.position;
        var endPosition = spawnPointGo.transform.position;
        var elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            camera_.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        camera_.transform.position = endPosition;
        StartCoroutine(OnPlayerTurnStarted());
    }

    public void EndPlayerTurn()
    {
        if (!activeGameplay) return;
        StartCoroutine(OnPlayerTurnEnded());
        activeGameplay = false;
    }

    private IEnumerator OnPlayerTurnEnded()
    {
        yield return new WaitForSeconds(1.0f);
        if (currentPlayerIdx + 1 >= PlayerCreator.Instance.MaxPlayers)
        {
            StartCoroutine(OnMapEnded());
        }
        else
        {
            currentPlayerIdx++;
            StartCoroutine(ProcessCamera());
        }
    }

    private IEnumerator OnMapEnded()
    {
        spawnPointGo = null;
        camera_ = null;
        yield return new WaitForSeconds(1.0f);
        if (currentMapIdx + 1 >= maps.Count)
        {
            StartCoroutine(OnGameEnded());
        }
        else
        {
            currentMapIdx++;
            SceneManager.LoadScene(maps[currentMapIdx]);
            yield return null;
            StartCoroutine(OnMapStarted());
        }
    }

    private IEnumerator OnGameEnded()
    {
        if (scoreboardPrefab != null)
        {
            Instantiate(scoreboardPrefab, Vector3.zero, Quaternion.identity);
        }
        else
        {
            print("Game Ended");
        }
        yield break;
    }

}
