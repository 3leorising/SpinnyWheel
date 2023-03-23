using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMovement : MonoBehaviour
{
    public static LevelMovement instance;
    public float moveSpeed = 0.02f;
    float destroyXCoord = -20f;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        transform.Translate(-Vector2.right * moveSpeed);

        if(gameObject.transform.position.x <= destroyXCoord)
        {
            Destroy(gameObject);
        }
    }
}
