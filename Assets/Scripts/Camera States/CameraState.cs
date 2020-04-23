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
    protected static Vector3 dynamicOffset_;

    public CameraState()
    {
        mainCamera_ = Camera.main;
        player_ = GameObject.FindWithTag("Player");
        offset_ = player_.transform.position - mainCamera_.transform.position;
    }

    public virtual void Rotate()
    {
        Debug.Log("Base rotate called");
        //dynamicOffset_ = player_.transform.position - mainCamera_.transform.position;
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
