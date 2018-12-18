# DlibLandmarkToolkit
dlibで物体認識とlandmark認識を行うためのツールキット

# Environment
- Windows10
- bash on ubuntu on windows

# Install library
- Xming-6-9-0-31-setup [https://ja.osdn.net/projects/sfnet_xming/](https://ja.osdn.net/projects/sfnet_xming/)
    + mp4動画のlandmarkを確認するために必要

# How to use
[ブログのurl](https://link)

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
        * ResizeXML (指定landmark用xmlファイルの物体認識枠とlandmark位置のスケールを変更する)
