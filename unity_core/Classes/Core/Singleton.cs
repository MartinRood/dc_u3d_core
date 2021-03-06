﻿using UnityEngine;
using System.Collections;

/// <summary>
/// 单例
/// @author hannibal
/// @time 2014-11-1
/// </summary>
public class Singleton<T> where T : new()
{
	public Singleton() { }

	public static T Instance
	{
		get { return SingletonCreator.instance; }
	}
	
	class SingletonCreator
	{
		static SingletonCreator() { }
		internal static readonly T instance = new T();
	}
} 
