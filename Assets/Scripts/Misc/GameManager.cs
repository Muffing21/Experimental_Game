using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject greenPrefab;
    LinkedPool<GameObject> greenObstacle; 
    public static GameManager Instance {get; private set;}
    [SerializeField] SpriteRenderer player; 
    const float xBounds = 4f;
    const float yBounds = 7f;
    public float HorizontalLowerBound { get { return -xBounds; } }
    public float HorizontalUpperBound {  get { return xBounds; } }
    public float VerticalLowerBound { get {  return -yBounds; } }
    public float VerticalUpperBound { get {  return yBounds; } }
    void Awake(){
        if(Instance != null && Instance != this){
            Destroy(this);
        }
        else{
            Instance = this;

            greenPrefab = Resources.Load<GameObject>("Prefabs/Obstacles/Greenblock");
            if(greenPrefab != null){
                print("green prefab isnt null");
            }
        }
    }

    void Start(){
        greenObstacle = new LinkedPool<GameObject>(MakeObstacles, PoolCommons.OnGetFromPool, PoolCommons.OnReleaseToPool, PoolCommons.OnPoolItemDestroy, false, 100);
        StartCoroutine(StartObstacleSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Pool Functions

    GameObject MakeObstacles(){
        return Instantiate(greenPrefab);
    }

    public GameObject GetObstacles(){
        return greenObstacle.Get();
    }

    public void ReturnObstacles(GameObject target){
        greenObstacle.Release(target);
    }

    #endregion

    #region Spawning Stuff
    
    private void SpawnObstacle(){
        greenObstacle.Get().transform.position = new Vector2(UnityEngine.Random.Range(-7f, 7f), 1);
        
    }
    IEnumerator StartObstacleSpawn(){
        //will change later when I make collision detect
        while(true){
            yield return new WaitForSeconds(2f); //this will also change to have random intervals
            SpawnObstacle();
        }
    }

    #endregion

    public void SetPlayerColor(Color spriteColor){
        player.color = spriteColor;
    }

    public SpriteRenderer GetPlayerColor(){
        return player;
    }
   
}
