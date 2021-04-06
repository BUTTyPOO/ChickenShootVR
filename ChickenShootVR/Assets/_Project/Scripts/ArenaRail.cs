using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaRail : MonoBehaviour
{
    [SerializeField] Transform start;
    [SerializeField] Transform end;

    [SerializeField] GameObject chicken;
    [SerializeField] GameObject goodGuy;
    [SerializeField] GameObject goldChicken;

    float timeTillNextSpawn = 3.0f;
    public bool playing = false;

    public delegate void RailOverEventHandler();
    public event RailOverEventHandler railOverEvent;

    public void BeginSession()
    {
        if (!playing)
        {
            StartCoroutine(PlaySession());
            playing = true;
        }
    }

    IEnumerator PlaySession()
    {
        timeTillNextSpawn = 3.0f;
        yield return new WaitForSeconds(Random.Range(0.0f, 1.0f));  //Rand Offset
        while(true)
        {
            yield return new WaitForSeconds(timeTillNextSpawn);
            SpawnActor();
            timeTillNextSpawn -= 0.1f;
            if (timeTillNextSpawn <= 0.7f)
                break;
        }
        playing = false;
        railOverEvent?.Invoke();
    }

    void SpawnActor()
    {
        GameObject spawnedActor;
        if (Random.Range(0, 5) == 0)    // 1 in 5 chance
            spawnedActor = Instantiate(goodGuy, start.transform.position, Quaternion.identity);
        else if (Random.Range(0, 5) == 1)    // 1 in 5 chance
            spawnedActor = Instantiate(goldChicken, start.transform.position, Quaternion.identity);
        else
            spawnedActor = Instantiate(chicken, start.transform.position, Quaternion.identity);

        ArenaActor arenaActorScript = spawnedActor.GetComponent<ArenaActor>();
        arenaActorScript.Init(start, end);
        arenaActorScript.moveTime = Random.Range(1.3f, 5.0f);
    }
}
