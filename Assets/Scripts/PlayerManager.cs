using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool disabled = false;

    public Transform lookAt;

    private Transform cameraTransform;
    private PlayerManager currentPlayerTarget;

    private PlayerMovement playerMovement;

    public void Initialize(Transform mainFPSCamera, PlayerManager playerTarget)
    {
        cameraTransform = mainFPSCamera;
        currentPlayerTarget = playerTarget;
    }

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        if (!disabled)
        {
            playerMovement.MoveRelativeToCamera(cameraTransform);
            playerMovement.LookAtTransform(currentPlayerTarget.lookAt);
        }
    }
}
