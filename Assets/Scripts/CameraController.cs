using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool movable = true;
    public float edgeBorderThinkness = 10f;
    public float speed = 30f;
    public float scrollSpeed = 6f;
    public float minScroll = 20f;
    public float maxScroll = 80f;

    private void Update()
    {
        if (GameManager.gameIsOver)
        {
            this.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            movable = !movable;
        }

        if (!movable)
        {
            return;
        }

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - edgeBorderThinkness)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= edgeBorderThinkness)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - edgeBorderThinkness)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= edgeBorderThinkness)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }

        var scroll = Input.GetAxis("Mouse ScrollWheel");

        var currentPosition = transform.position;

        currentPosition.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        currentPosition.y = Mathf.Clamp(currentPosition.y, minScroll, maxScroll);

        transform.position = currentPosition;
    }
}
