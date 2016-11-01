using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ITMachine : Machine {

    private ITAbility ability;
    float[] stateIT = new float[] {
        2, 3, 4, 2, 15, 15, 15, 15
        //(3固定),アクセル,最高速度,グリップ,エンジンHP,右HP,左HP,ユニットHP
    };

    protected override void Start()
    {
        base.Start();

        setMachineState(stateIT);

        ability = this.GetComponent<ITAbility>();
    }

    protected override void Update()
    {
        base.Update();
        if (useability)
        {
            ability.StateUP(nowState);
        }
        if (ability.End())
        {
            nowState["Accelerate"] -= 3;
            nowState["Speed"] -= 6;
            nowState["Grip"] -= 3;
        }
    }
}