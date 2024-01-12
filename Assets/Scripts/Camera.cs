using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    [SerializeField] private Transform player;
    private float smoothSpeed = 0.125f;
    private float startFollowXPosition = 0;
    public Vector3 offset;

    private bool _isFollowing = false;

    // Update is called once per frame
    void Update()
    {
        if(!_isFollowing && player != null)
        {
            if(player.position.x >= startFollowXPosition)
            {
                _isFollowing = true;
            }
        }

        if(_isFollowing && player != null)
        {
            var desiredPosition = player.position + offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            var freezeYPos = new Vector3(smoothedPosition.x,transform.position.y, transform.position.z);
            transform.position = freezeYPos;
        }
    }
}
