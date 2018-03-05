using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird : Bird {

    /// <summary>
    /// 重写方法
    /// </summary>
    public override void ShowSkill()
    {
        base.ShowSkill();
        Vector3 speed = rg.velocity;
        speed.x *= -1;
        rg.velocity = speed;
    }
}
