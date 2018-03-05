using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour {

    public int starNum = 0; //解锁需要的星星
    private bool isSelect = false; //地图是否能被选择

    public GameObject stars; //玩家在本地图中的星星组件
    public GameObject locks; //地图上的锁

    public GameObject panal; //存储关卡选择组件
    public GameObject map; //存储地图组件

    public int startNum = 1; //panal中level的开始关卡
    public int endNum = 3; //panal中level的结束关卡
    public Text starsText;//panal中已经获得和未获得星星的比例

	// Use this for initialization
	void Start () 
    {
        //PlayerPrefs.DeleteAll();//清除数据
        //记录玩家获得的星星放在 totalNum
		if(PlayerPrefs.GetInt("totalNum", 0) >= starNum)
        {
            isSelect = true;
        }

        if(isSelect)
        {
            stars.SetActive(true);
            locks.SetActive(false);
            //panal中的text的显示
            int sum = 0;
            for(int i = startNum; i <= endNum; i++)
            {
                sum += PlayerPrefs.GetInt("level" + i.ToString(), 0);
            }
            starsText.text = sum.ToString() + "/9";
        }
	}
    /// <summary>
    /// 鼠标点击
    /// </summary>
    public void Selected()
    {
        if (isSelect)
        {
            map.SetActive(false);
            panal.SetActive(true);
        }
    }
    public void PanalSelect()
    {
        map.SetActive(true);
        panal.SetActive(false);
    }
}
