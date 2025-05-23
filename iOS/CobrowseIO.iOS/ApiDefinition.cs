﻿using System;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;
using ReplayKit;

namespace Cobrowse.IO.iOS
{
    // @interface CBIOAgent : NSObject
    [BaseType(typeof(NSObject), Name = "CBIOAgent")]
    interface Agent
    {
        // @property (nullable) NSString* email;
        [Export("email")]
        string Email { get; }

        // @property NSString * _Nonnull name;
        [Export("name")]
        string Name { get; }

        // @property NSString * _Nonnull id;
        [Export("id")]
        string Id { get; }

        // +(instancetype _Nullable)from:(NSDictionary * _Nonnull)dict;
        [Static]
        [Export("from:")]
        [return: NullAllowed]
        Agent From(NSDictionary dict);
    }

    // @interface CBIORESTResource : NSObject
    [BaseType(typeof(NSObject), Name = "CBIORESTResource")]
    interface RESTResource
    {
        // -(NSString * _Nullable)id;
        [NullAllowed, Export("id")]
        string Id { get; }
    }

    // @interface CBIODevice : CBIORESTResource
    [BaseType(typeof(RESTResource), Name = "CBIODevice")]
    interface Device
    {
        // @property NSData * _Nullable token;
        [NullAllowed, Export("token", ArgumentSemantic.Assign)]
        NSData Token { get; set; }

        // -(NSString * _Nonnull)id;
        [Export("id")]
        string Id { get; }
    }

    // @interface CBIOKeyPress : NSObject
    [BaseType(typeof(NSObject), Name = "CBIOKeyPress")]
    interface KeyPress
    {
        // @property (readonly) NSString * _Nonnull key;
        [Export("key")]
        string Key { get; }

        // @property (readonly) NSString * _Nonnull code;
        [Export("code")]
        string Code { get; }

        // @property (readonly) NSString * _Nonnull state;
        [Export("state")]
        string State { get; }
    }

    // @protocol CBIOResponder <NSObject>
    /*
	  Check whether adding [Model] to this declaration is appropriate.
	  [Model] is used to generate a C# class that implements this protocol,
	  and might be useful for protocols that consumers are supposed to implement,
	  since consumers can subclass the generated class instead of implementing
	  the generated interface. If consumers are not supposed to implement this
	  protocol, then [Model] is redundant and will generate code that will never
	  be used.
	*/
    [Protocol, Model]
    [BaseType(typeof(NSObject), Name = "CBIOResponder")]
    interface Responder
    {
        // @optional -(void)cobrowseTouchesBegan:(NSSet<CBIOTouch *> * _Nonnull)touches withEvent:(CBIOTouchEvent * _Nonnull)event;
        [Export("cobrowseTouchesBegan:withEvent:")]
        void CobrowseTouchesBegan(NSSet<Touch> touches, TouchEvent @event);

        // @optional -(void)cobrowseTouchesMoved:(NSSet<CBIOTouch *> * _Nonnull)touches withEvent:(CBIOTouchEvent * _Nonnull)event;
        [Export("cobrowseTouchesMoved:withEvent:")]
        void CobrowseTouchesMoved(NSSet<Touch> touches, TouchEvent @event);

        // @optional -(void)cobrowseTouchesEnded:(NSSet<CBIOTouch *> * _Nonnull)touches withEvent:(CBIOTouchEvent * _Nonnull)event;
        [Export("cobrowseTouchesEnded:withEvent:")]
        void CobrowseTouchesEnded(NSSet<Touch> touches, TouchEvent @event);

        // @optional -(void)cobrowseTouchesCancelled:(NSSet<CBIOTouch *> * _Nonnull)touches withEvent:(CBIOTouchEvent * _Nonnull)event;
        [Export("cobrowseTouchesCancelled:withEvent:")]
        void CobrowseTouchesCancelled(NSSet<Touch> touches, TouchEvent @event);

        // @optional -(void)cobrowseKeyDown:(CBIOKeyPress * _Nonnull)event;
        [Export("cobrowseKeyDown:")]
        void CobrowseKeyDown(KeyPress @event);
    }

    // TODO remove CB prefix
    // typedef const void (^CBErrorSessionBlock)(NSError * _Nullable, CBIOSession * _Nullable);
    delegate void CBErrorSessionBlock([NullAllowed] NSError error, [NullAllowed] Session session);

    // @interface CBIOSession : CBIORESTResource
    [BaseType(typeof(RESTResource), Name = "CBIOSession")]
    interface Session
    {
        // -(_Bool)isPending;
        [Export("isPending")]
        bool IsPending { get; }

        // -(_Bool)isAuthorizing;
        [Export("isAuthorizing")]
        bool IsAuthorizing { get; }

        // -(_Bool)hasAgent;
        [Export("hasAgent")]
        bool HasAgent { get; }

        // -(_Bool)isActive;
        [Export("isActive")]
        bool IsActive { get; }

        // -(_Bool)isEnded;
        [Export("isEnded")]
        bool IsEnded { get; }

        // -(void)fetch:(CBErrorSessionBlock _Nullable)callback;
        [Export("fetch:")]
        void Fetch([NullAllowed] CBErrorSessionBlock callback);

        // -(void)activate:(CBErrorSessionBlock _Nullable)callback;
        [Export("activate:")]
        void Activate([NullAllowed] CBErrorSessionBlock callback);

        // -(void)end:(CBErrorSessionBlock _Nullable)callback;
        [Export("end:")]
        void End([NullAllowed] CBErrorSessionBlock callback);

        // -(NSString * _Nullable)code;
        [NullAllowed, Export("code")]
        string Code { get; }

        // -(NSString * _Nonnull)state;
        [Export("state")]
        string State { get; }

        // -(CBIOAgent * _Nullable)agent;
        [NullAllowed, Export("agent")]
        Agent Agent { get; }

        // -(CBIOFullDeviceState) fullDevice;
        [Export("fullDevice")]
        FullDeviceState FullDevice { get; }

        // -(void)setFullDeviceState:(CBIOFullDeviceState)state callback:(CBErrorSessionBlock _Nullable) callback;
        [Export("setFullDevice:callback:")]
        void SetFullDevice(FullDeviceState state, [NullAllowed] CBErrorSessionBlock callback);

        // -(CBIORemoteControlState)remoteControl;
        [Export("remoteControl")]
        RemoteControlState RemoteControl { get; }

        // -(void)setRemoteControl:(CBIORemoteControlState)state callback:(CBErrorSessionBlock _Nullable)callback;
        [Export("setRemoteControl:callback:")]
        void SetRemoteControl(RemoteControlState state, [NullAllowed] CBErrorSessionBlock callback);

        // -(void) setCapabilities: ( NSArray<NSString*>* _Nonnull) capabilities callback: (nullable CBErrorSessionBlock) callback;
        [Export("setCapabilities:callback:")]
        void SetCapabilities(string[] capabilities, [NullAllowed] CBErrorSessionBlock callback);
    }

    // @interface CBIOTouch : NSObject
    [BaseType(typeof(NSObject), Name = "CBIOTouch")]
    interface Touch : INativeObject
    {
        // @property (readonly) UIView * _Nonnull target;
        [Export("target")]
        UIView Target { get; }

        // -(CGPoint)position;
        [Export("position")]
        CGPoint Position { get; }
    }

    // @interface CBIOTouchEvent : NSObject
    [BaseType(typeof(NSObject), Name = "CBIOTouchEvent")]
    interface TouchEvent
    {
        // @property (readonly) CGPoint position;
        [Export("position")]
        CGPoint Position { get; }

        // @property (readonly) NSDate * _Nonnull timestamp;
        [Export("timestamp")]
        NSDate Timestamp { get; }
    }

    // @protocol CobrowseIODelegate <NSObject>
    [Protocol, Model/*(AutoGeneratedName = true): AutoGeneratedName was just made the default/only behavior in net6*/]
    [BaseType(typeof(NSObject))]
    interface CobrowseIODelegate
    {
        // @required -(void)cobrowseSessionDidUpdate:(CBIOSession * _Nonnull)session;
        [Abstract]
        [Export("cobrowseSessionDidUpdate:")]
        void SessionDidUpdate(Session session);

        // @required -(void)cobrowseSessionDidEnd:(CBIOSession * _Nonnull)session;
        [Abstract]
        [Export("cobrowseSessionDidEnd:")]
        void SessionDidEnd(Session session);

        // @optional -(void)cobrowseSessionDidLoad:(CBIOSession * _Nonnull)session;
        [Export("cobrowseSessionDidLoad:")]
        void SessionDidLoad(Session session);

        // @optional -(_Bool)cobrowseShouldAllowTouchEvent:(CBIOTouchEvent * _Nonnull)touchEvent forSession:(CBIOSession * _Nonnull)session;
        [Export("cobrowseShouldAllowTouchEvent:forSession:")]
        bool ShouldAllowTouchEvent(TouchEvent touchEvent, Session session);

        // @optional -(_Bool)cobrowseShouldAllowKeyEvent:(CBIOKeyPress * _Nonnull)keyEvent forSession:(CBIOSession * _Nonnull)session;
        [Export("cobrowseShouldAllowKeyEvent:forSession:")]
        bool ShouldAllowKeyEvent(KeyPress keyEvent, Session session);

        // @optional -(_Bool)cobrowseShouldCaptureWindow:(UIWindow * _Nonnull)window;
        [Export("cobrowseShouldCaptureWindow:")]
        bool ShouldCaptureWindow(UIWindow window);

        // @optional -(void)cobrowseHandleSessionRequest:(CBIOSession * _Nonnull)session;
        [Export("cobrowseHandleSessionRequest:")]
        void HandleSessionRequest(Session session);

        // @optional -(void)cobrowseHandleRemoteControlRequest:(CBIOSession * _Nonnull)session;
        [Export("cobrowseHandleRemoteControlRequest:")]
        void HandleRemoteControlRequest(Session session);

        // @optional -(void) cobrowseHandleFullDeviceRequest: (CBIOSession * _Nonnull) session;
        [Export("cobrowseHandleFullDeviceRequest:")]
        void HandleFullDeviceRequest(Session session);

        // @optional -(void)cobrowseShowSessionControls:(CBIOSession * _Nonnull)session;
        [Export("cobrowseShowSessionControls:")]
        void ShowSessionControls(Session session);

        // @optional -(void)cobrowseHideSessionControls:(CBIOSession * _Nonnull)session;
        [Export("cobrowseHideSessionControls:")]
        void HideSessionControls(Session session);

        // @optional -(NSArray<UIView *> * _Nonnull)cobrowseRedactedViewsForViewController:(UIViewController * _Nonnull)vc;
        [Export("cobrowseRedactedViewsForViewController:")]
        UIView[] RedactedViewsForViewController(UIViewController vc);

        // @optional -(NSArray<UIView *> * _Nonnull)cobrowseUnredactedViewsForViewController:(UIViewController * _Nonnull)vc __attribute__((availability(ios, introduced=9)));
        [Export("cobrowseUnredactedViewsForViewController:")]
        UIView[] UnredactedViewsForViewController(UIViewController vc);
    }

    // @protocol CobrowseIORedacted <NSObject>
    /*
	  Check whether adding [Model] to this declaration is appropriate.
	  [Model] is used to generate a C# class that implements this protocol,
	  and might be useful for protocols that consumers are supposed to implement,
	  since consumers can subclass the generated class instead of implementing
	  the generated interface. If consumers are not supposed to implement this
	  protocol, then [Model] is redundant and will generate code that will never
	  be used.
	*/
    [Protocol(Name = "CobrowseIORedacted"), Model]
    [BaseType(typeof(NSObject))]
    interface CobrowseIORedacted
    {
        // @required -(NSArray<UIView *> * _Nonnull)redactedViews;
        [Abstract]
        [Export("redactedViews")]
        UIView[] RedactedViews { get; }
    }
    // @protocol CobrowseIOUnredacted <NSObject>
    /*
	  Check whether adding [Model] to this declaration is appropriate.
	  [Model] is used to generate a C# class that implements this protocol,
	  and might be useful for protocols that consumers are supposed to implement,
	  since consumers can subclass the generated class instead of implementing
	  the generated interface. If consumers are not supposed to implement this
	  protocol, then [Model] is redundant and will generate code that will never
	  be used.
	*/
    [Protocol(Name = "CobrowseIOUnredacted"), Model]
    [BaseType(typeof(NSObject))]
    interface CobrowseIOUnredacted
    {
        // @required -(NSArray * _Nonnull)unredactedViews;
        [Abstract]
        [Export("unredactedViews")]
        NSObject[] UnredactedViews { get; }
    }

    // @interface CobrowseIO : NSObject
    [BaseType(typeof(NSObject), Name = "CobrowseIO")]
    interface CobrowseIO
    {
        // @property CBLicense * _Nonnull license;
        [Export("license")]
        string License { get; set; }

        // @property NSString * _Nonnull api;
        [Export("api")]
        string Api { get; set; }

        // @property NSDictionary<NSString *,NSString *> * _Nonnull customData;
        [Export("customData", ArgumentSemantic.Assign)]
        [Internal]
        NSDictionary<NSString, NSString> CustomNSDictionaryData { get; set; }

        // @property NSArray<NSString *> * _Nonnull capabilities;
        [Export("capabilities", ArgumentSemantic.Assign)]
        string[] Capabilities { get; set; }

        // @property _Bool registration;
        [Export("registration", ArgumentSemantic.Assign)]
        bool Registration { get; set; }

        // @property NSArray<NSString *> * _Nonnull capabilities;
        [Export("webviewRedactedViews", ArgumentSemantic.Assign)]
        string[] WebViewRedactedViews { get; set; }

        [Wrap("WeakDelegate")]
        [NullAllowed]
        [Internal]
        CobrowseIODelegate Delegate { get; set; }

        // @property id<CobrowseIODelegate> _Nullable delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Assign)]
        NSObject WeakDelegate { get; set; }

        // @property (readonly) NSString * _Nonnull deviceId;
        [Export("deviceId")]
        string DeviceId { get; }

        // @property (readonly) CBIODevice * _Nonnull device;
        [Export("device")]
        Device Device { get; }

        // +(instancetype _Nonnull)instance;
        [Static]
        [Export("instance")]
        [Internal]
        CobrowseIO GetInstance();

        // -(instancetype _Nonnull)start;
        [Export("start")]
        CobrowseIO Start();

        // -(instancetype _Nonnull)stop;
        [Export("stop")]
        CobrowseIO Stop();

        // -(instancetype _Nonnull)stop:(void (^ _Nonnull)(NSError * _Nullable))callback;
        [Export("stop:")]
        CobrowseIO Stop(Action<NSError> callback);

        // -(instancetype _Nonnull)createSession:(CBErrorSessionBlock _Nullable)callback;
        [Export("createSession:")]
        CobrowseIO CreateSession([NullAllowed] CBErrorSessionBlock callback);

        // -(instancetype _Nonnull)getSession:(NSString * _Nonnull)idOrCode callback:(CBErrorSessionBlock _Nullable)callback;
        [Export("getSession:callback:")]
        CobrowseIO GetSession(string idOrCode, [NullAllowed] CBErrorSessionBlock callback);

        // -(CBIOSession * _Nullable)currentSession;
        [NullAllowed, Export("currentSession")]
        Session CurrentSession { get; }

        // -(_Bool)isStarted;
        [Export("isStarted")]
        bool IsStarted { get; }

        // -(NSString * _Nonnull)version;
        [Export("version")]
        string Version { get; }

        // +(BOOL)isCobrowseNotification:(NSDictionary * _Nonnull)userInfo;
        [Static]
        [Export("isCobrowseNotification:")]
        bool IsCobrowseNotification(NSDictionary userInfo);

        // +(void)onPushNotification:(NSDictionary * _Nonnull)userInfo;
        [Static]
        [Export("onPushNotification:")]
        void OnPushNotification(NSDictionary userInfo);
    }

    // @interface CobrowseIOReplayKitExtension : RPBroadcastSampleHandler
    [BaseType(typeof(RPBroadcastSampleHandler))]
    interface CobrowseIOReplayKitExtension
    {
    }
}
