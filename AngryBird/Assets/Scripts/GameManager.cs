using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public List<Bird> birds;//存储小鸟
    public List<Pig> pig; //存储猪

    public static GameManager _instance;//用于小鸟组件对小鸟序列的控制

    private Vector3 originPos;//存储第一只小鸟的位置

    public GameObject win;//胜利界面
    public GameObject lose;//失败界面

    public GameObject[] stars;//存储星星

    private int starsNum = 0; //存储当前关卡星星的数量

    private int totalNum = 9; //总共的关卡

    private void Awake()
    {//将小鸟和猪的序列赋值
        _instance = this;
        //记录第一只小鸟的位置
        if(GameManager._instance.birds.Count > 0)
        {
            originPos = GameManager._instance.birds[0].transform.position;
        }
    }
    private void Start()
    {//开始游戏
        Initialized();
    }
    /// <summary>
    /// 初始化小鸟
    /// </summary>
    private void Initialized()
    {
        for(int i = 0; i < birds.Count; i++)
        {
            if(0 == i)
            {
                birds[i].transform.position = originPos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
            }
        }
    }
    /// <summary>
    /// 游戏逻辑判断
    /// </summary>
    public void NextBird()
    {
        if(pig.Count > 0)
        {//
            if(birds.Count > 0)
            {
                //游戏继续
                Initialized();
            }
            else
            {
                //输了
                lose.SetActive(true);
            }
        }
        else
        {
            //赢了
            win.SetActive(true);
        }
    }

    public void ShowStars()
    {//胜利之后触发
        StartCoroutine("show");
    }

    IEnumerator show()
    {//携程一下，让星星分开展示，不要一下出来
        //print(birds.Count);
        for (; starsNum < birds.Count + 1; starsNum++)
        {
            if (starsNum >= stars.Length)
            {
                break;
            }
            yield return new WaitForSeconds(0.2f);
            stars[starsNum].SetActive(true);
        }
    }
    /// <summary>
    /// 重玩返回第二游戏界面
    /// </summary>
    public void Retry()
    {
        saveDate();
        SceneManager.LoadScene(2);
    }
    /// <summary>
    /// 返回主页面
    /// </summary>
    public void Home()
    {
        saveDate();
        SceneManager.LoadScene(1);
    }
    public void saveDate()
    {
        //如果当前关卡的星星数量大于以前玩时的星星数量
        if (starsNum > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel")))
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), starsNum);
        }
        //遍历所有的关卡，存储星星的总数
        int sum = 0;
        for(int i = 1; i < totalNum; i++)
        {
            sum += PlayerPrefs.GetInt("level" + i.ToString());
        }
        PlayerPrefs.SetInt("totalNum", sum);
    }
}
