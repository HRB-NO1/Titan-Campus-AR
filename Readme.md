# CampusGuide AR

<div style="display: flex; flex-direction: row; align-items: center; justify-content: center;">
  <img src="Resource/Screenshot_20231119_192620_Gallery.jpg" width="300" style="margin-right: 10px;"/>
  <img src="Resource/Screenshot_20231119_192639_Gallery.jpg" width="300" style="margin-right: 10px;"/>
  <img src="Resource/Screenshot_20231119_192727_Gallery.jpg" width="300" style="margin-right: 10px;"/>
</div>


### Note
For optimal utilization within the Unity editor, it is recommended to commence with the Menu Scene as the initial point of interaction.

### Requirements for Developing
* A computing system operating on Windows or macOS platforms.
* Compatibility with Unity version 2021.3.12f1 is required for project integration.

### Requirements for AR Sample Project
* An Android mobile device equipped with a camera, GPS functionality, and Internet connectivity is mandatory for the application’s operation.

## Features

* [Feature service REST API](https://github.com/Esri/arcgis-maps-sdk-unity-samples/tree/main/samples_project/Assets/SampleViewer/Samples/FeatureLayer) - See how to query a feature service to create game objects in Unity located at real world positions.
* [Geocoding](https://github.com/Esri/arcgis-maps-sdk-unity-samples/tree/main/samples_project/Assets/SampleViewer/Samples/Geocoding) - Search for an address or click on the surface to get the address of that location.
* [HitTest](https://github.com/Esri/arcgis-maps-sdk-unity-samples/tree/main/samples_project/Assets/SampleViewer/Samples/HitTest) - Visualize individual Buildings ID's from a 3D Object Scene Layer.
* [Line of sight](https://github.com/Esri/arcgis-maps-sdk-unreal-engine-samples/tree/main/sample_project/Content/SampleViewer/Samples/LineOfSight) - See how to check line of sight between two object in Unity.
* [Material By Attribute](https://github.com/Esri/arcgis-maps-sdk-unity-samples/tree/main/samples_project/Assets/SampleViewer/Samples/MaterialByAttribute) - Apply materials to 3DObject Scene layer based on specific attributes.
* [Measure](https://github.com/Esri/arcgis-maps-sdk-unity-samples/tree/main/samples_project/Assets/SampleViewer/Samples/Measure) - Click on the map to get real world distances between points.
* [Routing](https://github.com/Esri/arcgis-maps-sdk-unity-samples/tree/main/samples_project/Assets/SampleViewer/Samples/Routing) - See how to query Esri's routing service to get the shortest path between two points and visualize that route in Unity.
* [Stream Layer](https://github.com/Esri/arcgis-maps-sdk-unity-samples/tree/main/samples_project/Assets/SampleViewer/Samples/StreamLayer) - See how to use web sockets to connect to an Esri real time service to update game objects locations in real time.
* [Third Person Controller](https://github.com/Esri/arcgis-maps-sdk-unity-samples/tree/main/samples_project/Assets/SampleViewer/Samples/ThirdPerson) - Explore from the perspective of a third person camera following a controllable character.

## Instructions

**Instructions for Importing Titan Campus AR Unity Package**
1. *Download the Unity Package:*
   * Locate the Titan Campus AR.unitypackage file in your GitHub repository.
   * Click on the file and use the download option provided by GitHub to save the .unitypackage file to your local machine.

2. *Open Unity:*
   * Launch the Unity Editor.
   * Open an existing project where you want to import the package, or create a new Unity project.

3. *Import the Unity Package:*
   * Once you have your project open in Unity, go to the top menu bar and click on Assets.
   * From the dropdown menu, select Import Package > Custom Package....
   * Navigate to the location where you downloaded Titan Campus AR.unitypackage.
   * Select the file and click Open.

4. *Confirm the Import:*
   * A dialog will appear showing all the files to be imported. By default, all files should be selected.
   * Verify that all the necessary files are checked. If you only need certain parts of the package, you can uncheck the items you don't want to import.
   * Click on the Import button to start the import process.

5. *Wait for Unity to Complete the Import:*
   * Unity will now import all the selected items from the package into your project. This process may take some time depending on the size of the package and the speed of your computer.
   
6. *Verify the Imported Assets:*
   * Once Unity has finished importing, you can verify the assets by navigating to the Assets folder in the Project window within Unity.
   * Look for a folder with the name Titan Campus AR or similar, which should contain all the imported assets.

7. *Ready to Use:*
   * The assets from the package are now ready to use in your project. You can drag and drop the assets into your scene or reference them in your scripts as needed.
   
**Instructions for Opening the Project from the Disk**
1. *Clone the Repository:*
   * Navigate to the GitHub page for the titan-ar-tour project.
   * Click on the 'Code' button and copy the URL to clone the repository.

2. *Open Unity Hub:*
   * Launch Unity Hub on your machine.
   * Go to the 'Projects' tab.

3. *Add the Project:*
   * Click on the 'Add' button.
   * Browse to the location where you cloned the titan-ar-tour repository.
   * Select the folder and Unity Hub will add it to your projects list.

4. *Open the Project:*
   * Click on the titan-ar-tour project in Unity Hub to open it with Unity Editor.
   * If Unity prompts you to upgrade the project to your current version of Unity, create a backup and proceed with the upgrade if you wish. Otherwise, continue with the project's current version if it's    * compatible with your Unity Editor version.

## Resources

* [ARCore](https://developers.google.com/ar)
* [Unity's documentation](https://docs.unity.com/)

## Issues

Find a bug or want to request a new feature?  Please let us know by submitting an issue.

## Licensing

* [ARCore](https://developers.google.com/ar)
* [Unity's documentation](https://docs.unity.com/)
