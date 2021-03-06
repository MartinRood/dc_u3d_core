﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

/// <summary>
/// 界面动画
/// @author hannibal
/// @time 2016-12-11
/// </summary>
[RequireComponent(typeof(Image))]
public class UISpriteAnimation : UIComponentBase
{
    public List<Sprite> SpriteFrames;
    public float FPS = 12;
    public bool AutoPlay = true;
    public bool Loop = true;
    public bool Foward = true;
    public bool SetNativeSize = true;
    
    private Image mImageSource;
    private int mCurFrame = 0;
    private float mDelta = 0;
    private bool mIsPlaying = false;
    /// <summary>
    /// 完成回调
    /// </summary>
    private Action mCompleteFun = null;

    public override void Awake()
    {
        mImageSource = GetComponent<Image>();
    }

    void Start()
    {
        if (AutoPlay)
        {
            this.Play();
        }
        else
        {
            mIsPlaying = false;
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        if (AutoPlay)
        {
            this.Rewind();
        }
        else
        {
            mIsPlaying = false;
        }
    }

    public override void OnDisable()
    {
        this.Stop();
        base.OnDisable();
    }

    void Update()
    {
        if (!mIsPlaying || 0 == FrameCount)
        {
            return;
        }

        mDelta += Time.deltaTime;
        if (mDelta > 1 / FPS)
        {
            mDelta = 0;
            if (Foward)
            {
                mCurFrame++;
            }
            else
            {
                mCurFrame--;
            }

            if (mCurFrame >= FrameCount)
            {
                if (Loop)
                {
                    mCurFrame = 0;
                }
                else
                {
                    this.OnStop();
                    return;
                }
            }
            else if (mCurFrame < 0)
            {
                if (Loop)
                {
                    mCurFrame = FrameCount - 1;
                }
                else
                {
                    this.OnStop();
                    return;
                }
            }

            SetSprite(mCurFrame);
        }
    }
    /// <summary>
    /// 播放
    /// </summary>
    public void Play()
    {
        mIsPlaying = true;
        Foward = true;
    }
    /// <summary>
    /// 倒放
    /// </summary>
    public void PlayReverse()
    {
        mIsPlaying = true;
        Foward = false;
    }
    /// <summary>
    /// 重新播放
    /// </summary>
    public void Rewind()
    {
        mCurFrame = 0;
        SetSprite(mCurFrame);
        Play();
    }
    /// <summary>
    /// 停止
    /// </summary>
    public void Stop()
    {
        mCurFrame = 0;
        SetSprite(mCurFrame);
        mIsPlaying = false;
    }
    /// <summary>
    /// 暂停
    /// </summary>
    public void Pause()
    {
        mIsPlaying = false;
    }
    /// <summary>
    /// 恢复
    /// </summary>
    public void Resume()
    {
        if (!mIsPlaying)
        {
            mIsPlaying = true;
        }
    }
    /// <summary>
    /// 完成回调，针对非循环
    /// </summary>
    /// <param name="fun"></param>
    public void OnCompleted(Action fun)
    {
        mCompleteFun = fun;
    }
    private void SetSprite(int idx)
    {
        mImageSource.sprite = SpriteFrames[idx];
        if (SetNativeSize) mImageSource.SetNativeSize();
    }
    private void OnStop()
    {
        mIsPlaying = false;
        if(mCompleteFun != null)
        {
            mCompleteFun();
        }
    }
    public int FrameCount
    {
        get
        {
            return SpriteFrames.Count;
        }
    }
    public bool IsPlaying
    {
        get { return mIsPlaying; }
    }
}