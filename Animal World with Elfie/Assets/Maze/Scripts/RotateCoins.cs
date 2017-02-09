using UnityEngine;
using System.Collections;

public class RotateCoins : MonoBehaviour
{
    public float rotationSpeed = 45f;

    void Update ()
    {
        transform.Rotate(transform.up * rotationSpeed * Time.deltaTime);
    }

   
}
