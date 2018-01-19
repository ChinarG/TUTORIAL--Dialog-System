# TUTORIAL--Dialog-System
一个简单的XML用法案例，对话系统。直接拿出来用即可。



<center><font size=7 >**用XML做的Unity游戏对话系统**<br><br></font>

----------
<table><tr><td bgcolor= #000000>
<center><font color=#FDF5E6 size=5>本文提供全流程，中文翻译。<br><br>Chinar坚持将简单的生活方式，带给世人！<br><br>（拥有更好的阅读体验 —— 高分辨率用户请根据需求调整网页缩放比例）
</font>
</td></tr></table>
<br>


----------


[toc]

----------

#<font size=7 >**1**</font>
##**<font face="Segoe UI Black" size=6> Create XML Document </font> —— 创建所需的XML文档</font>**
<br>

**<font color=#FF1493 face="微软雅黑" size=4>注意路径：</font>**

**我的XML文档"Dialog.xml"放在了<font face="Segoe UI Black" size=5> Assets </font>目录下创建的一个<font face="Segoe UI Black" size=5> Data </font>目录中**

**如果名字不一样，需要改名**

![这里写图片描述](http://img.blog.csdn.net/20180120013608821?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQvQ2hpbmFyQ1NETg==/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/SouthEast)

**或者直接修改解析XML代码中的读取路径，为你自己的路径**

**<font face="Segoe UI Black" size=4> `document.Load(Application.dataPath + "/Data/Dialog.xml"); //加载 XML 内容`</font>**

![举个栗子88](http://img.blog.csdn.net/20180115013437242?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQvQ2hpbmFyQ1NETg==/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/SouthEast)

```XML
<?xml version="1.0" encoding="utf-8"?>
<!-- Dialog System ; Background ; Image ; Music -->
<root>
  <bg>背景1</bg>
  <bgm>雨声</bgm>
  <say>
    <name>船长</name>
    <image>曲境一号船长1</image>
    <sound>花痴诗1</sound>
    <content>我的小鱼你醒了,还记认识早晨吗？</content>
  </say>
  <say>
    <name>秀秀</name>
    <image>秀秀1</image>
    <sound>花痴诗2</sound>
    <content>昨夜你曾经说， 愿夜幕永不开启。</content>
  </say>
  <say>
    <name>船长</name>
    <image>曲境一号船长2</image>
    <sound>花痴诗3</sound>
    <content>你的香腮边轻轻滑落的</content>
  </say>
  <say>
    <name>秀秀</name>
    <image>秀秀2</image>
    <sound>花痴诗4</sound>
    <content>是你的泪，还是我的泪？</content>
  </say>
  <bg>背景2</bg>
  <say>
    <name>秀秀</name>
    <image>秀秀3</image>
    <sound>花痴诗5</sound>
    <content>初吻吻别的那个季节</content>
  </say>
  <say>
    <name>船长</name>
    <image>曲境一号船长3</image>
    <sound>花痴诗6</sound>
    <content>不是已经哭过了吗？</content>
  </say>
  <bg>背景3</bg>
  <say>
    <name>船长</name>
    <image>曲境一号船长4</image>
    <sound>花痴诗7</sound>
    <content>我的指尖还记忆着</content>
  </say>
  <say>
    <name>秀秀</name>
    <image>秀秀4</image>
    <sound>花痴诗8</sound>
    <content>你慌乱的心跳</content>
  </say>
  <say>
    <name>船长</name>
    <image>曲境一号船长1</image>
    <sound>花痴诗9</sound>
    <content>温润的体香里</content>
  </say>
  <bg>背景4</bg>
  <say>
    <name>秀秀</name>
    <image>秀秀1</image>
    <sound>花痴诗10</sound>
    <content>那一缕长发飘飘…</content>
  </say>
</root>
```

----------

#<font size=7 >**2**</font>
##**<font face="Segoe UI Black" size=6> Audio Manager </font> —— 音频管理器脚本</font>**
<br>

**将此脚本挂在保证激活状态的<font face="Segoe UI Black" size=5> GameObject </font>上，并添加<font face="Segoe UI Black" size=5> 2 </font>个<font face="Segoe UI Black" size=5> AudioSource </font>组件**

**通过拖动的方式**

**在<font face="Segoe UI Black" size=5> Inspecter </font> 面板中将<font face="Segoe UI Black" size=5> 2 </font>个<font face="Segoe UI Black" size=5> AudioSource </font>组件拖到 ↓**

**背景音：<font face="Segoe UI Black" size=4>  BgmAudioSource  </font>和音效：<font face="Segoe UI Black" size=5>   SeAudioSource</font> 框中**

**代码直接<font face="Segoe UI Black" size=5> Copy </font>**

![举个栗子88](http://img.blog.csdn.net/20180115013437242?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQvQ2hpbmFyQ1NETg==/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/SouthEast)

```C#
using UnityEngine;


/// <summary>
/// 声音管理类
/// </summary>
public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;       //单例
	public        AudioSource  BgmAudioSource; //背景音
	public        AudioSource  SeAudioSource;  //音效
	private       AudioClip    _clip;          //音乐文件


	/// <summary>
	/// 初始化函数
	/// </summary>
	void Start()
	{
		Instance = this;
	}


	/// <summary>
	/// 播放背景音
	/// </summary>
	public void PlayBgm(string inName)
	{
		_clip               = Resources.Load<AudioClip>(inName); //加载音乐文件
		BgmAudioSource.clip = _clip;                             //更换音乐文件为Clip默认文件
		BgmAudioSource.Play();                                   //播放音乐
	}


	/// <summary>
	/// 播放音效
	/// </summary>
	public void PlaySe(string inName)
	{
		_clip = Resources.Load<AudioClip>(inName); //加载音乐文件
		SeAudioSource.PlayOneShot(_clip);          //播放音效，一声就完了
	}


	/// <summary>
	/// 停止背景音
	/// </summary>
	public void StopBgm()
	{
		BgmAudioSource.Stop(); //停止播放器
	}
}
```

![这里写图片描述](http://img.blog.csdn.net/20180120005837396?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQvQ2hpbmFyQ1NETg==/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/SouthEast)

----------
#<font size=7 >**3**</font>
##**<font face="Segoe UI Black" size=6> UI Manager </font> —— 界面交互控制</font>**
<br>

**将此脚本挂在保证激活状态的<font face="Segoe UI Black" size=5> GameObject </font>上**

**通过拖动的方式**

**<font face="Segoe UI Black" size=5> Inspecter </font> —— 面板中：分别添加声明的<font face="Segoe UI Black" size=5> 6 </font>个对象**

**代码直接<font face="Segoe UI Black" size=5> Copy </font>**

![举个栗子88](http://img.blog.csdn.net/20180115013437242?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQvQ2hpbmFyQ1NETg==/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/SouthEast)

```C#
using UnityEngine;
using System.Xml;                  //引用XML
using UnityEngine.UI;              //引用UI
using System.Collections.Generic;  //引用集合
using UnityEngine.SceneManagement; //引用命名空间



/// <summary>
/// 枚举指令类型
/// </summary>
public enum CommandType
{
	Say, //说话
	Bgm, //背景音
	Bg   //背景
}



/// <summary>
/// 基类：指令类
/// </summary>
public class Command
{
	public CommandType AllType; //定义成员变量 类型对象
}



/// <summary>
/// 说话指令类：继承 指令基类
/// </summary>
public class Say : Command
{
	public string Name;    //名字
	public string Image;   //图片
	public string Sound;   //音乐
	public string Content; //内容
}



/// <summary>
/// 背景音指令类：继承 指令基类
/// </summary>
public class Bgm : Command
{
	public string Name; //名字
}



/// <summary>
/// 背景指令类：继承 指令基类
/// </summary>
public class Bg : Command
{
	public string Name; //名字
}



/// <summary>
/// 对话系统
/// </summary>
public class DialogUI : MonoBehaviour
{
	public  List<Command> Commands = new List<Command>(); //声明一个 List 数组 类型为：Command
	private int           _index   = 0;                   //默认索引为0
	public  GameObject    GameImage;                      //游戏界面
	public  GameObject    ReloadBut;                      //重开按钮
	public  Image         BgImage;                        //背景图
	public  Image         HeadPortrait;                   //头像
	public  Text          NameText;                       //名字文本
	public  Text          ConttentText;                   //内容文本
	private bool          _isExecute = false;             //是否执行命令：默认不执行


	/// <summary>
	/// 初始化方法
	/// </summary>
	void Start()
	{
		AnalysisXml();                                                                            //调用解析XML方法
		GameObject.Find("StartGameButton").GetComponent<Button>().onClick.AddListener(StartGame); //给开始游戏按钮，添加监听事件
	}


	/// <summary>
	/// 更新函数
	/// </summary>
	void Update()
	{
		if (Input.GetMouseButtonDown(0)           && _isExecute == true ||
		    Input.GetKeyDown(KeyCode.KeypadEnter) && _isExecute == true) //如果按下鼠标左键或者按下Enter
		{
			OneByOneExecuteCommand(); //执行对话命令函数
		}
	}


	/// <summary>
	/// 开始游戏
	/// </summary>
	public void StartGame()
	{
		GameImage.SetActive(true); //激活游戏界面
		_isExecute = true;         //游戏开始：可以开始执行代码
		OneByOneExecuteCommand();  //游戏页面被激活的时候，就执行一次
	}


	/// <summary>
	/// 执行对话命令函数
	/// </summary>
	public void OneByOneExecuteCommand()
	{
		if (_index >= Commands.Count) //下标越界：读完
		{
			ReloadBut.SetActive(true);                                         //激活重载按钮
			ReloadBut.GetComponent<Button>().onClick.AddListener(ReloadScene); //给按钮添加 重载场景 监听事件
			_isExecute = false;                                                //关闭执行命令
			return;
		}

		Command command = Commands[_index++]; //自增：取出一条命令

		switch (command.AllType)
		{
			//如果类型是：Say 说话
			case CommandType.Say:
				Say say             = (Say) command;                     //实例化 Say 对象 say
				HeadPortrait.sprite = Resources.Load<Sprite>(say.Image); //更换头像
				NameText.text       = say.Name;                          //人物
				ConttentText.text   = say.Content;                       //说话内容
				if (!string.IsNullOrEmpty(say.Sound))                    //如果音效名不为空
				{
					AudioManager.Instance.PlaySe(say.Sound); //播放音效
				}
				break;

			//如果类型是：Bgm 背景音乐
			case CommandType.Bgm:
				Bgm bgm = (Bgm) command;                 //实例化 Bgm 对象 bgm
				AudioManager.Instance.PlayBgm(bgm.Name); //播放背景音乐
				OneByOneExecuteCommand();                //直接执行下一条
				break;

			//如果类型是：Bg 背景
			case CommandType.Bg:
				Bg bg          = (Bg) command;                    //实例化 Bg 对象 bg
				BgImage.sprite = Resources.Load<Sprite>(bg.Name); //更换背景图片
				OneByOneExecuteCommand();                         //直接执行下一条
				break;
		}
	}


	/// <summary>
	/// 重载场景
	/// </summary>
	public void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //重载当前场景
	}


	/// <summary>
	/// 解析XML
	/// </summary>
	private void AnalysisXml()
	{
		XmlDocument document = new XmlDocument();                 //实例化一个xml文档
		document.Load(Application.dataPath + "/Data/Dialog.xml"); //加载 XML 内容
		XmlElement rootEle = document.LastChild as XmlElement;    //根节点
		foreach (XmlElement ele in rootEle.ChildNodes)            //遍历根节点的所有子节点
		{
			if (ele.Name == "bgm") //如果元素的名字是XML文档中的节点"bgm"
			{
				Bgm bgm     = new Bgm();
				bgm.AllType = CommandType.Bgm;
				bgm.Name    = ele.InnerText;
				Commands.Add(bgm); //添加到命令数组 Commands 中
			}
			else if (ele.Name == "bg")
			{
				Bg bg      = new Bg();
				bg.AllType = CommandType.Bg;
				bg.Name    = ele.InnerText;
				Commands.Add(bg);
			}
			else if (ele.Name == "say")
			{
				Say say     = new Say();
				say.AllType = CommandType.Say;
				say.Name    = ele.ChildNodes[0].InnerText;
				say.Image   = ele.ChildNodes[1].InnerText;
				say.Sound   = ele.ChildNodes[2].InnerText;
				say.Content = ele.ChildNodes[3].InnerText;
				Commands.Add(say);
			}
		}
	}
}
```

![这里写图片描述](http://img.blog.csdn.net/20180120012213637?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQvQ2hpbmFyQ1NETg==/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/SouthEast)

----------
#<font size=7 >**4**</font>
##**<font face="Segoe UI Black" size=6> Finish </font> —— 测试运行</font>**
<br>
**直接点击运行  Or<font face="Segoe UI Black" size=5> Alt+P</font>**

**Chinar所作，Down下即可！**

![举个栗子88](http://img.blog.csdn.net/20180115013437242?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQvQ2hpbmFyQ1NETg==/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/SouthEast)            **附：[Chinar的GitHub工程案例](https://github.com/ChinarG/TUTORIAL--Dialog-System)**

![这里写图片描述](http://img.blog.csdn.net/20180120014938261?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQvQ2hpbmFyQ1NETg==/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/SouthEast)

----------
<center><font face="Segoe UI Black"  size=7> END</font>
<br>

<font face="微软雅黑"  size=5> 本博客为非营利性个人原创，除部分有明确署名的作品外，所刊登的所有作品的著作权均为本人所拥有，本人保留所有法定权利。违者必究
<br>对于需要复制、转载、链接和传播博客文章或内容的，请及时和本博主进行联系，留言，Email:  ichinar@icloud.com
<br>对于经本博主明确授权和许可使用文章及内容的，使用时请注明文章或内容出处并注明网址</font>


