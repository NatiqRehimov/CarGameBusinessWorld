using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCarMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform[] positions;

    private void Update()
    {
        AIMovement();
    }
    private void AIMovement()
    {
        if (rb.position != positions[0].position|| rb.velocity == positions[1].position)
        {
            rb.MovePosition(positions[0].position);
        }
        else if (rb.position == positions[0].position || rb.position!= positions[1].position)
        {
            rb.MovePosition(positions[1].position);
        }
        else
        {
            rb.MovePosition(positions[0].position);
        }

    }
}
