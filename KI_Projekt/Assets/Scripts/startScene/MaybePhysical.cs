using System.Collections;
using UnityEngine;

public class MaybePhysical : MonoBehaviour
{
    new Rigidbody rigidbody;
    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    protected void SetPosition(Vector3 position)
    {
        if (rigidbody == null) transform.position = position;
        else rigidbody.MovePosition(position);
    }

    protected void SetRotation(Quaternion rotation)
    {
        if (rigidbody == null) transform.rotation = rotation;
        else rigidbody.MoveRotation(rotation);
    }

    protected float updateTime => rigidbody ? Time.fixedTime : Time.time;
    protected float updateDeltaTime => rigidbody ? Time.fixedDeltaTime : Time.deltaTime;
}

