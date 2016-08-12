﻿namespace PlayFab.Editor
{
    using System;
    using UnityEngine;
    using UnityEditor;
    using System.Collections.Generic;

    public class ListDisplay : BaseUiComponent {
        public List<KvpItem> items;

        public string displayTitle = "New List Display";

        // this gets called after the Base draw loop
        public override void PostDraw()
        {
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(this.displayTitle);
                if(GUILayout.Button("Refresh"))
                {

                    //BaseUiAnimationController.StartAlphaFade(1, 0, listDisplay);
                    Action<PlayFab.Editor.EditorModels.GetTitleDataResult> cb = (result) => {
                        items.Clear();
                        foreach(var kvp in result.Data)
                        {
                            items.Add(new KvpItem(kvp.Key, kvp.Value));
                        }
                    };

                    Action<PlayFab.Editor.EditorModels.PlayFabError> onError = (err) => 
                    {
                        Debug.LogError(err.ErrorMessage);
                    };



                    PlayFabEditorApi.GetTitleData(cb, onError); 

                }

                if(GUILayout.Button("+"))
                {
                    AddRecord();
                }

            EditorGUILayout.EndHorizontal();


            if(items.Count > 0)
            {
                EditorGUILayout.BeginVertical(PlayFabEditorHelper.uiStyle.GetStyle("listDisplayBox"), GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth));
                    float keyInputBoxWidth = EditorGUIUtility.currentViewWidth > 200 ? 150 : (EditorGUIUtility.currentViewWidth - 100) / 2;
                    float valueInputBoxWidth = EditorGUIUtility.currentViewWidth > 200 ? EditorGUIUtility.currentViewWidth - 270 : (EditorGUIUtility.currentViewWidth - 100) / 2; 

                      for(var z = 0; z < this.items.Count; z++)
                    {
                        if(items[z].Value != null)
                        {

                            GUIContent c1 = new GUIContent(items[z].Value);
                            //Rect r1 = GUILayoutUtility.GetRect(c1, EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).GetStyle("TextArea"));

                            var style = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).GetStyle("TextArea");
                            var h = style.CalcHeight(c1, valueInputBoxWidth);


                            EditorGUILayout.BeginHorizontal(GUILayout.MaxHeight(200));



                            items[z].Key = GUILayout.TextField(items[z].Key, GUILayout.Width(keyInputBoxWidth));

                            EditorGUILayout.LabelField(":", GUILayout.MaxWidth(10));
                            GUILayout.Label(""+items[z].Value, GUILayout.MaxWidth(valueInputBoxWidth), GUILayout.MaxHeight(15));  

                            if(GUILayout.Button("E", GUILayout.MaxHeight(19), GUILayout.MaxWidth(20)))
                            {

                            } 
                            if(GUILayout.Button("X", GUILayout.MaxHeight(19), GUILayout.MaxWidth(20)))
                            {
//                                if(string.IsNullOrEmpty(items[z].Key))
//                                {
                                    items.RemoveAt(z);// items[z].Value = null;
//                                }
                            } 
                          
                            EditorGUILayout.EndHorizontal();
                        }
                    }


                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginHorizontal();
                if(GUILayout.Button("Save", GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth - 30)))
                    {
                        //BaseUiAnimationController.StartAlphaFade(1, 0, listDisplay);
                    }
                EditorGUILayout.EndHorizontal();
            }


            // draw code here.
            base.PostDraw();
        }


        public void AddRecord()
        {
            this.items.Add(new KvpItem("",""));
        }

        public void RefreshRecords()
        {
            
        }

        public void SaveRecords()
        {
            
        }



        public ListDisplay(List<KvpItem> i = null)
        {
            this.items = i ?? new List<KvpItem>();
        }

        public ListDisplay()
        {
            this.items = new List<KvpItem>();
        }





    }


    public class KvpItem
    {
        public string Key;
        public string Value;

        public KvpItem(string k, string v)
        {
            this.Key = k;
            this.Value = v;
        }
    }
}

