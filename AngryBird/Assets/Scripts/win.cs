using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class win : MonoBehaviour {
    /// <summary>
    /// 胜利之后的星星的显示
    /// </summary>
    public void Show()
    {
        GameManager._instance.ShowStars();
    }
}
