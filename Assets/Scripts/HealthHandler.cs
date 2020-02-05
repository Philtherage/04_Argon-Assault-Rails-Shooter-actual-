using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    [SerializeField] int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage(int damage)
    {
        health -= damage;
    }

    public int GetHealth()
    {
        return health;
    }
}
