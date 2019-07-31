using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Velocidad con la que rota la camara
    [SerializeField]
    private float turnSpeed = 5000.0f;
    // Velocidad con la que se panea la camara
    [SerializeField]
    private float panSpeed = 1500.0f;
    // Velocidad con la que se hace zoom
    [SerializeField]
    private float zoomSpeed = 500.0f;

    // Posicion y rotacion inicial de la camara
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private Vector3 lastMousePosition;
    private bool isPanning;
    private bool isRotating;

    // Velocidad con la que se mueve la camara con el teclado
    [SerializeField]
    private float moveSpeed = 10.0f;

    //Límites de habitación
    private MovementLimit movementLimits = new MovementLimit(5,-5,-15,25,20,-20);

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        ManageMouseInput();

        if (isRotating)
        {
            RotateCamera();
        }

        if (isPanning)
        {
            PanCamera();
        }

        ManageKeyboardInput();
        lastMousePosition = Input.mousePosition;
    }

    /**
     * Mueve la camara segun WASD
     */
    private void ManageKeyboardInput()
    {
        // Keyboard commands
        Vector3 moveDirection = GetDirectionInput();
        moveDirection = moveDirection * moveSpeed * Time.deltaTime;
        transform.Translate(moveDirection);
        if (!LimitsOk(transform.position))
        {
            transform.Translate(-moveDirection);
        }
    }

    /**
     * Devuelve la direccion hacia donde hay que mover la camara segun WASD
     */
    private Vector3 GetDirectionInput()
    {
        Vector3 moveDirection = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += new Vector3(1, 0, 0);
        }

        // Resetea camara a la posicion inicial
        if (Input.GetKey(KeyCode.R))
        {
            transform.SetPositionAndRotation(initialPosition, initialRotation);
            return Vector3.zero;
        }
        return moveDirection;
    }

    /**
     * Se fija que hacer si se apretaron los botones de mouse
     */
    private void ManageMouseInput()
    {
        // Boton derecho del mouse para rotar la camara
        if (Input.GetMouseButtonDown(1))
        {
            lastMousePosition = Input.mousePosition;
            isRotating = true;
        }

        // Boton medio del mouse para panear la camara (click en rueda)
        if (Input.GetMouseButtonDown(2))
        {
            lastMousePosition = Input.mousePosition;
            isPanning = true;
        }

        // Mueve la camara en Z cuando se mueve la rueda del mouse (zoom)
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Vector3 move = zoomSpeed * transform.forward * Input.GetAxis("Mouse ScrollWheel");
            transform.Translate(move * Time.deltaTime, Space.World);
        }

        // Deja de rotar/panear al soltar el boton del mouse
        if (!Input.GetMouseButton(1)) isRotating = false;
        if (!Input.GetMouseButton(2)) isPanning = false;
    }

    /**
     * Mueve la camara en el plano XY
     */
    private void PanCamera()
    {
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - lastMousePosition);
        Vector3 move = new Vector3(-pos.x * panSpeed, -pos.y * panSpeed, 0);
        transform.Translate(move * Time.deltaTime, Space.Self);
    }

    /**
     * Rota la camara sobre su propio eje
     */
    private void RotateCamera()
    {
        if (lastMousePosition != Input.mousePosition)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - lastMousePosition) * Time.deltaTime;
            transform.RotateAround(transform.position, -transform.right, -pos.y * turnSpeed);
            transform.RotateAround(transform.position, -Vector3.up, pos.x * turnSpeed);
        }
    }

    private bool LimitsOk(Vector3 position)
    {
        if(position.y > movementLimits.Superior || position.y < movementLimits.Inferior)
        {
            return false;
        }

        if(position.x > movementLimits.Right || position.x < movementLimits.Left)
        {
            return false;
        }
        
        if(position.z > movementLimits.Front || position.z < movementLimits.Back)
        {
            return false;
        }

        return true;
    }
}
