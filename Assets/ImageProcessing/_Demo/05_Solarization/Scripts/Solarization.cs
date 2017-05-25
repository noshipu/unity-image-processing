using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ViRD.ImageProcessing
{
    // ソラリゼーション
    public class Solarization : MonoBehaviour
    {
        [SerializeField] private RawImage m_InputRawImage;
        [SerializeField] private RawImage m_OutputRawImage;

        void Start()
        {
            // 入力テクスチャの色情報を取得
            Texture2D inputTexture = (Texture2D)m_InputRawImage.mainTexture;
            int width = inputTexture.width;
            int height = inputTexture.height;
            Color[] inputColors = inputTexture.GetPixels();
            Debug.Log("Finished load input image.");

            // 画像処理を行う
            Texture2D outputTexture = new Texture2D(width, height);
            Color[] outputColors = new Color[width*height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var color = inputColors[(width * y) + x];
                    if(color.r <= 0.5f)
                    {
                        color.r = 2 * color.r;
                    }
                    else
                    {
                        color.r = 1 * 2 - 2 * color.r;
                    }
                    if (color.g <= 0.5f)
                    {
                        color.g = 2 * color.g;
                    }
                    else
                    {
                        color.g = 1 * 2 - 2 * color.g;
                    }
                    if (color.b <= 0.5f)
                    {
                        color.b = 2 * color.b;
                    }
                    else
                    {
                        color.b = 1 * 2 - 2 * color.b;
                    }
                    outputColors[(width * y) + x] = color;
                }
            }
            outputTexture.SetPixels(outputColors);
            outputTexture.Apply();
            Debug.Log("Finished set output image.");

            // 出力テクスチャに貼り付け
            m_OutputRawImage.texture = outputTexture;
        }
    }
}