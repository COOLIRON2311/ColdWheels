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
        
    }

    public void SetNameText(string name)
    {
        nameText.text = name;
    }
}
