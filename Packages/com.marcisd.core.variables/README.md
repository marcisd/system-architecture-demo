# MSD • Core • Variables

[![Made with Unity](https://img.shields.io/badge/Made%20with-Unity-57b9d3.svg?style=flat&logo=unity)](https://unity3d.com)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/618166d224934d9d92b65f6fe0d8d883)](https://app.codacy.com/gh/marcisd/com.marcisd.core.variables/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

This module provides a modular variable system that utilizes `ScriptableObjects`.

## About

The architecture draws inspiration from Ryan Hipple's talk in Unite Austin 2017 titled "Game Architecture with Scriptable Objects" (video link below). While the usage sticks with the original design and implementation, this module focuses on making the architecture extensible. 

[![Watch the video](https://img.youtube.com/vi/raQ3iHhE_Kk/hqdefault.jpg)](https://youtu.be/raQ3iHhE_Kk?t=926)

## Usage

### Single Source of Truth

Share data across different systems without creating unnecessary dependency.

![Figure 1A](.readmesrc/1A.jpg)

Reference a single source of data and keep clients updated of changes in real time.

![Figure 1B](.readmesrc/1B_60fps.gif)

### Value as a Strategy

Keep the context independent of how the value is obtained. Extend, modify, or add new algorithms without changing the code of the context. 

![Figure 2](.readmesrc/2.jpg)

## Installation

This is a custom package for Unity Package Manager.

* [Installing custom package from a Git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html)
* [Git dependencies](https://docs.unity3d.com/Manual/upm-git.html)
