﻿using UnityEngine;
using System.Collections;

/// <summary>
/// mesh渐隐变换器
/// 1.受材质影响比较，此功能需要材质具有_TintColor项
/// @author hannibal
/// @time 2017-12-29
/// </summary>
public class MeshFadeTransformer : Transformer
{
    private int m_nStartType;
    private float m_StartAlpha;
    private float m_SpeedAlpha;
    private float m_TargetAlpha;
    private Renderer m_MeshRender;
    private Color m_InitColor = Color.white;

    public static MeshFadeTransformer FadeTo(GameObject target, float destAlpha, float time)
    {
        MeshFadeTransformer transformer = new MeshFadeTransformer();
        transformer.m_nStartType = 0;
        transformer.m_TargetAlpha = destAlpha;
        transformer.m_fTransformTime = time;
        transformer.target = target;
        return transformer;
    }
    public MeshFadeTransformer()
    {
        m_Type = eTransformerID.MeshFade;
    }
    public override void OnTransformStarted()
    {
        m_MeshRender = target.GetComponentInChildren<Renderer>();
        if (m_MeshRender == null || m_MeshRender.material == null) return;

        m_InitColor = m_MeshRender.material.GetColor("_TintColor");
        float startAlpha = m_InitColor.a;
        m_StartAlpha = startAlpha;
        if (m_nStartType == 0)
        {
            m_SpeedAlpha = (m_TargetAlpha - startAlpha) / m_fTransformTime;
        }
        else if (m_nStartType == 1)
        {
            m_TargetAlpha = (startAlpha + m_SpeedAlpha * m_fTransformTime);
        }
    }
    public override void runTransform(float currTime)
    {
        if (m_MeshRender == null || m_MeshRender.material == null) return;

		if (currTime >= m_fEndTime)
		{
            m_InitColor.a = m_TargetAlpha;
		}
		else
		{
			float timeElapased = currTime - m_fStartTime;
            m_InitColor.a = (m_StartAlpha + m_SpeedAlpha * timeElapased);
        }
        m_MeshRender.material.SetColor("_TintColor", m_InitColor);
	}
}