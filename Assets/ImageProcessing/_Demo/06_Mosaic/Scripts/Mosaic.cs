using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ViRD.ImageProcessing
{
    // モザイク
    public class Mosaic : MonoBehaviour
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

            int size = 151;
            for (int y = (size - 1) / 2; y < height; y = y + size)
            {
                for (int x = (size - 1) / 2; x < width; x = x + size)
                {
                    float colorR = 0f;
                    float colorG = 0f;
                    float colorB = 0f;

                    for (int j = y - (size - 1) / 2; j <= y + (size - 1) / 2; j++)
                    {
                        for (int i = x - (size - 1) / 2; i <= x + (size - 1) / 2; i++)
                        {
                            if (i >= 0 && j >= 0 && i < width && j < height)
                            {
                                colorR += inputColors[(width * j) + i].r;
                                colorG += inputColors[(width * j) + i].g;
                                colorB += inputColors[(width * j) + i].b;
                            }
                        }
                    }

                    colorR = colorR / (size * size);
                    colorG = colorG / (size * size);
                    colorB = colorB / (size * size);

                    for (int j = y - (size - 1) / 2; j <= y + (size - 1) / 2; j++)
                    {
                        for (int i = x - (size - 1) / 2; i <= x + (size - 1) / 2; i++)
                        {
                            if (i >= 0 && j >= 0 && i < width && j < height)
                            {
                                outputColors[(width * j) + i] = new Color(colorR, colorG, colorB);
                            }
                        }
                    }
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