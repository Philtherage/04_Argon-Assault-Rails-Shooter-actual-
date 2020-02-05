using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("In meters per second")]
    [SerializeField] float xSpeed = 10f;
    [SerializeField] float xScreenClamp = 5f;
    [SerializeField] float ySpeed = 10f;
    [SerializeField] float yScreenClamp = 3.5f;
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float PositionRollFactor = -20f;
    [SerializeField] float controlYawFactor = 5f;
    [Header("Damage System")]
    [SerializeField] List<GameObject> bullets;
    [SerializeField] int bulletDamage = 25;
 
    float xThrow;
    float yThrow;

    bool isAlive = true;

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            XMovement();
            YMovement();
            ProcessRotation();
            ProcessFiring();
        }
    }

    private void XMovement()
    {
        xThrow = Input.GetAxis("Horizontal2");
        float xOffsetThisFrame = xThrow * xSpeed * Time.deltaTime;
        float rawNewXPos = transform.localPosition.x + xOffsetThisFrame;

        float xPos = Mathf.Clamp(rawNewXPos, -xScreenClamp, xScreenClamp);
        transform.localPosition = new Vector3(xPos, transform.localPosition.y, transform.localPosition.z);
    }
    private void YMovement()
    {
        yThrow = Input.GetAxis("Vertical2");
        float yOffsetThisFrame = yThrow * ySpeed * Time.deltaTime;
        float rawNewYPos = transform.localPosition.y + yOffsetThisFrame;
        float yPos = Mathf.Clamp(rawNewYPos, -yScreenClamp, yScreenClamp);
        transform.localPosition = new Vector3(transform.localPosition.x, yPos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {

        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;

        float yaw = transform.localPosition.x * controlYawFactor;

        float roll = xThrow * PositionRollFactor;

        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }

    private void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            if(bullets.Count <= 0) { Debug.LogError("NO BULLET OBJECT LINKED TO PLAYERCONTROLLER! "); }
            foreach(GameObject bullet in bullets)
            {
                bullet.SetActive(true);
                if (Input.GetButtonUp("Fire1"))
                {
                    bullet.SetActive(false);
                }
            }
        }
        else
        {
            foreach (GameObject bullet in bullets)
            {
                bullet.SetActive(false);
            }
        }
        
    }

    private void OnPlayerDeath() // called by string reference
    {
        Debug.Log("Controls Frozen");
        isAlive = false;
        
    }

    public int GetBulletDamage()
    {
        return bulletDamage;
    }
}
