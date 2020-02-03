using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("In meters per second")]
    [SerializeField] float xSpeed = 10f;
    [SerializeField] float xRotateSpeed = 10f;
    [SerializeField] float xScreenClamp = 5f;
    [SerializeField] float xRotateClamp = 40f;
    [SerializeField] float ySpeed = 10f;
    [SerializeField] float yRotateSpeed = 10f;
    [SerializeField] float yScreenClamp = 3.5f;
    [SerializeField] float yRotateClamp = 40f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        XMovement();
        YMovement();
    }

    private void XMovement()
    {
        float horizontalThrow = Input.GetAxis("Horizontal");
        float xOffsetThisFrame = horizontalThrow * xSpeed * Time.deltaTime;
        float rawNewXPos = transform.localPosition.x + xOffsetThisFrame;

        float xPos = Mathf.Clamp(rawNewXPos, -xScreenClamp, xScreenClamp);
        transform.localPosition = new Vector3(xPos, transform.localPosition.y, transform.localPosition.z);
        XRotation(horizontalThrow);
    }

    private void XRotation(float horizontalThrow)
    {
        float rawRotatePos = horizontalThrow * xRotateSpeed * Time.deltaTime;
        float rotateClamp = Mathf.Clamp(rawRotatePos, -xRotateClamp, xRotateClamp);
        transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y, -rotateClamp, transform.localRotation.w);
    }

    private void YMovement()
    {
        float VerticalThrow = Input.GetAxis("Vertical");
        float yOffsetThisFrame = VerticalThrow * ySpeed * Time.deltaTime;
        float rawNewYPos = transform.localPosition.y + yOffsetThisFrame;

        float yPos = Mathf.Clamp(rawNewYPos, -yScreenClamp, yScreenClamp);
        transform.localPosition = new Vector3(transform.localPosition.x, yPos, transform.localPosition.z);
        YRotation(VerticalThrow);
    }

    private void YRotation(float verticalThrow)
    {
        float rawRotatePos = verticalThrow * yRotateSpeed * Time.deltaTime;
        float rotateClamp = Mathf.Clamp(rawRotatePos, -yRotateClamp, yRotateClamp);
        transform.localRotation = new Quaternion(-rotateClamp, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
    }
}
