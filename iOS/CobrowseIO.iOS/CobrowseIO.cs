using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Foundation;

namespace Cobrowse.IO.iOS
{
    public partial class CobrowseIO
    {
        // These values copied directly from CobrowseIO.h
        // https://forums.xamarin.com/discussion/8572/how-do-you-bind-extern-nsstring-const
        public static NSString UserIdKey { get; } = new NSString("user_id");

        public static NSString UserEmailKey { get; } = new NSString("user_email");

        public static NSString UserNameKey { get; } = new NSString("user_name");

        public static NSString DeviceIdKey { get; } = new NSString("device_id");

        public static NSString DeviceNameKey { get; } = new NSString("device_name");

        public static CobrowseIO Instance => GetInstance();

        public IReadOnlyDictionary<string, string> CustomData
        {
            get
            {
                if (this.CustomNSDictionaryData is NSDictionary dictionary)
                {
                    var rvalue = new Dictionary<string, string>();
                    foreach (KeyValuePair<NSObject, NSObject> next in dictionary)
                    {
                        rvalue.Add(next.Key.ToString(), next.Value.ToString());
                    }
                    return rvalue;
                }
                return null;
            }
            set => SetCustomData(value);
        }

        internal void SetCustomData(IReadOnlyDictionary<string, string> customData)
        {
            if (customData == null)
            {
                this.CustomNSDictionaryData = null;
                return;
            }
            NSString[] objects = new NSString[customData.Count];
            NSString[] keys = new NSString[customData.Count];
            int counter = 0;
            foreach (KeyValuePair<string, string> next in customData)
            {
                keys[counter] = new NSString(next.Key);
                objects[counter] = new NSString(next.Value);
                counter++;
            }
            this.CustomNSDictionaryData = NSDictionary<NSString, NSString>.FromObjectsAndKeys(objects, keys, customData.Count);
        }

        public void SetCustomData(NSDictionary<NSString, NSString> customData)
        {
            this.CustomNSDictionaryData = customData;
        }

        public void SetDelegate(CobrowseIODelegate @delegate)
        {
            this.Delegate = @delegate;
        }
    }
}
