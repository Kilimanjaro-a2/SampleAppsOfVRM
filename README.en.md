# VRM Sample Collection

[日本語版README](https://github.com/Kilimanjaro-a2/SampleAppsOfVRM/blob/master/README.md)

## Description

This is sample collection of apps using VRM.

It is supposed to be built for WebGL by Unity.

## Demo
This is a demo built with WebGL.

https://kilimanjaro-a2.github.io/SampleAppsOfVRM/

## Usage

Download the project and open it in Unity Editor.

Then download the latest UniVRM unitypackage from https://github.com/vrm-c/UniVRM/releases and import it into your project.

The project uses Unity Editor version 2019.3.9f1, but you can use any version you like.


In the project, we have prepared 6 scenes that are expected to be used in apps using VRM.

1. Scene to load VRM from file dialog
![ss1](https://github.com/Kilimanjaro-a2/SampleAppsOfVRM/blob/master/ScreenShots/ss1.PNG)

2. Scene to load multiple VRMs from file dialog
![ss2](https://github.com/Kilimanjaro-a2/SampleAppsOfVRM/blob/master/ScreenShots/ss2.PNG)

3. Scene to change the facial expression of the model
![ss3](https://github.com/Kilimanjaro-a2/SampleAppsOfVRM/blob/master/ScreenShots/ss3.PNG)

4. Scene to change the poses of the model
![ss4](https://github.com/Kilimanjaro-a2/SampleAppsOfVRM/blob/master/ScreenShots/ss4.PNG)

5. Scene to view meta data
![ss5](https://github.com/Kilimanjaro-a2/SampleAppsOfVRM/blob/master/ScreenShots/ss5.PNG)

6. Simple Game
![ss6](https://github.com/Kilimanjaro-a2/SampleAppsOfVRM/blob/master/ScreenShots/ss6.PNG)


# Others

## Assets

The VRM used in the project is output from VRoid Studio.

https://vroid.com/studio

## Tips of the WebGL builds

- VRM loading does not work!


If the name of the GameObject specified as the call destination of the callback after opening the dialog is different, it will not work properly.

Make sure a GameObject with the name specified in `FileImporterPlugin.jslib` exists in the scene and VRMLoadManager is attached to it.


In the sample project, the method named `FileSelected ()` is called for all the scripts attached to the GameObject named "VRMLoader" in the scene.

- Why are you calling `FileImporterCaptureClick ()` on PointerDown of EventTrigger instead of OnClick of Button?

In `FileImporterCaptureClick ()`, a handler for the Click event is generated and immediately fired.

If you do this when the Click fires, the event will not be handled until the next Click.

In order to avoid this phenomenon, the Pointer Down event, which is processed before the Click event, calls `FileImporterCaptureClick ()`.


- I want to load VRM asynchronously

The WebGL build of Unity does not support multi-threading, so
`VRMImporterContext.LoadAsync (`) doesn't seem to be usable.


Asynchronous loading can be supported by rewriting the script so that it does not use multithreading, but performance will be worse.


This project only supports synchronous loading.