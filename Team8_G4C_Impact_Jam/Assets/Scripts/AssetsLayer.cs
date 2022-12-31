using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsLayer : MonoBehaviour
{
    public float offset;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0);
        Gizmos.DrawLine(transform.position - Vector3.right * 15f + Vector3.up * offset, transform.position + Vector3.right * 15f + Vector3.up * offset);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Player").transform.position.y > transform.position.y + offset)
            GetComponent<SpriteRenderer>().sortingOrder = 21;
        else
            GetComponent<SpriteRenderer>().sortingOrder = 0;
    }
}
