using System;
using UnityEngine;

public class FPSCharacterController : MonoBehaviour
{
    public Joystick joystick;
    CharacterController controller;

    public FixedTouchField touchField;
    float xRotataion = 0f;

    [Range(1f, 20f)]
    public float sensitivity=1.0f;
    public float movementSpeed=3;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        RotationMov();
        MoveCharacter();
    }

    void RotationMov()
    {
        xRotataion -= touchField.TouchDist.y * sensitivity* Time.deltaTime;
        xRotataion = Mathf.Clamp(xRotataion, -90.0f, 40f);
        var r = Quaternion.Euler(xRotataion, 0, 0);
        Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, r, sensitivity);
        transform.Rotate(Vector3.up * (touchField.TouchDist.x * sensitivity*Time.deltaTime));
    }

    void MoveCharacter()
    {
        var input = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        Vector3 move = transform.right * input.x + transform.forward * input.z;

        controller.Move(move* movementSpeed*Time.deltaTime);

    }
}
