using UnityEngine;
using UnityEditor;
using System.Xml;
using System.IO;
using System.Collections.Generic;


public class CreatComponentNot 
{
    private XmlDocument xmlDoc;
    private GameObject obj;
    List<System.Type> list_type = new List<System.Type>();
    public CreatComponentNot(XmlDocument xmlDoc, GameObject obj)
    {
        this.xmlDoc = xmlDoc;
        this.obj = obj;
    }

    public void GetComponentType()
    {
       //UIRoot
       Rigidbody
    }

}

