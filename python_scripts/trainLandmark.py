#!/usr/bin/python
# coding: utf-8

import os
import sys
import glob
import dlib

# xmlファイルから顔器官認識用datファイルを学習
# ./trainLandmark.py landmark.xml landmark.dat

options = dlib.shape_predictor_training_options()
options.oversampling_amount = 300
options.nu = 0.05
options.tree_depth = 2
options.be_verbose = True

xml_path = sys.argv[1]
dat_path=sys.argv[2]
dlib.train_shape_predictor(xml_path, dat_path, options)

print("\nTraining accuracy: {}".format(dlib.test_shape_predictor(xml_path, dat_path)))
