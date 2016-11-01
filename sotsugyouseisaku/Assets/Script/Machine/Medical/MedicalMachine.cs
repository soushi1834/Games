using UnityEngine;
using System.Collections;

public class MedicalMachine : Machine {

    MedicalAbility ability;
    float[] stateMedical = new float[] {
        4, 3, 3, 3, 40, 40, 40, 20
        //(3固定),アクセル,最高速度,グリップ,エンジンHP,右HP,左HP,ユニットHP
    };

    protected override void Start()
    {
        base.Start();

        setMachineState(stateMedical);

        ability = this.GetComponent<MedicalAbility>();
    }

    protected override void Update()
    {
        base.Update();
        if (useability)
        {
            //breakdown = ability.BreakDownOff(breakParts);
            nowState = ability.Repair(nowState, MachineState, breakParts);
        }
    }
}