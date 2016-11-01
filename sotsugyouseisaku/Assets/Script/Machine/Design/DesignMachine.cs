using UnityEngine;
using System.Collections;

public class DesignMachine : Machine
{
    private FigureQuake figureQuake;
    float[] stateDesign = new float[] {
        2, 2, 3, 5, 20, 20, 20, 10
    };

    protected override void Start()
    {
        base.Start();

        setMachineState(stateDesign);

        figureQuake = this.GetComponent<FigureQuake>();
    }

    protected override void Update()
    {
        base.Update();
        if (useability)
        {
            figureQuake.Figure();
        }
    }
}