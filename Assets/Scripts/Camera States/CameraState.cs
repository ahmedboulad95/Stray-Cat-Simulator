using UnityEngine;

public class CameraState
{
    protected string stateName_;
    protected Camera mainCamera_;
    protected GameObject player_;
    protected float mouseSensitivity_ = 100.0f;
    protected Vector2 mousePosition_;
    protected static Vector3 offset_;
    protected float rotationSpeed_ = 1.0f;
    protected float directionalRotateSpeed_ = 1.5f;
    protected PlayerController playerController_;

    public CameraState()
    {
        mainCamera_ = Camera.main;
        player_ = GameObject.FindWithTag("Player");
        playerController_ = player_.GetComponent<PlayerController>();
        offset_ = player_.transform.position - mainCamera_.transform.position;
    }

    public virtual void Rotate()
    {
        float vertical = Input.GetAxisRaw("Vertical") * mainCamera_.transform.forward.z;
        float horizontal = Input.GetAxisRaw("Horizontal") * mainCamera_.transform.right.x;

        if (!Mathf.Approximately(horizontal, 0.0f) || !Mathf.Approximately(vertical, 0.0f))
        {
            Vector3 desiredMoveDirection = Input.GetAxisRaw("Vertical") * mainCamera_.transform.forward + Input.GetAxisRaw("Horizontal") * mainCamera_.transform.right;
            desiredMoveDirection.y = 0.0f;
            player_.transform.rotation = Quaternion.RotateTowards(player_.transform.rotation, Quaternion.LookRotation(desiredMoveDirection), Time.time * directionalRotateSpeed_);
        }
    }

    public Vector2 GetMousePosition()
    {
        return new Vector2(Input.GetAxis("Mouse X") * mouseSensitivity_ * Time.deltaTime, Input.GetAxis("Mouse Y") * mouseSensitivity_ * Time.deltaTime);
    }

    public void ReturnToStart()
    {
        mainCamera_.transform.RotateAround(player_.transform.position, Vector3.up, -1 * rotationSpeed_ * mainCamera_.transform.rotation.y);
    }
}
