using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    private bool isClick = false;//判断鼠标是否点击
    public float maxDis = 3;//小鸟与树枝的最大距离
    //不让sp在面板显示
    [HideInInspector]
    public SpringJoint2D sp; //弹簧组件
    protected Rigidbody2D rg; //小鸟

    public LineRenderer rightLine;//获取右线的组件
    public Transform rightTreePos;//右树枝的位置
    public LineRenderer leftLine;
    public Transform leftTreePos;

    public GameObject boom; //小鸟死亡特效

    protected TestMyTrail myTrail;//小鸟的拖尾效果

    private bool canMove = true;//防止小鸟射出后，很拾起来

    public float smooth = 3; //相机跟随的平滑度

    public AudioClip select;//点击小鸟的音效
    public AudioClip fly; //小鸟飞出的音效

    private bool isFly = false;//判断小鸟是否在飞

    protected SpriteRenderer render;//小鸟的照片显示
    public Sprite hurt;//存储受伤照片

    private void Awake()
    {
        //对变量进行初始化
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TestMyTrail>();
        render = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()//鼠标按下
    {
        if(canMove)
        {
            isClick = true;
            rg.isKinematic = true;//开启小鸟的动力学

            AudioPlay(select);//播放点击小鸟的音乐
        }
    }


    private void OnMouseUp() //鼠标抬起
    {
        if(canMove)
        {
            isClick = false;
            rg.isKinematic = false;//关闭小鸟的动力学，降低小鸟的速度
            Invoke("Fly", 0.1f);
            canMove = false;
        }

    }

    private void Update()
    {
        if (isClick)//鼠标一直按下
        {   //小鸟跟着鼠标移动
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.y);

            //小鸟与树枝的最大距离限定
            if(Vector3.Distance(transform.position, rightTreePos.position) > maxDis)
            {
                Vector3 pos = (transform.position - rightTreePos.position).normalized;
                pos *= maxDis;
                transform.position = pos + rightTreePos.position;
            }
            //画皮筋
            Line();
        }

        //相机跟随小鸟
        float posX = transform.position.x;//获取小鸟的坐标
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Mathf.Clamp(posX, -10, 10), Camera.main.transform.position.y,
    Camera.main.transform.position.z), smooth * Time.deltaTime);

        //炫技的触发
        if(isFly)
        {
            if(Input.GetMouseButtonDown(0))
            {
                ShowSkill();
            }
        }
    }

    //小鸟飞出后
    private void Fly()
    {
        isFly = true;
        //弹簧失效
        sp.enabled = false;
        //皮筋失效
        rightLine.enabled = false;
        leftLine.enabled = false;
        //添加拖尾效果
        myTrail.StartTrails();
        //播放小鸟飞出的音乐
        AudioPlay(fly);
        //添加死亡特效
        Invoke("Next", 4);
    }

    //画皮筋
    private void Line()
    {   //皮筋出现
        rightLine.enabled = true;
        leftLine.enabled = true;
        //画出皮筋
        rightLine.SetPosition(0, rightTreePos.position);
        rightLine.SetPosition(1, transform.position);
        leftLine.SetPosition(0, leftTreePos.position);
        leftLine.SetPosition(1, transform.position);
    }

    //下一只小鸟
    protected virtual void Next()
    {
        //删除当前已经飞出的小鸟
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        //下一只小鸟飞出
        GameManager._instance.NextBird();
    }
    /// <summary>
    /// 停止画小鸟的后尾
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //清除小鸟的拖尾效果
        myTrail.ClearTrails();

        isFly = false;
    }
    /// <summary>
    /// 静态音乐片段播放方法
    /// </summary>
    private void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
    /// <summary>
    /// 炫技
    /// </summary>
    public virtual void ShowSkill()
    {
        isFly = false;
    }
    /// <summary>
    /// 小鸟受伤
    /// </summary>
    public void Hurt()
    {
        render.sprite = hurt;
    }
}
