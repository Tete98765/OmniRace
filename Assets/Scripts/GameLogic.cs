using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static GameLogic Instance { get; private set; }

    [SerializeField]
    private int checkpointCount;

    public float CurrentTime { get; private set; }
    public float BestLapTime { get; private set; }
    public float DifferenceTime { get; private set; }
    public int LapNumber { get; private set; }
    public int NextCheckpoint { get; private set; }
    public int CheckpointCount {
        get => checkpointCount;
        private set => checkpointCount = value;
    }

    void Start()
    {
        CurrentTime = 0;
        BestLapTime = -1.0f;
        DifferenceTime = 0;
        LapNumber = 0;
        NextCheckpoint = 0;
    }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        CurrentTime += Time.deltaTime;
    }

    public void EndLap()
    {
        if (NextCheckpoint < checkpointCount) return;   // some checkpoints haven't been passed

        NextCheckpoint = 0;
        Debug.Log("Current lap time: " + (Mathf.Ceil(100 * CurrentTime) / 100.0f) + " sec");

        DifferenceTime = (BestLapTime < 0 ? DifferenceTime : CurrentTime - BestLapTime);
        BestLapTime = (BestLapTime < 0 || CurrentTime < BestLapTime ? CurrentTime : BestLapTime);
        CurrentTime = 0;
        
        LapNumber++;
    }

    public void PassCheckpoint(int checkpointNumber)
    {
        if (checkpointNumber == NextCheckpoint)
        {
            NextCheckpoint = checkpointNumber + 1;
            Debug.Log("Checkpoint " + checkpointNumber + " passed.");
        }
        else if (checkpointNumber < NextCheckpoint)
        {
            Debug.Log("Checkpoint " + checkpointNumber + " already passed.");
        }
        else
        {
            Debug.Log("Cannot pass checkpoint " + checkpointNumber + ". You must pass checkpoint " + NextCheckpoint + " first.");
        }
    }
}
