using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScorePanelScript : MonoBehaviour
{
    [SerializeField]
    PlayerScoreScript playerScorePrefab;
    [SerializeField]
    GameObject scoreHolder;
    [SerializeField]
    Color textColor = Color.white;

    List<PlayerScoreScript> scores;

    void Start()
    {
        scores = new List<PlayerScoreScript>();

        var playerCreator = PlayerCreator.Instance;
        playerCreator.OnScoreChanged += () => {
            var orderedInfos = playerCreator.GetOrderedByScorePlayersInfos();
            UpdatePlayerScores(orderedInfos);
        };

        UpdatePlayerScores(playerCreator.GetOrderedByScorePlayersInfos());
    }


    public void UpdatePlayerScores(List<PlayerInfo> playersInfos)
    {
        var parentTransform = transform;
        if(scoreHolder != null)
        {
            parentTransform = scoreHolder.transform;
        }
        if (scores.Count < playersInfos.Count)
        {
            var countToInstantiate = playersInfos.Count - scores.Count;
            for(int i = 0; i < countToInstantiate; i++)
            {
                scores.Add(Instantiate(playerScorePrefab, parentTransform));
            }
        }else if(scores.Count > playersInfos.Count)
        {
            var countToDestroy = scores.Count - playersInfos.Count;
            for(int i = 0; i < countToDestroy; i++)
            {
                var indexToRemove = scores.Count - 1 - i;
                Destroy(scores[indexToRemove].gameObject);
                scores.RemoveAt(indexToRemove);
            }
        }
        for (int i = 0; i < playersInfos.Count; i++)
        {
            scores[i].SetNameAndScore(playersInfos[i].Name, playersInfos[i].Score);
            scores[i].SetTextColor(textColor);
        }
    }
}
