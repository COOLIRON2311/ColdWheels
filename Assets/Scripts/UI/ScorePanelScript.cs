using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePanelScript : MonoBehaviour
{
    [SerializeField]
    PlayerScoreScript playerScorePrefab;
    [SerializeField]
    Color textColor = Color.white;

    List<PlayerScoreScript> scores;

    void Awake()
    {
        scores = new List<PlayerScoreScript>();
    }

    public void UpdatePlayerScores(List<PlayerInfo> playersInfos)
    {
        if(scores.Count < playersInfos.Count)
        {
            var countToInstantiate = playersInfos.Count - scores.Count;
            for(int i = 0; i < countToInstantiate; i++)
            {
                scores.Add(Instantiate(playerScorePrefab, transform));
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
