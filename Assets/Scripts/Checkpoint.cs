using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : ACheckpoint
{
    [SerializeField]
    private int checkpointNumber;

    [SerializeField]
    private Material defaultMaterial;

    [SerializeField]
    private Material markedMaterial;

    private Renderer renderer = null;

    public int CheckpointNumber { get { return checkpointNumber; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (renderer == null) renderer = transform.GetChild(0).GetComponent<Renderer>();

        if (GameLogic.Instance.NextCheckpoint == CheckpointNumber) renderer.material = markedMaterial;
        else renderer.material = defaultMaterial;
    }

    public override void RecordTime()
    {
        GameLogic.Instance.PassCheckpoint(this.checkpointNumber);
    }
}
