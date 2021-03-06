﻿using UnityEngine;
using System.Collections;
/**
 * 渐隐变换器
 */
public class UIFadeTransformer : Transformer
{
    public int m_nStartType;
    public float m_StartAlpha;
    public float m_SpeedAlpha;
    public float m_TargetAlpha;
    public CanvasGroup m_CanvasGroup;

    public static UIFadeTransformer FadeTo(GameObject target, float destAlpha, float time)
    {
        UIFadeTransformer transformer = new UIFadeTransformer();
        transformer.m_nStartType = 0;
        transformer.m_TargetAlpha = destAlpha;
        transformer.m_fTransformTime = time;
        transformer.target = target;
        return transformer;
    }
    public UIFadeTransformer()
    {
        m_Type = eTransformerID.UIFade;
    }
    public override void OnTransformStarted()
    {
        m_CanvasGroup =  target.GetComponent<CanvasGroup>();
        if (m_CanvasGroup == null)
            m_CanvasGroup = target.AddComponent<CanvasGroup>();

        float startAlpha = m_CanvasGroup.alpha;
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
        if (m_CanvasGroup == null)
            m_CanvasGroup = target.AddComponent<CanvasGroup>();
		if (currTime >= m_fEndTime)
		{
            m_CanvasGroup.alpha = m_TargetAlpha;
		}
		else
		{
			float timeElapased = currTime - m_fStartTime;
            m_CanvasGroup.alpha = (m_StartAlpha + m_SpeedAlpha * timeElapased);
		}
	}
}