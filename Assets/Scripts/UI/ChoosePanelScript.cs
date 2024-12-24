using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoosePanelScript : MonoBehaviour
{
    [SerializeField]
    PlayersNumberScript playerNumberScript;
    [SerializeField]
    GameObject playerNameWindowPrefab;

    List<PlayerNameScript> playerNameWindows = new List<PlayerNameScript>();

    private void Start()
    {
        var playerCreator = PlayerCreator.Instance;


        playerNumberScript.OnApplyButtonClick += () => {
            playerNameWindows.Capacity = playerCreator.PlayersCount;
            for(int i = 0; i < playerCreator.PlayersCount; i++)
            {
                playerNameWindows.Add(CreatePlayerNameWindow(i));
            }
        };

        playerNumberScript.OnBackButtonClick += () => gameObject.SetActive(false);
    }

    public PlayerNameScript CreatePlayerNameWindow(int index)
    {
        var playerNameScript = Instantiate(playerNameWindowPrefab, transform).GetComponent<PlayerNameScript>();
        playerNameScript.gameObject.SetActive(false);

        if (index == playerNameWindows.Capacity - 1)
        {
            playerNameScript.OnApplyButtonClick += () =>
            {
                SoundController.Instance.StopMusic();
                SceneManager.LoadScene("Game");
            };
            playerNameScript.SetApplyButtonText("Готово");
        }
        else
        {
            playerNameScript.OnApplyButtonClick += () => SetActiveNameWindowAt(playerNameScript, index + 1);
        }

        if (index == 0)
        {
            playerNameScript.SetActive();
            playerNameScript.OnBackButtonClick += () =>
            {
                ClearNameWindows();
            };
        }
        else
        {
            playerNameScript.OnBackButtonClick += () => SetActiveNameWindowAt(playerNameScript, index - 1);
        }

        return playerNameScript;
    }

    private void SetActiveNameWindowAt(PlayerNameScript currentActive, int index)
    {
        currentActive.gameObject.SetActive(false);
        playerNameWindows[index].gameObject.SetActive(true);
    }

    // В тупую, устал
    private void ClearNameWindows()
    {
        foreach(var playerNameScript in playerNameWindows)
        {
            Destroy(playerNameScript.gameObject);
        }
        playerNameWindows.Clear();
    }

    public void Clear()
    {
        playerNumberScript.Clear();
    }
}
