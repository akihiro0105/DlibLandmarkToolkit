#!/usr/bin/python
# coding: utf-8

import sys
import os
import glob
import dlib

# 対象フォルダの画像の特徴点認識用xmlのひな形を出力
# trainDetect.pyで作成した物体認識結果を利用
# 認識したい特徴点を番号で指定<mark>
#  ./landmarkXml.py <svmファイル> <対象フォルダ> > landmark.xml

print("<?xml version=\'1.0\' encoding=\'ISO-8859-1\'?>")
print("<?xml-stylesheet type=\'text/xsl\' href=\'image_metadata_stylesheet.xsl\'?>")
print("<dataset>")
print("<name>landmark</name>")
print("<comment></comment>")
print("<images>")

detector = dlib.simple_object_detector(sys.argv[1])

mark=[0,1,2,3,4,5,6,7]

for f in glob.glob(os.path.join(sys.argv[2], "*.jpg")):
        img = dlib.load_rgb_image(f)
        dets = detector(img, 1)
        if len(dets)>0:
            # 物体検出
            d=dets[0]
            # xml出力
            print("\t<image file=\'"+"{}".format(os.path.relpath(f,os.path.join(sys.argv[2],"../")))+"\'>")
            print("\t\t<box top=\'{}\' left=\'{}\' width=\'{}\' height=\'{}\'>".format(d.top(),d.left(),d.width(),d.height()))
            for p in mark:
                print("\t\t\t<part name=\'{0:02d}\'".format(p)+" x=\'{}\' y=\'{}\'/>".format(0,0))
            print("\t\t</box>")
            print("\t</image>")

print("</images>")
print("</dataset>")
