using UnityEngine;
using System.Collections;

public class MusicMachine : Machine {

    private ElectricGuitar guitar;
    float[] stateMusic = new float[] {
        2, 5, 2, 3, 20, 20, 20, 10
        //装甲,アクセル,最高速度,グリップ,エンジンHP,右HP,左HP,ユニットHP
    };

    protected override void Start()
    {
        base.Start();

        setMachineState(stateMusic);

        guitar = this.GetComponent<ElectricGuitar>();
    }

    protected override void Update()
    {
        base.Update();
        if (useability)
        {
            guitar.Electric();
        }
    }
}