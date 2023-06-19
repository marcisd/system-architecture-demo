# System Architecture Demo

Name: Marci San Diego<br>
LinkedIn: [marcisd.github.io](https://marcisd.github.io/) <br/>
Portfolio: [linkedin.com/in/marcisd](https://www.linkedin.com/in/marcisd/)

## Info
The purpose of this repository is to demonstrate some of the libraries that I have made for Unity.

I prefer writing software that follows the microservices architectural pattern. 
The introduction of custom packages in Unity allows us to create loosely-coupled, granular, and reusable subsystems. 

When subsystems are created inside their own assemblies, I find that software becomes easily maintainable, documentable, and testable.
It's easier to establish what its scopes are -- what are the public-facing members versus what information and implementation we keep hidden from its clients.

I also consider core architecture and tooling as my niche. Thus my demo code features packages on that classification.

## Contents

This project contains some sample scenes and some embedded packages.

### Core Variable Module Package

- [Read more about this module here.](https://github.com/marcisd/com.marcisd.core.variables)
- You can find this package inside the `Packages` folder with the display name `MSD • Core • Variables` and the folder name `com.marcisd.core.variables`.

#### Test Scene

The test scene demonstrates the capability of the architecture to define a single source of truth across different references and usages.

- In the main `Assets` folder, open the scene `Samples/VariableUsageSample/VariableUsageSampleScene.unity`.
- Run the scene and play mode. Notice that the blue and red circles automatically scatters around the scene and bounces around.
- Select the asset named `Samples/VariableUsageSample/Assets/Blue Unit Speed.asset`. 
- Modify the field `Value`. Notice what happens to the blue circles.
- Select the asset named `Samples/VariableUsageSample/Assets/Red Unit Speed.asset`. 
- Modify the field `Value`. Notice what happens to the red circles.

![Test Scene A](https://raw.githubusercontent.com/marcisd/com.marcisd.core.variables/main/.readmesrc/1B_60fps.gif)


### Remote Variable Module Package

- [Read more about this module here.](https://github.com/marcisd/com.marcisd.modules.remote-variables)
- You can find this package inside the `Packages` folder with the display name `MSD • Modules • Remote Variables` and the folder name `com.marcisd.modules.remote-variables`.

#### Test Scenes

The test scene demonstrates extending the Core Variable package to utilize Unity's Remote Config. While still using the same `GenericRefernce<T>` in the client class.

- In the main `Assets` folder, open the scene `Samples/RemoteVariableSample/RemoteVariableSampleScene.unity`.
- Run the scene and play mode. Notice that the label of the text automatically changes after a small delay.
- This is because in the system is configured to run automatically on app start in the `MSD/Config/Remote Config Fetcher`.
- Select the `MSD/Config/Remote Config Fetcher` asset to modify the environment. 
- Set the environment to a different value and press `Fetch Remote Config`. This fetches the configs for the specified environment.
- Notice that the label of the text automatically changes after the config is loaded.

![Test Scene B](https://raw.githubusercontent.com/marcisd/com.marcisd.modules.remote-variables/main/.readmesrc/Fig1_15fps.gif)

### Others

- Inside the `Packages` folder you can find another package with the display name `MSD • Core` and the folder name `com.marcisd.core`.
- This was part of a bigger core library. It is only keeping the dependencies necessary to run the demo for now.

## There's more...

### Analytics Event System Package

- [Read more about this module here.](https://github.com/marcisd/com.marcisd.systems.analytics)
- While this package is not part of the demo project, it demonstrates further use cases for the `Core Variable` module.
- While the `Remote Variable` package demonstrates extension of the `Core Variable`, 
this package uses the `Core Variable` module to codelessly assign parameter values to analytics reports.
- This was created in collaboration with game designers and analysts to allow them to create consistent analytics reports for Unity projects.
