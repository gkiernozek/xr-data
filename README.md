
# XRData

A data representation example running on Quest 3 in XR mode.

It shows graph of preset function with 10000 of data points.


![Image](https://img.youtube.com/vi/ZLhM_BuFv7c/0.jpg)

## [V2 Video from Quest 3](https://youtube.com/watch/ZLhM_BuFv7c)

![Image](https://img.youtube.com/vi/cbMDsphYAmU/0.jpg)

## [Video from Quest 3](https://www.youtube.com/watch?v=cbMDsphYAmU)


## Brief documentation

This project is a Unity-based application that utilizes the Unity DOTS framework. It includes systems for graph point spawning and interaction, leveraging the Burst compiler for performance optimization.


## Scenes

- `Assets/_Project/Scenes/`
  - `Graph.unity`: Scene with just a graph subscene for faster interactions
  - `XR Graph.unity`: Scene with graph & xr rig

## Key scripts

- `Assets/_Project/Scripts/Graph/Systems/`
  - `PointsSpawnSystem.cs`: Spawning points in the graph basing on GraphConfig data.
  - `GraphingSystem.cs`: Responsible for drawing graph points.
  - `GraphInteractionSystem.cs`: Handling graph interactions basing on XRInteractionPointsData.
- `Assets/_Project/Scripts/Graph/ComponentsAuthoring/`
  - `Coordinates.cs`: XZ coordinates for data points.
  - `GraphConfigAuthoring.cs`: Contains config for whole graph - dimensionSize, spacing, pointPrefab, graphPointsTransform.
  - `XRInteractionPointsData.cs`: Contains array of XRInteractionPoints positions served into ecs by XRInteractionPoints.
- `Assets/_Project/Scripts/Graph/XRInteractionPoints/XRInteractionPoints.cs`: connection between XR and ECS


## Other key files / folders / info

- `Assets/_Project/Assets/Materials` contains axis materials as well as a YShaderGraph used to visually render differen color gradient basing on Point height while maintaining single batch.



#
#
#
# How to reach me:

![Image](https://img.shields.io/badge/linkedin-%230077B5.svg?&style=for-the-badge&logo=linkedin&logoColor=white) 
[LinkedIn](https://www.linkedin.com/in/gkiernozek/)

![Image](https://img.shields.io/badge/gmail-%23D14836.svg?&style=for-the-badge&logo=gmail&logoColor=white) 
[Email](mailto:gkiernozek@gmail.com)


Lots of commit history was on Bitbucket & GitLab :)

[![Made with Unity](https://img.shields.io/badge/Made%20with-Unity-57b9d3.svg?style=for-the-badge&logo=unity)](https://unity3d.com)

💻 🤝 👨‍💻
