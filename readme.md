# Introduction
DeepFakeGui is a gui for the [first order motion model](https://github.com/AliaksandrSiarohin/first-order-model)
it uses ffmpeg to prepare images automatically, and adds the audio back to the video, using ffmpeg.

make sure you have a dataset from the original project from [Google drive](https://drive.google.com/drive/folders/1PyQJmkdCsAkOYwUyaj_l-l0as-iLDgeH) or [Yandex](https://disk.yandex.ru/d/lEw8uRm140L_eQ), i recommend vox-adv-cpk, which is for facial animation (from [original repo](https://github.com/AliaksandrSiarohin/first-order-model))

FOM has some issues on newer version of python, this gui aims to fix that by patching the files with simple changes to make it compatible. there will be warnings, but errors should be gone from the training.

# Requirements
some things are not automatically installed, you will need to install them yourself
* python (from the python.org website, not windows store)
* pytorch (run the following command): `py -m pip install torch torchvision torchaudio --extra-index-url https://download.pytorch.org/whl/cu113`
* [microsoft visual c++ 2017 sdk](https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=BuildTools&rel=15)

   ![Desktop environment with c++](/visual-c++.png)
   
# Gui:
   ![Gui](/gui.png)
* press `f1` to open the output folder automatically
* image preview
* load image
   * lets you open an image, you can also open a video or gif image, to get the first frame
* load video
   * lets you open a video, this will be for the motion, it's best when it's a video fitting the dataset
   * with voxceleb that would be someone facing the camera
* absolute position
   * turn this on if you want the face to be mapped onto the original video (good with an image with greenscreen background, for overlapping a deepfake)
   * turn this off if you want it to be based off of the image instead (good for quick videos)
* resolution
   * just keep this at 512, the ai is made for 512x512 pixel images, so changing this might cause issues, i still put it in just in case you want to try
* config/dataset
   * type the name of the dataset and config here, for `vox-adv-cpk.pth.tar` you should put `vox-adv`
* process
   * set up the files for the ai, then run the ai, then add the audio back on and put it in the output folder (open with `f1`)

# Fixes to known issues
* It creates a result.mp4 but doesn't create a new file in the output folder
   * make sure there are no spaces in the path to the exe
* I'm getting an error when i press the Process button
   * make sure you have both a video and an image selected
