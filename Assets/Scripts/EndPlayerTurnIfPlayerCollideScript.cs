using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlayerTurnIfPlayerCollideScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DirectorScript.Instance.EndPlayerTurn(collision.gameObject);
        }
    }
}
