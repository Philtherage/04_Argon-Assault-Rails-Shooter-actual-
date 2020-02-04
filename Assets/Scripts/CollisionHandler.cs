using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] GameObject explosion;

    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
        FindObjectOfType<LevelLoader>().RestartLevel();
        if (!explosion) { Debug.LogError("NO EXPLOSION LINKED TO COLLISIONHANDLER ON PLAYERSHIP"); }
        explosion.SetActive(true);
    }

}
