using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

    public bool isSelect = false;
    public Sprite levelBG; //存储没关卡的去除锁之后的背景
    private Image image; //存储关卡组件

    public GameObject[] stars; //存储关卡下面的三个星星

    private void Awake()
    {
        image = GetComponent<Image>();
    }

	// Use this for initialization
	void Start () {

        if (transform.parent.GetChild(0).name == gameObject.name)
        {
            isSelect = true;
        }
        else
        {// 判断当前关卡是否可以选择
            int beforeNum = int.Parse(gameObject.name) - 1;
            if (PlayerPrefs.GetInt("level" + beforeNum.ToString()) > 0)
            {
                isSelect = true;
            }
        }
        if (isSelect)
        {
            image.overrideSprite = levelBG;
            transform.Find("num").gameObject.SetActive(true);
        }
        //获取现在关卡对应的名字，然后获得对应的星星个数
        int count = PlayerPrefs.GetInt("level" + gameObject.name);
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                stars[i].SetActive(true);
            }
        }
	}
    /// <summary>
    /// 储存当前可被选择关卡的名字
    /// </summary>
    public void Selected()
    {
        if(isSelect)
        {
            PlayerPrefs.SetString("nowLevel", "level" + gameObject.name);
            SceneManager.LoadScene(2);
        }
    }

}
