using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Java.Lang.Annotation;

namespace Cobrowse.IO.Android
{
    public partial class CobrowseIO
    {
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
            set => SetCustomData(value);
        }

        internal void SetCustomData(IReadOnlyDictionary<string, string> customData)
        {
            if (customData == null)
            {
                throw new ArgumentNullException(nameof(customData));
            }
            this.CustomJavaData = new Dictionary<string, string>(customData);
        }

        public void CreateSession(CobrowseCallbackDelegate<Java.Lang.Error, Session> @delegate)
        {
            this.CreateSession(new CobrowseCallback<Java.Lang.Error, Session>(@delegate));
        }

        public void GetSession(string idOrCode, CobrowseCallbackDelegate<Java.Lang.Error, Session> @delegate)
        {
            this.GetSession(idOrCode, new CobrowseCallback<Java.Lang.Error, Session>(@delegate));
        }

        /// <summary>
        /// Gets or sets the available capabilities for a session. Different
        /// annotation tools and events will be available during a session
        /// depending on the capabilities you set here. By default all
        /// capabilities supported by the device are enabled.
        /// </summary>
        public string[] Capabilities
        {
            get => GetCapabilities();
            set => SetCapabilities(value);
        }

        /// <summary>
        /// Gets or sets the CSS selectors which will be used to redact content within WebViews.
        /// Any HTML element matching one of the selectors configured here will be redacted and
        /// not visible to your agents.
        /// Defaults to an empty list which means the feature is disabled.
        /// </summary>
        public string[] WebViewRedactedViews
        {
            get => GetCapabilities();
            set => SetCapabilities(value);
        }
    }
}
