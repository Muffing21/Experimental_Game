using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    [SerializeField] SpriteRenderer player; 
    void Awake(){
        if(Instance != null && Instance != this){
            Destroy(this);
        }
        else{
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetPlayerColor(Color spriteColor){
        player.color = spriteColor;
    }

    public SpriteRenderer GetPlayerColor(){
        return player;
    }   
}
