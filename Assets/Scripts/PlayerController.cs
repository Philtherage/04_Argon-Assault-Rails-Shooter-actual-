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
    [Header("Gun Heat Settings")]
    [SerializeField] float maxGunHeat = 20f;
    [SerializeField] float timeGunHeatIncrease = .2f;
    [SerializeField] float timeGunHeatDecrease = .2f;
    [SerializeField] float gunHeatUpSpeed = 5f;
    [SerializeField] float gunCoolDownSpeed = 10f;



    float xThrow;
    float yThrow;
    float currentGunHeat = 0;

    bool isAlive = true;
    bool isFiring = false;

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            XMovement();
            YMovement();
            ProcessRotation();
            ProcessFiring();
            ProcessGunHeat();
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

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessFiring()
    {     
        if (Input.GetButton("Fire1"))
        {        
            if (bullets.Count <= 0) { Debug.LogError("NO BULLET OBJECT LINKED TO PLAYERCONTROLLER! "); }
            foreach (GameObject bullet in bullets)
            {

                isFiring = true;
                var bpsEmission = bullet.GetComponent<ParticleSystem>().emission;
                bpsEmission.enabled = true;
                if (IsGunOverheated())
                {
                    var bps = bullet.GetComponent<ParticleSystem>().emission;
                    bps.enabled = false;
                }
            }
        }
        else
        {
            foreach (GameObject bullet in bullets)
            {
                var bps = bullet.GetComponent<ParticleSystem>().emission;
                bps.enabled = false;
                isFiring = false;
            }
            
        }
    }

    private void ProcessGunHeat()
    {
        if (isFiring)
        {
            StartCoroutine(IncreaseGunHeat());
        }
        else
        {
            StartCoroutine(DecreaseGunHeat());
        }
    }

    private void OnPlayerDeath() // called by string reference
    {
        isAlive = false;
        
    }

    public int GetBulletDamage()
    {
        return bulletDamage;
    }

    private IEnumerator IncreaseGunHeat()
    {
        if(currentGunHeat < maxGunHeat)
        {
            float gunHeatFactor = gunHeatUpSpeed * Time.deltaTime;
            currentGunHeat = Mathf.Clamp(currentGunHeat, 0f, maxGunHeat);
            currentGunHeat = currentGunHeat + 1f * gunHeatFactor;
            yield return new WaitForSeconds(timeGunHeatIncrease);
        }
    }

    private IEnumerator DecreaseGunHeat()
    {
        float gunHeatFactor = gunCoolDownSpeed * Time.deltaTime;
        currentGunHeat = Mathf.Clamp(currentGunHeat, 0f, maxGunHeat);
        currentGunHeat = currentGunHeat - 1f * gunHeatFactor;
        yield return new WaitForSeconds(timeGunHeatDecrease);              
    }
    public float GetCurrentGunHeat()
    {
        return currentGunHeat;
    }

    public float GetMaxGunHeat()
    {
        return maxGunHeat;
    }

    private bool IsGunOverheated()
    {
        return currentGunHeat >= maxGunHeat;
    }
}
