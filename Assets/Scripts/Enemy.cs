using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathExplosion;


    BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        if (!gameObject.GetComponent<BoxCollider>())
        {
            boxCollider = gameObject.AddComponent<BoxCollider>();
            boxCollider.isTrigger = false;
        }      
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.GetComponent<PlayerController>()) { return; }    
        Destroy(gameObject,1f);
        deathExplosion.SetActive(true);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        boxCollider.enabled = false;
        
    }
}
