using UnityEngine;

public class MovementUtility
{
    public static Vector3 GetMoveDirection(GameObject self, Camera relativeToCamera, float turnSmoothTime, ref float turnSmoothVelocity) {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;
        if(direction.magnitude > 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + relativeToCamera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(self.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            self.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        } else {
            return Vector3.zero;
        }
    }
}
