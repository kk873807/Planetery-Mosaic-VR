using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSnapToPlanet : MonoBehaviour
{
    // Start is called before the first frame update
     Transform planet;
    public int currentPlanet = 0;

    public Transform[] planets;

   Transform selfTransform;
    void Start()
    {
        selfTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(selfTransform.position , planets[currentPlanet].position, Time.deltaTime*10); 
        if(Input.GetKeyDown(KeyCode.S))
            {
            SwitchPlanet();
        }
    }

    void SwitchPlanet()
    {
        currentPlanet += 1;
        if(currentPlanet == planets.Length)
        {
            currentPlanet = 0;
        }
    }
}
