using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBgLoop : MonoBehaviour
{
    public float speed = 0.1f;
    private float distance; // bg moved distance
    private int firstBgIndex;
    private int lastBgIndex;
    // Start is called before the first frame update
    void Start()
    {
        distance = 0f;
        firstBgIndex = 0;
        lastBgIndex = 5;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(speed*Time.deltaTime, 0, 0);
        distance += speed * Time.deltaTime;
        if (distance > 19.16f)
        {
            Debug.Log("change");
            Debug.Log(lastBgIndex);
            Debug.Log(firstBgIndex);
            distance = 0;
            Vector3 lastBgPos = this.transform.GetChild(lastBgIndex).position;
            this.transform.GetChild(firstBgIndex).position = new Vector3(lastBgPos.x-19.16f, lastBgPos.y, lastBgPos.z);
            lastBgIndex = firstBgIndex;
            firstBgIndex += 1;
            if (firstBgIndex == 6)
            {
                firstBgIndex = 0;
            }
        }
    }

    
}
