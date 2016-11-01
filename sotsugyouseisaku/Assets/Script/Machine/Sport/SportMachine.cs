using UnityEngine;
using System.Collections;

public class SportMachine : Machine {

    private SportAbility ability;
    float[] stateSport = new float[] {
        5, 3.5f, 2, 2, 50, 50, 50, 40
        //(3固定),アクセル,最高速度,グリップ,エンジンHP,右HP,左HP,ユニットHP
    };

    protected override void Start()
    {
        base.Start();

        setMachineState(stateSport);

        ability = this.GetComponent<SportAbility>();
    }

    protected override void Update()
    {
        base.Update();
        if (useability)
        {
            ability.Punch(this.gameObject);
        }
    }
}