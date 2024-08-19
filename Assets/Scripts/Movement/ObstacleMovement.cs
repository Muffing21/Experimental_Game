using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 2f;
    private Vector3 moveDirection = Vector3.down;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);

        if(transform.position.y < GameManager.Instance.VerticalLowerBound){
            GameManager.Instance.ReturnObstacles(gameObject);
        }
    }

    public void ObstacleMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }
}
