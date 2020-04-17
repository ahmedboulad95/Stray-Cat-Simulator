using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100.0f;

    private Transform playerTransform;
    private GameObject player;
    private PlayerController playerCon;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;
        playerCon = player.GetComponent<PlayerController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Vector3 rotationVector = (playerCon.IsMoving) ? Vector3.up : playerTransform.position;

        if(playerCon.IsMoving)
        {
            playerTransform.Rotate(Vector3.up * mouseX);
        }
        else
        {
            transform.RotateAround(playerTransform.position, Vector3.up, mouseX);
        }
    }

}