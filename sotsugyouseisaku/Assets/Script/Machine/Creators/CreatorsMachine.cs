using UnityEngine;
using System.Collections;

public class CreatorsMachine : Machine
{
    private GMShot shot;
    float[] stateCreators = new float[] {
        3, 3, 3, 3, 30, 30, 30, 20
        //(3固定),アクセル,最高速度,グリップ,エンジンHP,右HP,左HP,ユニットHP
    };

    protected override void Start()
    {
        base.Start();

        setMachineState(stateCreators);

        shot = GetComponent<GMShot>();
    }

    protected override void Update()
    {
        base.Update();
        if (useability)
        {
            shot.Shot();
        }
    }
}