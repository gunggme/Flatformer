using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;
    
    // Starting position for the parallax game object
    private Vector2 startingPosition;
    
    // Start Z value of the parallax game object
    private float startingZ;
    
    // distance that the camera has moved from the starting position of the parallax object
    private Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    private float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    // If object is in front of target, use near clip plane. If behind target, use farClipPlane
    private float clippingPlane =>
        (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    
    // The futher the object from the player, the faster the ParallaxEffect object will move. Drag it's Z value closer to the target to make it move slower.
    private float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;
    
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    void Update()
    {
        // When the target moves, move the parallax object the same distance times a multiplier
        Vector2 newPosition = startingPosition + camMoveSinceStart / parallaxFactor;

        // The X/Y position changes based on target travel speed times the parallax factor, but Z stays consistent
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
