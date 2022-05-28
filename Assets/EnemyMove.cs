using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Transform target;
    public float speed = 2f;

    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        transform.LookAt(target.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);
        if (Vector3.Distance(transform.position, target.position) > 0)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
    }
}
