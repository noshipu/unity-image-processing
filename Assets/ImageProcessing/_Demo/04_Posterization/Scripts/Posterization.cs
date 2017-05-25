using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ViRD.ImageProcessing
{
    // RGBを入れ替えて色を変更させる
    public class Posterization : MonoBehaviour
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

            int split = 3;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var color = inputColors[(width * y) + x];
                    for (int i = 0; i < split; i++)
                    {
                        float col1 = i * (1f / (float)split);
                        float col2 = (i + 1f) * (1f / (float)split);
                        if(col1 <= color.r && color.r <= col2)
                        {
                            color.r = (col1 + col2) / 2f;
                        }
                        if (col1 <= color.g && color.g <= col2)
                        {
                            color.g = (col1 + col2) / 2f;
                        }
                        if (col1 <= color.b && color.b <= col2)
                        {
                            color.b = (col1 + col2) / 2f;
                        }
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