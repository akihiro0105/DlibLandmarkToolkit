#!/usr/bin/python
# coding: utf-8

import os
import sys
import glob
import dlib

# xmlファイルから物体検出用svmファイルを学習
# ./trainDetect.py object.xml object.svm

options = dlib.simple_object_detector_training_options()
options.add_left_right_image_flips = True
options.C = 5
options.num_threads = 4
options.be_verbose = True

xml_path = sys.argv[1]
svm_path=sys.argv[2]
dlib.train_simple_object_detector(xml_path, svm_path, options)

print("\nTraining accuracy: {}".format(dlib.test_simple_object_detector(xml_path, svm_path)))
