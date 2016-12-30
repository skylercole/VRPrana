/* CSharp version of above JavaScript file by MicroEyes */

using UnityEditor;  //Obviosly its an editor script.
using UnityEngine;  //Unity base Texture, Debug, Color, etc.
using System;       //Exception & Environment
using System.IO;    //FileStream, BinaryWritter.

public class SaveFontTexture
{

    [MenuItem("Assets/Save Font Texture...")]
    static void Init()
    {
        Texture2D l_texture = null;

        try
        {
            l_texture = (Texture2D)Selection.activeObject;
        }
        catch (InvalidCastException e)
        {
            Debug.Log("Selected Object is not a texture: " + Environment.NewLine + e.Message);
        }

        if (l_texture == null)
        {
            EditorUtility.DisplayDialog("No texture selected", "Please select a texture", "Cancel");
            return;
        }

        if (l_texture.format != TextureFormat.Alpha8)
        {
            EditorUtility.DisplayDialog("Wrong format", "Texture must be in uncompressed Alpha8 format", "Cancel");
            return;
        }

        Color[] l_pixels = l_texture.GetPixels();

        Texture2D l_newTexture = new Texture2D(l_texture.width, l_texture.height, TextureFormat.ARGB32, false);

        l_newTexture.SetPixels(l_pixels);

        var texBytes = l_newTexture.EncodeToPNG();

        string fileName = EditorUtility.SaveFilePanel("Save font texture", "", "font Texture", "png");

        if (fileName.Length > 0)
        {
            FileStream f = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            BinaryWriter b = new BinaryWriter(f);
            for (var i = 0; i < texBytes.Length; i++) b.Write(texBytes[i]);
            b.Close();
        }

        UnityEngine.Object.DestroyImmediate(l_texture);

    }
}