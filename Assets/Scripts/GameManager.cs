using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform mainFPSCamera;

    public PlayerManager player1;
    public PlayerManager player2;

    void Start()
    {
        player1.Initialize(mainFPSCamera, player2);
        player2.Initialize(mainFPSCamera, player1);
    }
}
