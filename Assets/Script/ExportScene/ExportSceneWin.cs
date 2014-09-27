using UnityEngine;
using UnityEditor;
using System.Xml;
using System.IO;
using System.Collections.Generic;

public class ExportSceneWin : Editor
{
    /// <summary>
    /// 场景路径
    /// </summary>
    static string sPath;
    /// <summary>
    /// 场景名
    /// </summary>
    static string sName;
    /// <summary>
    /// 存放XML文件路径
    /// </summary>
    static string filepath = Application.dataPath + "/StreamingAssets/SceneXml/";
    //将所有游戏场景导出为XML格式
    [MenuItem("ExportScene/ExportXML")]
    static void ExportXML()
    {
        //遍历所有的游戏场景
        foreach (UnityEditor.EditorBuildSettingsScene scene in UnityEditor.EditorBuildSettings.scenes)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("Scenes");
            //当关卡启用
            if (scene.enabled)
            {
                //获得场景存放路径
                sPath = scene.path;
                //获取场景名称
                string[] arr_path = sPath.Split('/');
                sName = arr_path[arr_path.Length - 1].Split('.')[0];
                //获取场景配置文件存放路劲
                filepath = filepath + "/" + sName + ".xml";

                if (!File.Exists(filepath))
                {
                    File.Create(filepath);
                }

                //创建XML跟节点
                EditorApplication.OpenScene(sPath);
                XmlElement scenes = xmlDoc.CreateElement("Scene");
                scenes.SetAttribute("path", sPath);
                //找到场景中所有的跟节点
                List<GameObject> list_parantObj = new List<GameObject>();
                foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
                {
                    if (!list_parantObj.Contains(obj.transform.root.gameObject))
                    {
                        list_parantObj.Add(obj.transform.root.gameObject);
                    }
                }
                //在XMl中创建其跟节点
                for (int i = 0; i < list_parantObj.Count; i++)
                {
                    XmlElement gameObject = xmlDoc.CreateElement("gameObject");
                    gameObject.SetAttribute("name", list_parantObj[i].name);
                    gameObject.AppendChild(CreattransformNot(xmlDoc, list_parantObj[i]));
                    scenes.AppendChild(gameObject);
                    root.AppendChild(scenes);
                    xmlDoc.AppendChild(root);
                    xmlDoc.Save(filepath);
                }
                //知道其跟节点，知道子节点，怎么找到子节点路劲 ?

                AssetDatabase.Refresh();

            }
        }
        //刷新Project视图， 不然需要手动刷新哦
    }

    static XmlElement CreattransformNot(XmlDocument xmlDoc, GameObject obj, string path = null)
    {
        XmlElement transform = xmlDoc.CreateElement("transform");
        XmlElement position = xmlDoc.CreateElement("position");
        XmlElement position_x = xmlDoc.CreateElement("x");
        position_x.InnerText = obj.transform.position.x + "";
        XmlElement position_y = xmlDoc.CreateElement("y");
        position_y.InnerText = obj.transform.position.y + "";
        XmlElement position_z = xmlDoc.CreateElement("z");
        position_z.InnerText = obj.transform.position.z + "";

        position.AppendChild(position_x);
        position.AppendChild(position_y);
        position.AppendChild(position_z);

        XmlElement rotation = xmlDoc.CreateElement("rotation");
        XmlElement rotation_x = xmlDoc.CreateElement("x");
        rotation_x.InnerText = obj.transform.rotation.eulerAngles.x + "";
        XmlElement rotation_y = xmlDoc.CreateElement("y");
        rotation_y.InnerText = obj.transform.rotation.eulerAngles.y + "";
        XmlElement rotation_z = xmlDoc.CreateElement("z");
        rotation_z.InnerText = obj.transform.rotation.eulerAngles.z + "";
        XmlElement rotation_w = xmlDoc.CreateElement("w");
        position_z.InnerText = obj.transform.rotation.w + "";
        rotation.AppendChild(rotation_x);
        rotation.AppendChild(rotation_y);
        rotation.AppendChild(rotation_z);
        rotation.AppendChild(rotation_w);

        XmlElement scale = xmlDoc.CreateElement("scale");
        XmlElement scale_x = xmlDoc.CreateElement("x");
        scale_x.InnerText = obj.transform.localScale.x + "";
        XmlElement scale_y = xmlDoc.CreateElement("y");
        scale_y.InnerText = obj.transform.localScale.y + "";
        XmlElement scale_z = xmlDoc.CreateElement("z");
        scale_z.InnerText = obj.transform.localScale.z + "";

        scale.AppendChild(scale_x);
        scale.AppendChild(scale_y);
        scale.AppendChild(scale_z);

        transform.AppendChild(position);
        transform.AppendChild(rotation);
        transform.AppendChild(scale);

        return transform;
    }

    static XmlElement CreatComponentNot(XmlDocument xmlDoc, GameObject obj, string path = null)
    {
        
        
    }

    //将所有游戏场景导出为JSON格式
    [MenuItem("ExportScene/ExportJSON")]

    static void ExportJSON()
    {

    }


}
