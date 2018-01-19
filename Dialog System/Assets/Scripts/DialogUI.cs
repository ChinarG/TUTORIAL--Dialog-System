using System;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //引用命名空间


/// <summary>
/// 枚举内容类型
/// </summary>
public enum CommandType
{
	Say, //说话内容
	Bgm, //背景音
	Bg   //背景
}


/// <summary>
/// 内容类
/// </summary>
public class Command
{
	public CommandType type;
}


/// <summary>
/// 说话类
/// </summary>
public class Say : Command
{
	public string name;    //名字
	public string image;   //图片
	public string sound;   //音乐
	public string content; //内容
}


/// <summary>
/// 背景音
/// </summary>
public class Bgm : Command
{
	public string name; //名字
}


/// <summary>
/// 背景
/// </summary>
public class Bg : Command
{
	public string name; //名字
}


/// <summary>
/// 对话系统
/// </summary>
public class DialogUI : MonoBehaviour
{
	public  List<Command> Commands = new List<Command>(); //声明一个 List 数组 类型为：Command
	private int           _index   = 0;                   //默认索引为0
	public  GameObject    GameImage;                      //游戏界面
	public  Image         BgImage;                        //背景图
	public  Image         HeadPortrait;                   //头像
	public  Text          NameText;                       //名字文本
	public  Text          ConttentText;                   //内容文本
	private bool          _isExecute = false;             //是否执行命令：默认不执行
	public  GameObject    ReloadBut;

	/// <summary>
	/// 初始化方法
	/// </summary>
	void Start()
	{
		AnalysisXml();                                                                            //调用解析XML方法
		GameObject.Find("StartGameButton").GetComponent<Button>().onClick.AddListener(StartGame); //给开始游戏按钮，添加监听事件
	}


	/// <summary>
	/// 开始游戏
	/// </summary>
	public void StartGame()
	{
		GameImage.SetActive(true); //激活游戏界面
		_isExecute = true;         //游戏开始：可以开始执行代码
		Execute();                 //游戏页面被激活的时候，就执行一次
	}


	/// <summary>
	/// 更新函数
	/// </summary>
	void Update()
	{
		if (Input.GetMouseButtonDown(0) && _isExecute == true) //如果按下鼠标左键
		{
			Execute();
		}
	}

	/// <summary>
	/// 执行对话命令函数
	/// </summary>
	public void Execute()
	{
		print(_index);
		if (_index >= Commands.Count)
		{
			print(Commands.Count);
			print("激活");
			ReloadBut.SetActive(true);                                         //激活重载按钮
			ReloadBut.GetComponent<Button>().onClick.AddListener(ReloadScene); //给按钮添加 重载场景 监听事件
			_isExecute = false;
			return;
		}

		Command command = Commands[_index++]; //自增：取出一条命令

		switch (command.type)
		{
			case CommandType.Say:                                     //如果类型是：Say 说话
				Say say             = (Say) command;                     //实例化 Say 对象 say
				HeadPortrait.sprite = Resources.Load<Sprite>(say.image); //更换头像
				NameText.text       = say.name;                          //人物
				ConttentText.text   = say.content;                       //说话内容
				if (!string.IsNullOrEmpty(say.sound))                    //如果音效不为空
				{
					AudioManager.Instance.PlaySe(say.sound); //播放音效
				}

				break;
			case CommandType.Bgm:                     //如果类型是：Bgm 背景音乐
				Bgm bgm = (Bgm) command;                 //实例化 Bgm 对象 bgm
				AudioManager.Instance.PlayBgm(bgm.name); //播放背景音乐
				Execute();                               //直接执行下一条
				break;
			case CommandType.Bg:            //如果类型是：Bg 背景
				Bg bg          = (Bg) command; //实例化 Bg 对象 bg
				BgImage.sprite = Resources.Load<Sprite>(bg.name);
				Execute(); //直接执行下一条
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
			if (ele.Name == "bgm")
			{
				Bgm bgm  = new Bgm();
				bgm.type = CommandType.Bgm;
				bgm.name = ele.InnerText;
				Commands.Add(bgm);
			}
			else if (ele.Name == "bg")
			{
				Bg bg   = new Bg();
				bg.type = CommandType.Bg;
				bg.name = ele.InnerText;
				Commands.Add(bg);
			}
			else if (ele.Name == "say")
			{
				Say say     = new Say();
				say.type    = CommandType.Say;
				say.name    = ele.ChildNodes[0].InnerText;
				say.image   = ele.ChildNodes[1].InnerText;
				say.sound   = ele.ChildNodes[2].InnerText;
				say.content = ele.ChildNodes[3].InnerText;
				Commands.Add(say);
			}
		}
	}
}