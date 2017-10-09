### Overview
This app builds a 3D model from graph data in Unity, which can then be deployed to a Hololens.

### Model
The model is roughly based on SISO and Archimate. Each system is shown as a platform with paths to other systems. On each platform is placed boxes to show:
* OS - Dark green
* System Software - Light green
* Data - Blue
* Business Functions - Yellow

### Graph data
There is a Data folder (not part of the Unity project) that contains a spreadsheet for the nodes and edges of the graph. There is an R script that applies a Fruchterman Reingold algorithm to layout the system nodes. The resultant csv files are embedded as Resources in the Unity app. 

### Features
Voice commands:
* Spin - rotates the entire model
* Stop - stops the rotation

Gaze and Gestures
* Cursor appears on objects
* Tap gesture selects system node
* Selected system node is highlighted and more info is displayed in a UI panel

Labels
* Each system node has a label

### Software
* Windows 10 Pro 64-bit 
* Unity 2017.1.1f1 (64-bit)
* Hololens Emulator

A setup guide can be found here:
https://developer.microsoft.com/en-us/windows/mixed-reality/install_the_tools

### Build Instructions
Create a folder called App to build the Unity app for hololens. The App folder is set to be ignored by Git. 

Open the App in Visual Studio and build and deploy to the Hololens device or emulator.

### To Do
* Add search functionality (ideally using voice)
* Use more decriptive prefabs for the objects
* Label each object (partially done)
* Expand metamodel (eg application boxes)
* Add halo effect on found or selected objects (partially done)
* Add gaze and gestures to select objects (partially done)
* Display more information about a selected object (partially done)
* Add spatial mapping
* Improve object placement for Hololens
