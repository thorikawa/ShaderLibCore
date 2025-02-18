﻿using UnityEngine;
using UnityEngine.Rendering;

namespace ComputeShaderUtil
{
    public class RenderTexUtil : MonoBehaviour
    {
        //
        // Creates RenderTexture.
        // It seems like ComputeShader only supports RenderTexture generated on a script.
        // So I recommend to use this for it.
        //
        // examples:
        //   RenderTexUtil.CreateRenderTexture(
        //     rippleTex.width, rippleTex.height, 0
        //     RenderTextureFormat.ARGBFloat, TextureWrapMode.Repeat,
        //     FilterMode.Point);
        public static RenderTexture CreateRenderTexture(int width, int height, int depth, RenderTextureFormat format, TextureWrapMode wrapMode = TextureWrapMode.Repeat, FilterMode filterMode = FilterMode.Bilinear, RenderTexture rt = null)
        {
            if (rt != null)
            {
                if (rt.width == width && rt.height == height) return rt;
            }

            ReleaseRenderTexture(rt);
            rt = new RenderTexture(width, height, depth, format);
            rt.enableRandomWrite = true;
            rt.wrapMode = wrapMode;
            rt.filterMode = filterMode;
            rt.Create();
            ClearRenderTexture(rt, Color.clear);
            return rt;
        }

        public static RenderTexture CreateVolumetricRenderTexture(int width, int height, int volumeDepth, int depth, RenderTextureFormat format, TextureWrapMode wrapMode = TextureWrapMode.Repeat, FilterMode filterMode = FilterMode.Bilinear, RenderTexture rt = null)
        {
            if (rt != null)
            {
                if (rt.width == width && rt.height == height) return rt;
            }

            ReleaseRenderTexture(rt);
            rt = new RenderTexture(width, height, depth, format);
            rt.dimension = TextureDimension.Tex3D;
            rt.volumeDepth = volumeDepth;
            rt.enableRandomWrite = true;
            rt.wrapMode = wrapMode;
            rt.filterMode = filterMode;
            rt.Create();
            ClearRenderTexture(rt, Color.clear);
            return rt;
        }

        public static void ReleaseRenderTexture(RenderTexture rt)
        {
            if (rt == null) return;

            rt.Release();
            Destroy(rt);
        }

        public static void ClearRenderTexture(RenderTexture target, Color bg)
        {
            var active = RenderTexture.active;
            RenderTexture.active = target;
            GL.Clear(true, true, bg);
            RenderTexture.active = active;
        }
    }
}