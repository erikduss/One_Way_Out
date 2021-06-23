using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightJump : MonoBehaviour
{
    Light light;

    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        light.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
