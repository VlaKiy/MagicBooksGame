using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameobject : MonoBehaviour
{
    public Transform lookAt;
    public Vector3 offset;

    private void Update()
    {
        var pos = lookAt.position + offset;

        if (transform.position != pos)
        {
            transform.position = new Vector3(pos.x, pos.y, 0f);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0f);
        }
    }
}
