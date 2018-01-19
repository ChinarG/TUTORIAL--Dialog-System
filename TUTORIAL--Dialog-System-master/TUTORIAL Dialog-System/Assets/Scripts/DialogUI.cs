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