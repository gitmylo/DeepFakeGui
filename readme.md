# Requirements
some things are not automatically installed, you will need to install them yourself
* python (from the python.org website, not windows store)
* pytorch (run the following command): `py -m pip install torch torchvision torchaudio --extra-index-url https://download.pytorch.org/whl/cu113`
* [microsoft visual c++ 2017 sdk](https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=BuildTools&rel=15)

# Fixes to known issues
* It creates a result.mp4 but doesn't create a new file in the output folder
    * make sure there are no spaces in the path to the exe
