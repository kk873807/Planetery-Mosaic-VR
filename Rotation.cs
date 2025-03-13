using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Sun;
    public float speed = 5;
    void Start()
    {
        Sun = GameObject.FindGameObjectWithTag("Sun").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Sun.position,Vector3.up, speed * Time.deltaTime);
    }
}
