using UnityEngine;
using System.Collections;

public class TechnologyMachine : Machine {

    private TechnologyAbility ability;
    float[] stateTechnology = new float[] {
        1, 5, 7, 1, 10, 10, 10, 5
        //(3固定),アクセル,最高速度,グリップ,エンジンHP,右HP,左HP,ユニットHP
    };

    protected override void Start()
    {
        base.Start();
        
        setMachineState(stateTechnology);

        ability = this.GetComponent<TechnologyAbility>();
    }

    protected override void Update()
    {
        base.Update();
        if (useability)
        {
            ability.Dash();
        }
    }
}