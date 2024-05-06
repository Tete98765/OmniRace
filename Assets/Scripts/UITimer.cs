using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class UITimer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI minutes;

    [SerializeField]
    private TextMeshProUGUI seconds;

    [SerializeField]
    private TextMeshProUGUI centiseconds;

    [SerializeField]
    private TextMeshProUGUI dividerMS;

    [SerializeField]
    private TextMeshProUGUI dividerSC;

    [SerializeField]
    private bool showSign;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTimer(float time)
    {
        float abstime = Mathf.Abs(time);
        int csecs = Mathf.FloorToInt(100 * abstime) % 100;
        int secs = Mathf.FloorToInt(abstime) % 60;
        int mins = Mathf.FloorToInt(abstime) / 60;
        string sign = "";

        if (showSign)
        {
            sign = (time > 0) ? "+" : "-";
            var color = (time > 0) ? Color.red : Color.green;
            minutes.color = color;
            seconds.color = color;
            centiseconds.color = color;
            dividerMS.color = color;
            dividerSC.color = color;
        }

        minutes.text = sign + " " + mins.ToString();
        seconds.text = secs.ToString("D2");
        centiseconds.text = csecs.ToString("D2");
    }

    public void SetTimer()
    {
        minutes.text = "-";
        seconds.text = "--";
        centiseconds.text = "--";
    }
}
