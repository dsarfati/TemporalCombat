using System.Collections;
using System.Collections.Generic;
using Anima2D;
using UnityEngine;

public static class Extensions
{
    //Breadth-first search
    public static Transform FindDeepChild(this Transform aParent, string aName)
    {
        var result = aParent.Find(aName);
        if (result != null)
            return result;
        foreach (Transform child in aParent)
        {
            result = child.FindDeepChild(aName);
            if (result != null)
                return result;
        }
        return null;
    }

    public static void SetMaterial(this GameObject go, Material mat)
    {
        var sprites = go.GetComponentsInChildren<SpriteRenderer>();
        var meshes = go.GetComponentsInChildren<Anima2D.SpriteMeshInstance>();

        foreach (var sprite in sprites)
        {
            sprite.material = mat;
        }

        foreach (var mesh in meshes)
        {
            mesh.sharedMaterial = mat;
            // if (mat.color != Color.white)
            mesh.color = mat.color;
        }
    }

    public static void SetColor(this GameObject go, Color color)
    {
        go.transform.FindDeepChild("Body").GetComponent<SpriteRenderer>().color = color;
        go.transform.FindDeepChild("Leg L").GetComponent<SpriteMeshInstance>().color = color;
        go.transform.FindDeepChild("Leg R").GetComponent<SpriteMeshInstance>().color = color;
    }


    /*
    //Depth-first search
    public static Transform FindDeepChild(this Transform aParent, string aName)
    {
        foreach(Transform child in aParent)
        {
            if(child.name == aName )
                return child;
            var result = child.FindDeepChild(aName);
            if (result != null)
                return result;
        }
        return null;
    }
    */
}
