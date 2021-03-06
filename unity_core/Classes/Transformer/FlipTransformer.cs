﻿using UnityEngine;
using System.Collections;

/// <summary>
/// 镜像
/// @author hannibal
/// @time 2016-2-14
/// </summary>
public class FlipTransformer : Transformer 
{
    private bool m_FlipX;
    private bool m_FlipY;

    public static FlipTransformer flip(GameObject target, bool flipX, bool flipY)
    {
        FlipTransformer transformer = new FlipTransformer();
        transformer.m_FlipX = flipX;
        transformer.m_FlipY = flipY;
        transformer.target = target;
        return transformer;
    }
    public FlipTransformer()
    {
        m_Type = eTransformerID.Flip;
    }
    public override void OnTransformStarted()
    {
        Vector3 scale = m_Target.transform.localScale;
        if (m_FlipX)
            m_Target.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        else
            m_Target.transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        if (m_FlipY)
            m_Target.transform.localScale = new Vector3(scale.x, -scale.y, scale.z);
        else
            m_Target.transform.localScale = new Vector3(scale.x, scale.y, scale.z);
    }
}
