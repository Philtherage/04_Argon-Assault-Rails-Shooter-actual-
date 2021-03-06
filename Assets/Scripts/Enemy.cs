﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathExplosion;
    [Tooltip("The Number of Points this enemy is worth")]
    [SerializeField] int score = 100;
    [SerializeField] Transform parent;


    BoxCollider boxCollider;
    ScoreHandler scoreHandler;
    HealthHandler health;
    PlayerController player;
    

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        health = GetComponent<HealthHandler>();
        scoreHandler = FindObjectOfType<ScoreHandler>();
        AddNoneTriggerBoxCollider();
    }

    private void AddNoneTriggerBoxCollider()
    {
        if (!gameObject.GetComponent<BoxCollider>())
        {
            boxCollider = gameObject.AddComponent<BoxCollider>();
            boxCollider.isTrigger = false;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<PlayerController>()) { return; }
        if (health.GetHealth() <= 0)
        {
            Destroy(gameObject);
            GameObject deathVFX = Instantiate(deathExplosion, transform.position, Quaternion.identity) as GameObject;
            deathVFX.transform.parent = parent;
            Destroy(deathVFX, 1.5f);           
            scoreHandler.AddToScore(score);
        }
        health.DealDamage(player.GetBulletDamage());
        
    }

}
