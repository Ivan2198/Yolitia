using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private PlayerDeathManager deathManager;

    private void OnTriggerEnter(Collider other)
    {
        deathManager.Die();
    }
}
