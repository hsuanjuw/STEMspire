using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeSpinning : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, 0, 50 * Time.deltaTime);
    }
}
