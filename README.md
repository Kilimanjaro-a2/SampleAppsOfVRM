# VRMサンプルアプリ集


[English version of this file](https://github.com/Kilimanjaro-a2/SampleAppsOfVRM/blob/master/README.en.md)

## Description

VRMを使ったアプリのサンプル集です。

UnityからWebGL向けにビルドすることを想定しています。

## Demo
WebGLでビルドされたデモです。

https://kilimanjaro-a2.github.io/SampleAppsOfVRM/

## Usage
プロジェクトをダウンロードしてUnity Editorで開いてください。

そして、 https://github.com/vrm-c/UniVRM/releases から最新のUniVRMのunitypackageをダウンロードして、プロジェクトにインポートしてください。

Unity Editorのバージョンは2019.3.9f1を想定しています。


プロジェクトの中には、VRMを使ったアプリで使用されることが想定される6つのシーンを用意してあります

1. VRMをファイルダイアログから読み込むシーン
![ss1](https://github.com/Kilimanjaro-a2/SampleAppsOfVRM/blob/master/ScreenShots/ss1.PNG)

2. VRMをファイルダイアログから読み込むシーン（複数に対応）
![ss2](https://github.com/Kilimanjaro-a2/SampleAppsOfVRM/blob/master/ScreenShots/ss2.PNG)

3. 表情を変更するシーン
![ss3](https://github.com/Kilimanjaro-a2/SampleAppsOfVRM/blob/master/ScreenShots/ss3.PNG)

4. ポーズ・アニメーションを変更するシーン
![ss4](https://github.com/Kilimanjaro-a2/SampleAppsOfVRM/blob/master/ScreenShots/ss4.PNG)

5. メタ情報を表示するシーン
![ss5](https://github.com/Kilimanjaro-a2/SampleAppsOfVRM/blob/master/ScreenShots/ss5.PNG)

6. シンプルなゲーム
![ss6](https://github.com/Kilimanjaro-a2/SampleAppsOfVRM/blob/master/ScreenShots/ss6.PNG)

# Others

## 使用したアセット

プロジェクト内で使用しているVRMはVRoid Studioより出力したものです。

https://vroid.com/studio


## WebGLビルドで詰まりがちな部分

- VRMの読み込みがうまくいかない！

ダイアログを開いてからコールバックを呼び出す先として指定しているGameObjectの名前が異なっていると、うまく動作しません。

`FileImporterPlugin.jslib`で指定されている名前のついたGameObjectがシーン上に存在し、それにVRMLoadManagerがアタッチされていることを確認してください。

サンプルプロジェクトの中では、シーン上の"VRMLoader"というGameObjectにアタッチされているスクリプトすべてに対して、`FileSelected()`という名前のメソッドを呼び出すようになっています。

- 何故ButtonのOnClickではなくEventTriggerのPointerDownで`FileImporterCaptureClick()`を呼んでいるの？

`FileImporterCaptureClick()`内ではClickイベントに対するハンドラを生成してすぐに発火させる処理を行っています。

これをClickの発火時に行ってしまうと次のClick時までイベントがハンドルされないからです。

その現象を避けるため、Clickイベントよりも先に処理されるPointer Downイベントに`FileImporterCaptureClick()`を呼ぶ処理を行わせています。

- ビルドしたときに文字が表示されない！

UnityのデフォルトフォントであるArialなどは、日本語に対応していません。

日本語に対応しているフォントを使用しましょう。

- 非同期でVRMを読み込みたい

UnityのWebGLビルドは、マルチスレッディングに対応していないので、
`VRMImporterContext.LoadAsync(`)は使用できなさそうです。

マルチスレッディングを使用しないようスクリプトを書き換えることで、非同期読み込みに対応できますが、パフォーマンスは悪くなります。

このプロジェクトでは同期的な読み込みのみ対応しています。