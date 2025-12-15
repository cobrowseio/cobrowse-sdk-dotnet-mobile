using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        #region Custom data

        public IReadOnlyDictionary<string, string> CustomData
        {
            get
            {
                if (this.CustomJavaData is Dictionary<string, Java.Lang.String> dictionary)
                {
                    var rvalue = new Dictionary<string, string>();
                    foreach (KeyValuePair<string, Java.Lang.String> next in dictionary)
                    {
                        rvalue.Add(next.Key, next.Value.ToString());
                    }
                    return rvalue;
                }
                if (this.CustomJavaData is IDictionary<string, string>)
                {
                    return new ReadOnlyDictionary<string, string>(this.CustomJavaData);
                }
                return null;
            }
        }

        public void SetCustomData(
            IReadOnlyDictionary<string, string> customData, ICallback callback)
        {
            if (customData == null)
            {
                throw new ArgumentNullException(nameof(customData));
            }
            this.SetCustomJavaData(new Dictionary<string, string>(customData), callback);
        }

        public void SetCustomData(
            IReadOnlyDictionary<string, string> customData,
            CobrowseCallbackDelegate<Java.Lang.Error, Session> callback)
        {
            if (customData == null)
            {
                throw new ArgumentNullException(nameof(customData));
            }
            this.SetCustomJavaData(new Dictionary<string, string>(customData), new CobrowseCallback<Java.Lang.Error, Session>(callback));
        }

        #endregion

        #region Ended reason

        [GeneratedEnum]
        public Cobrowse.IO.Android.SessionEndedReason EndedReason
        {
            get
            {
                return this._EndedReason().ToManagedEnum();
            }
        }

        #endregion
    }
}
