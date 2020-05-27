
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick;
    public FixedButton jumpButton;
    public FixedTouchField touchField;
    public PlayerWeaponManagement PWM;
    public PlayerAnimationController PAC;
    public FixedButton shotButton;

    protected Rigidbody rb;
    public GameObject ledgeGrabber;
    public LedgeGrabber ledge;


    float cameraAngleSpeed = 0.2f;

    public float MovementSpeed=5f;
    public float jumpForce=1;
    public Vector3 CamPos;
    float shootperSecond;

    public bool grounded;
    float cameraAngleY;
    float cameraPosY;
    Ray ray;
    void Start()
    {
        ledge = GetComponent<LedgeGrabber>();
        rb = GetComponent<Rigidbody>();
        PWM = GetComponent<PlayerWeaponManagement>();
        PAC = GetComponent<PlayerAnimationController>();
    }


    private void FixedUpdate()
    {
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f,Screen.height/2f,0));
        grounded = Physics.Raycast(transform.position + Vector3.up * 0.05f, Vector3.down, 0.1f);

        playerMovement();
        Jump();
        wallClimb();
        shoot();
    }
    void playerMovement()
    {
        cameraAngleY += touchField.TouchDist.x * cameraAngleSpeed;
        cameraPosY =Mathf.Clamp(cameraPosY- touchField.TouchDist.y * (cameraAngleSpeed/10),0f,5f);

        var input = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        var vel = Quaternion.AngleAxis(cameraAngleY+180, Vector3.up) * input * MovementSpeed;
        if (!ledge.hanging)
        {

            rb.velocity = new Vector3(vel.x, rb.velocity.y, vel.z);
            transform.rotation = Quaternion.AngleAxis(cameraAngleY +180+ Vector3.SignedAngle(Vector3.forward, input.normalized + Vector3.forward * 0.001f, Vector3.up), Vector3.up);

        }



        Camera.main.transform.position = (transform.position +CamPos+ Quaternion.AngleAxis(cameraAngleY, Vector3.up) * new Vector3(0, cameraPosY, 4));
        Camera.main.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 3f - Camera.main.transform.position, Vector3.up);

        if ((rb.velocity.magnitude > 0&&rb.velocity.magnitude > 4 && grounded)&&(joystick.Horizontal!=0||joystick.Vertical!=0))
        {
            PAC.run();
        }
        else if (rb.velocity.magnitude >0 && rb.velocity.magnitude < 4 && grounded && (joystick.Horizontal != 0 || joystick.Vertical != 0))
        {
            PAC.walk();
        }
        else
        {
            PAC.idle();
        }
    }

    private void Jump()
    {

        Debug.DrawRay(transform.position + Vector3.up * 0.05f, Vector3.down, Color.red, 0.1f);

        if ((Input.GetKey(KeyCode.Space)||jumpButton.Pressed )&& grounded)
        {
            if (rb.velocity.magnitude == 0)
            {
                //animator.Play("jump");
                PAC.jump();

            }
            if (rb.velocity.magnitude > 0)
            {
                //animator.Play("VelJump");
                PAC.runAndJump();

            }

            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);


        }


    }

    private void shoot()
    {
        RaycastHit hitInfo;
        if (shotButton.Pressed)
        {
            PAC.shoot();
            if (Physics.Raycast(ray,out hitInfo))
            {
                if (hitInfo.collider.CompareTag("shootable"))
                {
                    Destroy(hitInfo.collider.gameObject);


                }


            }
        }
    }

    void wallClimb()
    {
        if (!grounded)
        {
            ledgeGrabber.SetActive(true);
           
        }
        else
        {
            ledgeGrabber.SetActive(false) ;
            ledge.hanging = false;
        }

        if (ledge.hanging)
        {
            PAC.hanging();
        }
    }
}
