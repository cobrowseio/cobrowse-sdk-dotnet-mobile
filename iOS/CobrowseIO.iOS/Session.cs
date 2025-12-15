using System;
using Foundation;
using ObjCRuntime;

namespace Cobrowse.IO.iOS;

public partial class Session
{
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
    }

    public void SetCustomData(IReadOnlyDictionary<string, string> customData, CBErrorSessionBlock callback)
    {
        if (customData == null)
        {
            this.SetCustomNSDictionaryData(null, callback);
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
        this.SetCustomNSDictionaryData(
            NSDictionary<NSString, NSString>.FromObjectsAndKeys(objects, keys, customData.Count),
            callback);
    }
}
