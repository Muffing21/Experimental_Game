using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    bool alreadyHitPlayer = false;
    bool obstacleDestroyed = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < GameManager.Instance.VerticalLowerBound){
            GameManager.Instance.ReturnObstacles(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(!alreadyHitPlayer && other.tag == "Player"){
            other.GetComponent<IObstacleHittable>().OnObstacleHit();
            alreadyHitPlayer = true;
        }    
    }

    public void NotifyObstacleHit(){
        // kill player and restart game (player has only 1 health), Destroy or return Obstacle to pool, maybe increase counter of obstacles destroyed by player if color matches? 
        if(obstacleDestroyed){
            return;
        }
        //if color matches.... destroy block, increase counter, continue
            obstacleDestroyed = true;
        //else
            //player dies
    }

    #region Behaviour Sequence and Actions
    void ObstacleColorChanger(){

    }

    //create some kind of croroutine for color changing sequence

    #endregion
}
