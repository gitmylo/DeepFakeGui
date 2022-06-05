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

# Fixes to known issues
* It creates a result.mp4 but doesn't create a new file in the output folder
   * make sure there are no spaces in the path to the exe
* I'm getting an error when i press the Process button
   * make sure you have both a video and an image selected
