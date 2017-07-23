﻿

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FireSyncronizer : MonoBehaviour {
    public float m_Brightness = 8.0f;
    public float m_Speed = 1.0f;
    static MaterialPropertyBlock m_MatProps;
    public float m_StartTime = 0.0f;
    public Skin customSkin;

    public Material flameMat;
    public static Material[] flameMaterials;

    void Start()
    {
        if (Application.isPlaying)
        {
            flameMaterials = new Material[6];
            for (int i = 0; i < flameMaterials.Length; ++i)
            {
                flameMaterials[i] = new Material(flameMat);

                if (customSkin.sustain_mats[i])
                    flameMaterials[i].color = customSkin.sustain_mats[i].color;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        Fire.m_Brightness = m_Brightness;
        Fire.m_Speed = m_Speed;

        if (!Fire.cam)
        {
            Fire.cam = Camera.main;
            Fire.cam.depthTextureMode |= DepthTextureMode.Depth;
        }

        if (Application.isPlaying)
        {
            float time = Time.time;
            Fire.m_TimeElapsed += m_Speed * (time - Fire.m_LastFrameTime);

            Fire.m_LastFrameTime = time;
            Shader.SetGlobalFloat("_FireTime", m_StartTime + Fire.m_TimeElapsed);
        }
        else
        {
            Shader.SetGlobalFloat("_FireTime", m_StartTime);
        }
    }
}