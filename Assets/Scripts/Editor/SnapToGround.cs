using UnityEditor;
using UnityEngine;

public class SnapToGround : MonoBehaviour
{
    [MenuItem("Custom/Snap To Ground %g")]
    public static void Ground() {
        foreach(Transform transform in Selection.transforms) {
            //RaycastHit hitInfo;
            var hits = Physics.RaycastAll(transform.position + Vector3.up, Vector3.down, 10.0f);
            foreach(var hit in hits) {
                if(hit.collider.gameObject == transform.gameObject)
                    continue;

                transform.position = hit.point;
                break;
                //transform.position = new Vector3(hit.point.x, hit.point.y+(transform.gameObject.transform.localScale.y/2), hit.point.z);
            }
        }
    }
}
