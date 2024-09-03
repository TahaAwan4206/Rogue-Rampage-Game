using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController player;
    public BoxCollider2D bounds;

    private float halfHeight, halfWidth;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;
        AudioManager.instance.PlayLevelMusic();
    }

    // Update is called once per frame
    void Update()
    {
        player = FindAnyObjectByType<PlayerController>();
        if (player != null)
        {
            transform.position = new Vector3(
                Mathf.Clamp(player.transform.position.x, bounds.bounds.min.x + halfWidth, bounds.bounds.max.x - halfWidth),
                Mathf.Clamp(player.transform.position.y, bounds.bounds.min.y + halfHeight, bounds.bounds.max.y - halfHeight),
                transform.position.z);
        }
        else
        {
            player = FindFirstObjectByType<PlayerController>();
        }
    }
}
