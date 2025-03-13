using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using System.Collections;

public class TMP : MonoBehaviour
{
    Transform selfTransform;
    public Transform[] planets;
    public int currentPlanet = 0;
    public RawImage planetVideo;

    public VideoClip[] planetVideoClips; 
    void Start()
    {
        selfTransform = transform;
    
    }
    void Update()
    {
        transform.position = Vector3.Lerp(selfTransform.position, planets[currentPlanet].position, Time.deltaTime * 10);
        if (Input.GetKeyDown(KeyCode.S))
        {
            SwitchPlanet();
        }
    }



    void SwitchPlanet()
    {


        currentPlanet = (currentPlanet + 1) % planets.Length;
        // Update camera position
        transform.position = planets[currentPlanet].position;
        // Update button text


        StartCoroutine(PlayPlanetVideo());
    }
        
    IEnumerator PlayPlanetVideo()
    {
        // Get the video clip for the current planet
        VideoClip videoClip = GetVideoClipForCurrentPlanet();
        if (videoClip != null)
        {
            // Get the VideoPlayer component attached to the RawImage
            VideoPlayer videoPlayer = planetVideo.GetComponent<VideoPlayer>();

            // Set the video clip to the VideoPlayer component
            videoPlayer.clip = videoClip;

            // Prepare the VideoPlayer
            videoPlayer.Prepare();

            // Wait until the video player is prepared
            float waitTime = 10f;
            float timer = 0f;
            while (!videoPlayer.isPrepared && timer < waitTime)
            {
                // Wait for a frame
                // This ensures that the video player is properly prepared before attempting to play
                yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds
                timer += 0.1f;
            }

            // If the video player is still not prepared after waiting, log a warning
            if (!videoPlayer.isPrepared)
            {
                Debug.LogWarning("Video player preparation timed out.");
                yield break; // Exit the coroutine
            }

            // Assign the RenderTexture output of the VideoPlayer to the RawImage's texture
            planetVideo.texture = videoPlayer.texture;

            // Play the video
            videoPlayer.Play();
        }
    }

    // Method to get the video clip for the current planet
    VideoClip GetVideoClipForCurrentPlanet()
    {
        // Check if the array is valid and contains the video clip for the current planet
        if (planetVideoClips != null && planetVideoClips.Length > currentPlanet)
        {
            return planetVideoClips[currentPlanet];
        }
        else
        {
            // Log a warning if the array is invalid or does not contain the video clip
            Debug.LogWarning("Invalid or missing VideoClip for the current planet.");
            return null;
        }
    }
}