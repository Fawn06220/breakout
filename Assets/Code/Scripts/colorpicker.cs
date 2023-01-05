using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class colorpicker : MonoBehaviour
{
    private GameObject sol;
    private Texture2D tex;


    // Start is called before the first frame update
    void Start()
    {
        sol = GameObject.Find("Sol");
        Renderer renderer = sol.GetComponent<Renderer>();

        tex = renderer.material.mainTexture as Texture2D;

        Color32[] texColors = tex.GetPixels32();
        int total = texColors.Length;
        Dictionary<int, int> colors = new Dictionary<int, int>();

        int max = 1;
        Color32 mostCol = texColors[0]; //default to first texel
        mostCol.GetHashCode();
        for (int i = 0; i < total; i++)
            {

                Color32Array c = new Color32Array(); 
                c.color = texColors[i];

                if (colors.ContainsKey(c.key))
                {
                    int count = ++colors[c.key];
                    if (count > max)
                    {
                        max = count;
                        mostCol = c.color;
                    }
                }
                else colors.Add(c.key, 0);
            }

        if (mostCol == Color.black)
        {
            mostCol = new Color(0.2f, 0.2f, 0.2f, 1F);
        }

        //On applique la couleur sur le material du gameobject
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = mostCol;
        

    }



    // Update is called once per frame
    void Update()
    {
        
    }
    [StructLayout(LayoutKind.Explicit)]
    public struct Color32Array
    {
        [FieldOffset(0)]
        public int key;

        [FieldOffset(0)]
        public Color32 color;
    }
}
