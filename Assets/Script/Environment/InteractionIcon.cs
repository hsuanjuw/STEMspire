using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionIcon : MonoBehaviour
{
    /// <summary>
    /// Handle InteractionIcon zoomInOut Effect
    /// </summary>
    /// 
    public bool isInteracted = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(zoomInOut());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator zoomInOut()
    {
        while (!isInteracted)
        {
            yield return new WaitForSeconds(0.3f);
            this.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            yield return new WaitForSeconds(0.3f);
            this.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            yield return new WaitForSeconds(0.3f);
            this.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            yield return new WaitForSeconds(0.3f);
            this.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            yield return new WaitForSeconds(0.3f);
            this.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            yield return new WaitForSeconds(0.3f);
            yield return new WaitForSeconds(2f);
        }
    }
}
