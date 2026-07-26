using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#region Serializable classes
[System.Serializable]
public class Level
{
    public EnemyWaves[] enemyWaves;
    public bool doesSpawnBoss;
}

[System.Serializable]
public class EnemyWaves 
{
    [Tooltip("time for wave generation from the moment the game started")]
    public float timeToStart;

    [Tooltip("Enemy wave's prefab")]
    public GameObject wave;
}

#endregion

public class LevelController : MonoBehaviour {

   
    public static UnityAction<int> OnEnemyCountChanged;

    //Serializable classes implements
    public Level[] levels;
    public Transform bossSpawnPosition;
    public Boss bossPrefab;
    public GameObject powerUp;
    public float timeForNewPowerup;
    public GameObject[] planets;
    public float timeBetweenPlanets;
    public float planetsSpeed;
    List<GameObject> planetsList = new List<GameObject>();

    Camera mainCamera;

    private EnemyWaves[] _enemyWaves;
    private int _currentLevel;
    private int _wavesLeftToSpawn;
    private int _enemiesAlive;

    private void OnEnable()
    {
        OnEnemyCountChanged += HandleEnemyCountChanged;
    }

    private void OnDisable()
    {
        OnEnemyCountChanged -= HandleEnemyCountChanged;
    }

    private void Start()
    {
        mainCamera = Camera.main;
        StartLevel();
    }

    private void StartLevel()
    {
        Debug.Log($"Level {_currentLevel + 1} Start!");
        _enemyWaves = levels[_currentLevel].enemyWaves;
        _wavesLeftToSpawn = _enemyWaves.Length;
        for (int i = 0; i < _enemyWaves.Length; i++)
        {
            StartCoroutine(CreateEnemyWave(_enemyWaves[i].timeToStart, _enemyWaves[i].wave));
        }
        StartCoroutine(PowerupBonusCreation());
        StartCoroutine(PlanetsCreation());
    }

    private void EndLevel()
    {

        if (levels[_currentLevel].doesSpawnBoss)
        {
            Instantiate(bossPrefab, bossSpawnPosition.position, Quaternion.identity);
            return;
        }

        _currentLevel++;
        if(_currentLevel < levels.Length)
        {
            Invoke("StartLevel", 5f);
            return;
        }

        Debug.Log("You Win");
    }

    private void HandleEnemyCountChanged(int value)
    {
        _enemiesAlive += value;
        if(_enemiesAlive == 0 && _wavesLeftToSpawn == 0)
        {
            EndLevel();
        }
    }

    //Create a new wave after a delay
    IEnumerator CreateEnemyWave(float delay, GameObject Wave) 
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);
        if (Player.instance != null)
        {
            Instantiate(Wave);
            _wavesLeftToSpawn--;
        }
            
    }

    //endless coroutine generating 'levelUp' bonuses. 
    IEnumerator PowerupBonusCreation() 
    {
        while (true) 
        {
            yield return new WaitForSeconds(timeForNewPowerup);
            Instantiate(
                powerUp,
                //Set the position for the new bonus: for X-axis - random position between the borders of 'Player's' movement; for Y-axis - right above the upper screen border 
                new Vector2(
                    Random.Range(PlayerMoving.instance.borders.minX, PlayerMoving.instance.borders.maxX), 
                    mainCamera.ViewportToWorldPoint(Vector2.up).y + powerUp.GetComponent<Renderer>().bounds.size.y / 2), 
                Quaternion.identity
                );
        }
    }

    IEnumerator PlanetsCreation()
    {
        //Create a new list copying the arrey
        for (int i = 0; i < planets.Length; i++)
        {
            planetsList.Add(planets[i]);
        }
        yield return new WaitForSeconds(10);
        while (true)
        {
            ////choose random object from the list, generate and delete it
            int randomIndex = Random.Range(0, planetsList.Count);
            GameObject newPlanet = Instantiate(planetsList[randomIndex]);
            planetsList.RemoveAt(randomIndex);
            //if the list decreased to zero, reinstall it
            if (planetsList.Count == 0)
            {
                for (int i = 0; i < planets.Length; i++)
                {
                    planetsList.Add(planets[i]);
                }
            }
            newPlanet.GetComponent<DirectMoving>().speed = planetsSpeed;

            yield return new WaitForSeconds(timeBetweenPlanets);
        }
    }
}
