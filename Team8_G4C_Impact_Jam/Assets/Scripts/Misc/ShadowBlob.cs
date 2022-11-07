using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ShadowBlob : MonoBehaviour
{
    [SerializeField] private Transform _targetToFollow;

    private void Update()
    {
        this.transform.position = new Vector3(_targetToFollow.position.x, this.transform.position.y, _targetToFollow.position.z);
    }
}