using UnityEngine;
using System.Collections;


/// <summary>
/// 音乐管理类
/// </summary>
public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;       //单例
	public        AudioSource  BgmAudioSource; //背景音
	public        AudioSource  SeAudioSource;  //音效
	private       AudioClip    clip;           //音乐文件


	void Start()
	{
		Instance = this;
	}


	/// <summary>
	/// 播放背景音
	/// </summary>
	public void PlayBgm(string name)
	{
		clip      = Resources.Load<AudioClip>(name); //加载音乐文件
		BgmAudioSource.clip = clip;                            //更换音乐文件为Clip默认文件
		BgmAudioSource.Play();                                 //播放音乐
	}


	/// <summary>
	/// 播放音效
	/// </summary>
	public void PlaySe(string name)
	{
		clip = Resources.Load<AudioClip>(name); //加载音乐文件
		SeAudioSource.PlayOneShot(clip);                  //播放音效，一声就完了
	}


	/// <summary>
	/// 停止背景音
	/// </summary>
	public void StopBgm()
	{
		BgmAudioSource.Stop();
	}
}