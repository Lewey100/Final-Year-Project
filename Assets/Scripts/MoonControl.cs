using UnityEngine;
using System.Collections;

public class MoonControl : MonoBehaviour
{
    public float distance = 1000.0f;
    public float scale = 15.0f;
    // Use this for initialization
    void Start()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, distance);
        transform.localScale = new Vector3(scale, scale, scale); //Used to set the moon's distance, moon is never seen from camera however.
    }

    // Update is called once per frame
    void Update()
    {

    }
}
