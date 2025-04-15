﻿using System;
using Android.Runtime;

namespace Cobrowse.IO.Android
{
    public partial class Session
    {
        public void Activate(CobrowseCallbackDelegate<Java.Lang.Error, Session> @delegate)
        {
            this.Activate(new CobrowseCallback<Java.Lang.Error, Session>(@delegate));
        }

        public void End(CobrowseCallbackDelegate<Java.Lang.Error, Session> @delegate)
        {
            this.End(new CobrowseCallback<Java.Lang.Error, Session>(@delegate));
        }

        #region Full device

        [GeneratedEnum]
        public Cobrowse.IO.Android.FullDeviceState FullDevice
        {
            get
            {
                return this._FullDevice().ToManagedEnum();
            }
        }

        public void SetFullDevice([GeneratedEnum] Cobrowse.IO.Android.FullDeviceState state, ICallback callback)
        {
            Cobrowse.IO.Android.Session.FullDeviceStateJava javaState = state.ToJavaEnum();
            this._SetFullDevice(javaState, callback);
        }

        public void SetFullDevice([GeneratedEnum] Cobrowse.IO.Android.FullDeviceState state, CobrowseCallbackDelegate<Java.Lang.Error, Session> @delegate)
        {
            Cobrowse.IO.Android.Session.FullDeviceStateJava javaState = state.ToJavaEnum();
            this._SetFullDevice(javaState, new CobrowseCallback<Java.Lang.Error, Session>(@delegate));
        }

        #endregion

        #region Capabilities

        public void SetCapabilities(string[] capabilities, CobrowseCallbackDelegate<Java.Lang.Error, Session> @delegate)
        {
            this._SetCapabilities(capabilities, new CobrowseCallback<Java.Lang.Error, Session>(@delegate));
        }

        #endregion

        #region Remote control

        [GeneratedEnum]
        public Cobrowse.IO.Android.RemoteControlState RemoteControl
        {
            get
            {
                return this._RemoteControl().ToManagedEnum();
            }
        }

        public void SetRemoteControl([GeneratedEnum] Cobrowse.IO.Android.RemoteControlState state, ICallback callback)
        {
            Cobrowse.IO.Android.Session.RemoteControlState javaState = state.ToJavaEnum();
            this._SetRemoteControl(javaState, callback);
        }

        public void SetRemoteControl([GeneratedEnum] Cobrowse.IO.Android.RemoteControlState state, CobrowseCallbackDelegate<Java.Lang.Error, Session> @delegate)
        {
            Cobrowse.IO.Android.Session.RemoteControlState javaState = state.ToJavaEnum();
            this._SetRemoteControl(javaState, new CobrowseCallback<Java.Lang.Error, Session>(@delegate));
        }

        #endregion
    }
}
