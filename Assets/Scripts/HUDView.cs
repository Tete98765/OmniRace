using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using Enums;

public class HUDView : MonoBehaviour
{
    [SerializeField]
    private PlayerCarController carController;

    [SerializeField]
    private TextMeshProUGUI speed;

    [SerializeField]
    private TextMeshProUGUI carMode;

    [SerializeField]
    private TextMeshProUGUI checkpointPassed;

    [SerializeField]
    private TextMeshProUGUI checkpointCount;

    [SerializeField]
    private UITimer lapTimer;

    [SerializeField]
    private UITimer bestTimer;

    [SerializeField]
    private UITimer diffTimer;

    [SerializeField]
    private float displayDifferenceSeconds;

    [SerializeField]
    private float speedCoefficient;

    private GameLogic gameLogic = null;

    private bool infoUpdated = false;

    void Update()
    {
        if (gameLogic == null) gameLogic = GameLogic.Instance;

        if (!infoUpdated)
        {
            infoUpdated = true;
            checkpointCount.text = gameLogic.CheckpointCount.ToString();
        }

        checkpointPassed.text = gameLogic.NextCheckpoint.ToString();

        speed.text = Mathf.FloorToInt(carController.CurrentSpeed * speedCoefficient).ToString();

        carMode.color = carController.CurrentMode.GetColor();
        carMode.text = carController.CurrentMode.ToString() + " mode";

        lapTimer.SetTimer(gameLogic.CurrentTime);
        
        if (gameLogic.BestLapTime < 0) {
            bestTimer.SetTimer();
        } else
        {
            bestTimer.SetTimer(gameLogic.BestLapTime);
        }
        
        if (gameLogic.LapNumber >= 2 && gameLogic.CurrentTime < displayDifferenceSeconds)
        {
            diffTimer.SetTimer(gameLogic.DifferenceTime);
            diffTimer.gameObject.SetActive(true);
        } else
        {
            diffTimer.gameObject.SetActive(false);
        }
    }
}
