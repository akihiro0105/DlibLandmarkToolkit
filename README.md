# DlibLandmarkToolkit
dlibで物体認識とlandmark認識を行うためのツールキット

# Environment
- Windows10
    + bash on ubuntu on windows
    + Visual Studio 2017

# Install library
- Xming-6-9-0-31-setup [https://ja.osdn.net/projects/sfnet_xming/](https://ja.osdn.net/projects/sfnet_xming/)
    + mp4動画のlandmarkを確認するために必要

# How to use
[「Tokyo HoloLens ミートアップ vol.11　忘年会スペシャル！」で展示，LTしました](http://akihiro-document.azurewebsites.net/post/event/hololensmeetup20181215/)

# Scripts
- python_scripts

bash ubuntu on windows上のpythonで動作させるpythonスクリプト

    + checkVideo.py (mp4動画のlandmarkを確認できる)
    + detectXml.py (object detect用のxmlファイルを出力)
    + landmarkXml.py (landmark用のxmlファイルを出力)
    + mp42jpg.py (mp4動画をjpgファイルの連番に出力)
    + resize.py (指定フォルダ内のjpgファイルの解像度を変更する)
    + trainDetect.py (object detect用の学習済みモデルデータを作成)
    + trainLandmark.py (landmark用の学習済みモデルデータを作成)

- Viewer

Windows上で動作する指定画像の物体認識枠とlandmark位置の設定が行えるプログラム

    + Viewer.sln
        * Viewer (物体位置とlandmark位置設定用プログラム)
        + ラベルファイル.xml内のパーツ位置を編集するプログラム
        + xmlファイルをdrag&dropで読み込み
        + リストのパーツ選択で位置調整するパーツを選択
        + A,Dキーで画像を切り替え
        + W,Sキーで画像切替速度を変更
        + 位置調整はドラックで調整
        + 設定完了後はSaveボタンで上書き保存

        * ResizeXML (指定landmark用xmlファイルの物体認識枠とlandmark位置のスケールを変更する)
