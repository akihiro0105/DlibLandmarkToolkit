#!/usr/bin/python
# coding: utf-8

import sys
import os
import glob

# 対象フォルダの画像の物体検出用xmlのひな形を出力
#  ./detectXml.py <対象フォルダ> > object.xml

print("<?xml version=\'1.0\' encoding=\'ISO-8859-1\'?>")
print("<?xml-stylesheet type=\'text/xsl\' href=\'image_metadata_stylesheet.xsl\'?>")
print("<dataset>")
print("<name>landmark</name>")
print("<comment></comment>")
print("<images>")

for f in glob.glob(os.path.join(sys.argv[1], "*.jpg")):
    print("\t<image file=\'"+"{}".format(os.path.relpath(f,os.path.join(sys.argv[1],"../")))+"\'>")
    print("\t\t<box top=\'{}\' left=\'{}\' width=\'{}\' height=\'{}\'>".format(10,10,10,10))
    print("\t\t</box>")
    print("\t</image>")

print("</images>")
print("</dataset>")
