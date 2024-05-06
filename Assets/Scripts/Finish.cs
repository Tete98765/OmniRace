using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : ACheckpoint
{
    [SerializeField]
    private Material defaultMaterial;

    [SerializeField]
    private Material markedMaterial;

    private Renderer renderer = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (renderer == null) renderer = transform.GetChild(0).GetComponent<Renderer>();

        if (GameLogic.Instance.NextCheckpoint == GameLogic.Instance.CheckpointCount) renderer.material = markedMaterial;
        else renderer.material = defaultMaterial;
    }

    public override void RecordTime()
    {
        GameLogic.Instance.EndLap();
    }
}
