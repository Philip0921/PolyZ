using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    public float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;
    [SerializeField]
    private float minAngle = -90f;
    [SerializeField]
    private float maxAngle = 90f;
    private float _xRot = 0f;

    public bool isSprinting = false;
    float sprintCooldown = 3f;
    float countdown = 2f;

    private PlayerMotor motor;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        float _MoveX = Input.GetAxisRaw("Horizontal");
        float _MoveZ = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _MoveX;
        Vector3 _movVertical = transform.forward * _MoveZ;

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;
        motor.Move(_velocity);

        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        motor.Rotate(_rotation);

        _xRot += Input.GetAxisRaw("Mouse Y" ) * lookSensitivity;
        _xRot = Mathf.Clamp(_xRot, minAngle, maxAngle);

        Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f);

        motor.RotateCamera(_cameraRotation);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (Input.GetKeyDown(KeyCode.LeftShift) && sprintCooldown <= 0f)
        {
            isSprinting = true;
            Invoke("Sprint", 0f);
        }
        if (countdown <= 0)
        {
            isSprinting = false;
            countdown = 2f;
            speed = 5f;
            sprintCooldown = 3f;
        }

    }

    public void FixedUpdate()
    {
        if (isSprinting == true)
        {
            countdown -= Time.deltaTime;
        }
        if (countdown >= 0 && isSprinting == false)
        {
            sprintCooldown -= Time.deltaTime;
            if (sprintCooldown < 0f)
            {
                sprintCooldown = 0f;
            }
        }
    }

    void Sprint()
    {
        speed = 10f;
    }
}
