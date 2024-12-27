using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTurnScript : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI nameText;
    
    void Start()
    {
        var playerCreator = PlayerCreator.Instance;
        playerCreator.OnActivePlayerChanged += () => { SetNameText(playerCreator.GetActivePlayerInfo().Name); };
    }

    public void SetNameText(string name)
    {
        nameText.text = name;
    }
}
