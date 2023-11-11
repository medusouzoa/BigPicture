using UnityEngine;

namespace Runtime.Context.Game.Scripts.Models.Bundle
{
  public class CameraController : MonoBehaviour
  {
    public Transform cameraTransform;

    public float movementSpeed;
    public float normalSpeed;
    public float fastSpeed;
    public float movementTime;
    public float rotationAmount;

    public Vector3 newPosition;

    void Start()
    {
      newPosition = transform.position;
    }

    void LateUpdate()
    {
      HandleMovementInput();
    }

    void HandleMovementInput()
    {
      if (Input.GetKey(KeyCode.LeftShift))
      {
        movementSpeed = fastSpeed;
      }
      else
      {
        movementSpeed = normalSpeed;
      }

      if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
      {
        newPosition += (transform.forward * movementSpeed);
      }

      if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
      {
        newPosition += (transform.forward * -movementSpeed);
      }

      if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
      {
        newPosition += (transform.right * movementSpeed);
      }

      if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
      {
        newPosition += (transform.right * -movementSpeed);
      }


      transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
    }
  }
}