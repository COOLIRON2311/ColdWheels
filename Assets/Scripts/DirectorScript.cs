using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DirectorScript : MonoBehaviour
{
    public static DirectorScript Instance { get; private set; }

    [SerializeField] private List<GameObject> trackPrefabs = new();
    [SerializeField] private GameObject playerPrefab;
    public GameObject scoreboard;

    private Camera camera_;
    private SmoothFollowCamera smoothFollowCamera;
    private GameObject spawnPointGo;
    private GameObject trackGo;
    private GameObject playerGo;

    private int currentTrackIdx;
    private int currentPlayerIdx;
    private bool activeGameplay;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EndPlayerTurn();
        }
    }

    public void StartGame()
    {
        StartCoroutine(OnGameStarted());
    }

    private IEnumerator OnGameStarted()
    {
        currentTrackIdx = 0;
        SceneManager.LoadScene(1);
        yield return null;
        StartCoroutine(OnMapStarted());
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator OnMapStarted()
    {
        currentPlayerIdx = 0;
        trackGo = Instantiate(trackPrefabs[currentTrackIdx]);
        spawnPointGo = GameObject.FindWithTag("SpawnPoint");
        camera_ = Camera.main;
        smoothFollowCamera = camera_!.GetComponent<SmoothFollowCamera>();
        if (!smoothFollowCamera)
        {
            smoothFollowCamera = camera_.gameObject.AddComponent<SmoothFollowCamera>();
            smoothFollowCamera.smoothSpeed = 0.125f;
            smoothFollowCamera.cameraOffset = new Vector3(0, 4, -8);
        }
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(OnPlayerTurnStarted());
    }

    private IEnumerator OnPlayerTurnStarted()
    {
        SoundController.Instance.PlayPhonkMusic();
        playerGo = Instantiate(playerPrefab, spawnPointGo.transform.position, Quaternion.identity);
        smoothFollowCamera.target = playerGo.transform.GetChild(0);
        PlayerCreator.Instance.SetActivePlayerIndex(currentPlayerIdx);
        activeGameplay = true;
        yield break;
    }

    private IEnumerator ProcessCamera(float duration = 1.0f)
    {
        smoothFollowCamera.target = null;
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

    public void EndPlayerTurn(GameObject player = null)
    {
        if (player && player != playerGo) return;
        if (!activeGameplay) return;
        activeGameplay = false;
        StartCoroutine(OnPlayerTurnEnded());
    }

    private IEnumerator OnPlayerTurnEnded()
    {
        playerGo.GetComponent<PlayerController>().enabled = false;
        playerGo = null;
        yield return new WaitForSeconds(1.0f);
        if (currentPlayerIdx + 1 >= PlayerCreator.Instance.PlayersCount)
        {
            StartCoroutine(OnMapEnded());
        }
        else
        {
            currentPlayerIdx++;
            StartCoroutine(OnPlayerTurnStarted());
        }
    }

    private IEnumerator OnMapEnded()
    {
        spawnPointGo = null;
        camera_ = null;
        yield return new WaitForSeconds(1.0f);
        if (currentTrackIdx + 1 >= trackPrefabs.Count)
        {
            StartCoroutine(OnGameEnded());
        }
        else
        {
            currentTrackIdx++;
            Destroy(trackGo);
            yield return null;
            StartCoroutine(OnMapStarted());
        }
    }

    private IEnumerator OnGameEnded()
    {
        if (scoreboard)
        {
            scoreboard.SetActive(true);
        }
        else
        {
            print("Game Ended: quit to main menu in 4 seconds");
            yield return new WaitForSeconds(4.0f);
            SceneManager.LoadScene(0);
        }
    }

}
