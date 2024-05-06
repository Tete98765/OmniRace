using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACheckpoint : MonoBehaviour
{
    public abstract void RecordTime();

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerGround") ||
            other.gameObject.layer == LayerMask.NameToLayer("PlayerWater")  ||
            other.gameObject.layer == LayerMask.NameToLayer("PlayerAir"))
        {
            RecordTime();
        }
    }
}
