﻿namespace PlayFab.Editor
{
    using UnityEngine;
    using System.Collections;
    using UnityEditor;

    public class PlayFabEditorMenu : Editor
    {

        internal enum MenuStates
        {
            Data,
            Services,
            Sdks,
            Settings,
            Help,
            Logout,
            None
        }



        internal static MenuStates _menuState = MenuStates.Sdks;
        //private static readonly GUIStyle GlobalButtonStyle = PlayFabEditorHelper.GetTextButtonStyle();
        
        //Create a color vector for the background;
        //private static Vector3 ColorVector = PlayFabEditorHelper.GetColorVector(62);
        //private static Texture2D Background = PlayFabEditorHelper.MakeTex(1, 1, new Color(ColorVector.x, ColorVector.y, ColorVector.z));

        public static void DrawMenu()
        {
            if (EditorPrefs.HasKey("PLAYFAB_CURRENT_MENU"))
            {
                if (PlayFabEditorSDKTools.IsInstalled)
                {
                    _menuState = (MenuStates) EditorPrefs.GetInt("PLAYFAB_CURRENT_MENU");
                }
            }

            //using Begin Vertical as our container.

            var servicesButtonStyle = PlayFabEditorHelper.uiStyle.GetStyle("textButton");
            var sdksButtonStyle = PlayFabEditorHelper.uiStyle.GetStyle("textButton");
            var settingsButtonStyle = PlayFabEditorHelper.uiStyle.GetStyle("textButton");
            var dataButtonStyle = PlayFabEditorHelper.uiStyle.GetStyle("textButton");
            var helpButtonStyle = PlayFabEditorHelper.uiStyle.GetStyle("textButton");
            var logoutButtonStyle = PlayFabEditorHelper.uiStyle.GetStyle("textButton");

            //TODO Move to state machine for states.
            if (_menuState == MenuStates.Services)
            {
                servicesButtonStyle = PlayFabEditorHelper.uiStyle.GetStyle("textButton_selected");
            }
            else
            {
                servicesButtonStyle = PlayFabEditorHelper.uiStyle.GetStyle("textButton");
            }

            if (_menuState == MenuStates.Sdks)
            {
                sdksButtonStyle = PlayFabEditorHelper.uiStyle.GetStyle("textButton_selected");
            }
            else
            {
                sdksButtonStyle = PlayFabEditorHelper.uiStyle.GetStyle("textButton");
            }

            if (_menuState == MenuStates.Settings)
            {
                settingsButtonStyle = PlayFabEditorHelper.uiStyle.GetStyle("textButton_selected");
            }
            else
            {
                settingsButtonStyle = PlayFabEditorHelper.uiStyle.GetStyle("textButton");
            }

            if (_menuState == MenuStates.Logout)
            {
                logoutButtonStyle = PlayFabEditorHelper.uiStyle.GetStyle("textButton_selected");
            }
            else
            {
                logoutButtonStyle = PlayFabEditorHelper.uiStyle.GetStyle("textButton");
            }

            if (_menuState == MenuStates.Data)
            {
                dataButtonStyle = PlayFabEditorHelper.uiStyle.GetStyle("textButton_selected");
            }
            else
            {
                dataButtonStyle = PlayFabEditorHelper.uiStyle.GetStyle("textButton");
            }

            if (_menuState == MenuStates.Help)
            {
                helpButtonStyle = PlayFabEditorHelper.uiStyle.GetStyle("textButton_selected");
            }
            else
            {
                helpButtonStyle = PlayFabEditorHelper.uiStyle.GetStyle("textButton");
            }

            GUILayout.BeginHorizontal(PlayFabEditorHelper.uiStyle.GetStyle("gpStyleGray1"), GUILayout.Height(25), GUILayout.ExpandWidth(true));
   
            GUILayout.Space(5);

            if (GUILayout.Button("SDK", sdksButtonStyle, GUILayout.MaxWidth(35)))
            {
                _menuState = MenuStates.Sdks;
                OnSdKsClicked();
            }

           // GUILayout.Space(15);

            if (PlayFabEditorSDKTools.IsInstalled && PlayFabEditorSDKTools.isSdkSupported)
            {

                if (GUILayout.Button("DATA", dataButtonStyle, GUILayout.MaxWidth(60)))
                {
                    _menuState = MenuStates.Data;
                    OnDataClicked();
                }

                //GUILayout.Space(15);

                if (GUILayout.Button("SETTINGS", settingsButtonStyle, GUILayout.MaxWidth(60)))
                {
                    _menuState = MenuStates.Settings;
                    OnSettingsClicked();
                }

            }

            if (GUILayout.Button("HELP", helpButtonStyle, GUILayout.MaxWidth(60)))
                {
                    _menuState = MenuStates.Help;
                    OnHelpClicked();
                   
                }
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("LOGOUT", logoutButtonStyle, GUILayout.MaxWidth(55)))
            {
                _menuState = MenuStates.Logout;
                OnLogoutClicked();
            }

            GUILayout.EndHorizontal();
        }



        public static void OnDataClicked()
        {
           // DisableActivePanels(MenuStates.Data);

            //Debug.Log("Data Clicked");
            //PlayFabEditorDataMenu.OnEnable();
            PlayFabEditor.RaiseStateUpdate(PlayFabEditor.EdExStates.OnMenuItemClicked, MenuStates.Data.ToString());

            EditorPrefs.SetInt("PLAYFAB_CURRENT_MENU",(int)MenuStates.Data);

        }

        public static void OnHelpClicked()
        {
            //DisableActivePanels(MenuStates.Help);

           // Debug.Log("Help Clicked");

            PlayFabEditor.RaiseStateUpdate(PlayFabEditor.EdExStates.OnMenuItemClicked, MenuStates.Help.ToString());
            EditorPrefs.SetInt("PLAYFAB_CURRENT_MENU",(int)MenuStates.Help);

        }

        public static void OnServicesClicked()
        {
           // DisableActivePanels(MenuStates.Services);

            //Debug.Log("Services Clicked");
            PlayFabEditor.RaiseStateUpdate(PlayFabEditor.EdExStates.OnMenuItemClicked, MenuStates.Services.ToString());
            EditorPrefs.SetInt("PLAYFAB_CURRENT_MENU",(int)MenuStates.Services);
        }

        public static void OnSdKsClicked()
        {
           // DisableActivePanels(MenuStates.Sdks);

            //Debug.Log("SDKS Clicked");
            PlayFabEditor.RaiseStateUpdate(PlayFabEditor.EdExStates.OnMenuItemClicked, MenuStates.Sdks.ToString());
            EditorPrefs.SetInt("PLAYFAB_CURRENT_MENU", (int)MenuStates.Sdks);
        }

        public static void OnSettingsClicked()
        {
            //DisableActivePanels(MenuStates.Settings);

            //Debug.Log("Settings Clicked");
            PlayFabEditor.RaiseStateUpdate(PlayFabEditor.EdExStates.OnMenuItemClicked, MenuStates.Settings.ToString());
            EditorPrefs.SetInt("PLAYFAB_CURRENT_MENU", (int)MenuStates.Settings);
        }

        public static void OnLogoutClicked()
        {
           // DisableActivePanels(MenuStates.Logout);

            //Debug.Log("Logout Clicked");
            PlayFabEditor.RaiseStateUpdate(PlayFabEditor.EdExStates.OnMenuItemClicked, MenuStates.Logout.ToString());

            PlayFabEditorAuthenticate.Logout();


            _menuState = MenuStates.Sdks;
            EditorPrefs.SetInt("PLAYFAB_CURRENT_MENU", (int)MenuStates.Sdks);
        }


//        internal static void DisableActivePanels(MenuStates active)
//        {
//            if(active != MenuStates.Data)
//            {
//                PlayFabEditorDataMenu.OnDisable();
//            }
//        }

    }
}