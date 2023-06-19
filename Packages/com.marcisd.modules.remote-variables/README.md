# MSD • Modules • Remote Variables

[![Made with Unity](https://img.shields.io/badge/Made%20with-Unity-57b9d3.svg?style=flat&logo=unity)](https://unity3d.com)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/f11d0ae45cdb45b28c7d31d72827f986)](https://app.codacy.com/gh/marcisd/com.marcisd.modules.remote-variables/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

This module extends the [Core Variable Module](https://github.com/marcisd/com.marcisd.core.variables) to create `ScriptableObject` variables that get their value using [Unity's Remote Config service](https://unity.com/products/remote-config).

## Usage

Use the power of [Unity's Remote Config service](https://unity.com/products/remote-config) to update content in real-time without changing the code or rebuilding the app.

![Figure 1A](.readmesrc/Fig1_15fps.gif)

## Installation

This is a custom package for Unity Package Manager.

* [Installing custom package from a Git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html)
* [Git dependencies](https://docs.unity3d.com/Manual/upm-git.html)

### Setting-up Remote Config

[Learn How to Integrate Remote Config](https://docs.unity3d.com/Packages/com.unity.remote-config@3.3/manual/index.html)

### Setup

Open the `Remote Config Fetcher` from the menu `MSD/Config/Remote Config Fetcher`. The user will be presented with options for controlling the setup for the `Remote Config`.