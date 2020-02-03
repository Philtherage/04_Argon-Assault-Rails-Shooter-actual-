using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("In meters per second")]
    [SerializeField] float xSpeed = 10f;
    [SerializeField] float xRotateSpeed = 10f;
    [SerializeField] float ySpeed = 10f;
    [SerializeField] float yRotateSpeed = 10f;

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

        float xPos = Mathf.Clamp(rawNewXPos, -5, 5);
        transform.localPosition = new Vector3(xPos, transform.localPosition.y, transform.localPosition.z);
        XRotation(horizontalThrow);
    }

    private void XRotation(float horizontalThrow)
    {
        float rawRotatePos = horizontalThrow * xRotateSpeed * Time.deltaTime;
        float rotateClamp = Mathf.Clamp(rawRotatePos, -40, 40);
        transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y, -rotateClamp, transform.localRotation.w);
    }

    private void YMovement()
    {
        float VerticalThrow = Input.GetAxis("Vertical");
        float yOffsetThisFrame = VerticalThrow * ySpeed * Time.deltaTime;
        float rawNewYPos = transform.localPosition.y + yOffsetThisFrame;

        float yPos = Mathf.Clamp(rawNewYPos, -3.5f, 3.5f);
        transform.localPosition = new Vector3(transform.localPosition.x, yPos, transform.localPosition.z);
        YRotation(VerticalThrow);
    }

    private void YRotation(float verticalThrow)
    {
        float rawRotatePos = verticalThrow * xRotateSpeed * Time.deltaTime;
        float rotateClamp = Mathf.Clamp(rawRotatePos, -40, 40);
        transform.localRotation = new Quaternion(-rotateClamp, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
    }
}
