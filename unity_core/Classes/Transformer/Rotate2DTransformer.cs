﻿using UnityEngine;
using System.Collections;

/// <summary>
/// 2d旋转变换器
/// @author hannibal
/// @time 2016-2-14
/// </summary>
public class Rotate2DTransformer : Transformer 
{
    private int m_nStartType;
    private float m_fStartDegree;
    private float m_fTargetDegree;
    private float m_fSpeed;

    /// <summary>
    /// 绝对旋转
    /// </summary>
    /// <param name="target">目标对象</param>
    /// <param name="degree">目标角度</param>
    /// <param name="time">变换时长</param>
    /// <returns></returns>
    public static Rotate2DTransformer rotateTo(GameObject target, float degree, float time)
    {
        Rotate2DTransformer transformer = new Rotate2DTransformer();
        transformer.m_nStartType = 0;
        transformer.m_fTargetDegree = degree;
        transformer.m_fTransformTime = time;
        transformer.target = target;
        return transformer;
    }
    /// <summary>
    /// 相对旋转
    /// </summary>
    /// <param name="target">目标对象</param>
    /// <param name="speed">速度：每秒变换角度</param>
    /// <param name="time">变换时长</param>
    /// <returns></returns>
    public static Rotate2DTransformer rotateBy(GameObject target, float speed, float time)
    {
        Rotate2DTransformer transformer = new Rotate2DTransformer();
        transformer.m_nStartType = 1;
        transformer.m_fSpeed = speed;
        transformer.m_fTransformTime = time;
        transformer.target = target;
        return transformer;
    }
    public Rotate2DTransformer()
    {
        m_Type = eTransformerID.Rotate2D;
    }
    public override void OnTransformStarted()
    {
        //获得当前对象的缩放
        Vector3 euler = target.transform.localEulerAngles;
        m_fStartDegree = euler.z;
        if (m_nStartType == 0)
        {
            m_fSpeed = (m_fTargetDegree - euler.z) / m_fTransformTime;
        }
        base.OnTransformStarted();
    }
    public override void runTransform(float currTime)
    {
        if (m_nStartType == 0)
        {
            Vector3 euler = target.transform.localEulerAngles;
            if (currTime >= m_fEndTime)
            {
                target.transform.localEulerAngles = new Vector3(euler.x, euler.y, m_fTargetDegree);
            }
            else
            {
                float timeElapased = currTime - m_fStartTime;
                target.transform.localEulerAngles = new Vector3(euler.x, euler.y, m_fStartDegree + m_fSpeed * timeElapased);
            }
        }
        else if (m_nStartType == 1)
        {
            Vector3 euler = target.transform.localEulerAngles;
            target.transform.localEulerAngles = new Vector3(euler.x, euler.y, euler.z + Time.deltaTime * m_fSpeed);
        }
    }
}
