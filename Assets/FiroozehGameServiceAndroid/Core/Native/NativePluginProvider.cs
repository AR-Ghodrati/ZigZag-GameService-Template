// <copyright file="NativePluginProvider.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2019 Firoozeh Technology LTD. All Rights Reserved.
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>



using UnityEngine;

/**
* @author Alireza Ghodrati
*/

namespace FiroozehGameServiceAndroid.Core.Native
{
#if UNITY_ANDROID
    /// <summary>
    /// Represents Game Service Plugin Provider In Native Mode
    /// ATTENTION : DO NOT CHANGE THIS CODE ANY WAY
    /// </summary>
    public static class NativePluginProvider
    {
        public static AndroidJavaObject GetGameService()
        {
            var unityInstanter = new AndroidJavaClass("ir.firoozehcorp.gameservice.android.unity.Native.Handlers.UnityGameServiceNative");
            return unityInstanter.CallStatic<AndroidJavaObject>("GetInstance");
        }

        public static AndroidJavaObject GetDownloadHandler()
        {
            var handler = new AndroidJavaClass("ir.firoozehcorp.gameservice.android.unity.Native.Handlers.DownloadHandler");
            return handler.CallStatic<AndroidJavaObject>("GetInstance");
        }
    
        public static AndroidJavaObject GetUnityActivity()
        {
            var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            return unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }
       
    }
#endif
}