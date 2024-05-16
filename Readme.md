# Titan Campus AR
![image](https://github.com/HRB-NO1/Titan-Campus-AR/assets/54606160/666f1a97-3d5c-4681-a2b3-ae83225d1253)
![image](https://github.com/HRB-NO1/Titan-Campus-AR/assets/54606160/e05ea7f1-a157-4194-b6ec-37969754d41b)


<div style="display: flex; flex-direction: row; align-items: center; justify-content: center;">
  <img src="Resource/Screenshot_20231119_192620_Gallery.jpg" width="300" style="margin-right: 10px;"/>
  <img src="Resource/Screenshot_20231119_192639_Gallery.jpg" width="300" style="margin-right: 10px;"/>
  <img src="Resource/Screenshot_20231119_192727_Gallery.jpg" width="300" style="margin-right: 10px;"/>

https://github.com/HRB-NO1/Titan-Campus-AR/assets/54606160/afa9698b-a62f-4372-9bbe-a9ad87e28cee

</div>


### Note
For optimal utilization within the Unity editor, it is recommended to commence with the Menu Scene as the initial point of interaction.

### Requirements for Developing
* A computing system operating on Windows or macOS platforms.
* Compatibility with Unity version 2021.3.12f1 is required for project integration.

### Requirements for AR Sample Project
* An Android mobile device equipped with a camera, GPS functionality, and Internet connectivity is mandatory for the application’s operation.

## Features

**Map Scene**
* [GoogleMapStyleGesture](https://github.com/HRB-NO1/Titan-Campus-AR/blob/main/titan-ar-tour/Assets/Menu%20Related/GoogleMapStyleGesture.cs) - The GoogleMapStyleGesture class in Unity is designed to replicate the interaction model of Google Maps, allowing users to pan and zoom the camera view smoothly. This class is particularly useful for applications that involve navigating large maps or other 2D environments.


**AR Nav**
* [NavigationCalculator](https://github.com/HRB-NO1/Titan-Campus-AR/blob/main/titan-ar-tour/Assets/AR%20Nav%20Related/Scripts/NavigationCalculator.cs) - The NavigationCalculator class in the CSUF_AR_Navigation namespace provides essential geospatial calculations for augmented reality navigation applications. This class includes methods to calculate distances and headings between two geographic coordinates, making it a critical component for navigation and mapping tasks.
* [NavigationManager](https://github.com/HRB-NO1/Titan-Campus-AR/blob/main/titan-ar-tour/Assets/AR%20Nav%20Related/Scripts/NavigationManager.cs) - The NavigationManager class in the CSUF_AR_Navigation namespace provides navigation functionalities for augmented reality applications. This class interacts with Google Maps API to fetch directions and manage navigation steps, decoding polylines for intermediary points and calculating distances and headings between coordinates. It supports AR-based navigation by converting real-world geospatial data into actionable navigation steps and intermediary points.
* [ProprietaryStructs](https://github.com/HRB-NO1/Titan-Campus-AR/blob/main/titan-ar-tour/Assets/AR%20Nav%20Related/Scripts/ProprietaryStructs.cs) - The ProprietaryStructs.cs file in the CSUF_AR_Navigation namespace defines essential data structures used for augmented reality navigation applications. These structures encapsulate key information required for geospatial calculations and navigation steps, supporting the overall navigation functionality of the application.
* [SampleScript](https://github.com/HRB-NO1/Titan-Campus-AR/blob/main/titan-ar-tour/Assets/AR%20Nav%20Related/Scripts/SampleScript.cs) - The SampleScript class in the CSUF_AR_Navigation namespace is designed for augmented reality navigation using ARCore and Google's Geospatial API. It tracks user location and orientation to provide real-time navigation guidance through AR markers and intermediary points.
* [UIController](https://github.com/HRB-NO1/Titan-Campus-AR/blob/main/titan-ar-tour/Assets/AR%20Nav%20Related/Scripts/UIController.cs) - The UIController class in the CSUF_AR_Navigation namespace manages the user interface for an augmented reality navigation application in Unity. This class uses Unity's UIElements system to control and update various UI components, providing real-time feedback and controls for the user.

**AR Tuffy**
* [SampleScript](https://github.com/HRB-NO1/Titan-Campus-AR/blob/main/titan-ar-tour/Assets/AR%20Tuffy%20Related/SampleScript.cs) - The SampleScript class in Unity is designed to manage augmented reality (AR) experiences using ARCore and the Google Geospatial API. It handles tracking, object instantiation, and UI updates to create an interactive AR navigation experience.

* [DialogueFetcher](https://github.com/HRB-NO1/Titan-Campus-AR/blob/main/titan-ar-tour/Assets/AR%20Tuffy%20Related/AR_Fukuoka/Scripts/Dialogue/DialogueFetcher.cs) - The DialogueFetcher class is a utility designed for asynchronously fetching dialogue data from a remote server and returning it as a TextAsset in Unity. This class leverages Unity's UnityWebRequest to perform HTTP GET requests and handle the responses efficiently.

* [DialogueManager](https://github.com/HRB-NO1/Titan-Campus-AR/blob/main/titan-ar-tour/Assets/AR%20Tuffy%20Related/AR_Fukuoka/Scripts/Dialogue/DialogueManager.cs) - The DialogueManager class in Unity is designed to manage and display interactive dialogue using Ink, a narrative scripting language. This class handles the initialization, display, and progression of dialogue, including presenting choices to the player and managing user input.

**Shared Scripts Utilized in Both AR Nav and AR Tuffy**
* [SceneSwitcher](https://github.com/HRB-NO1/Titan-Campus-AR/blob/main/titan-ar-tour/Assets/AR%20Tuffy%20Related/AR_Fukuoka/Scripts/SceneSwitcher.cs) - The SceneSwitcher class in Unity is designed to handle scene transitions triggered by UI button clicks. This class facilitates smooth transitions between scenes while also setting a destination value using Unity's PlayerPrefs.



## Instructions

**Android Installation Instructions for Titan Campus AR**
1. *Download the APK File:*
   * Locate the [Titan Campus AR.apk](https://github.com/HRB-NO1/Titan-Campus-AR/blob/main/Titan%20Campus%20AR.apk) file in the repository.
   * Download the .apk file to your Android device.

2. *Install the Application:*
   * Once downloaded, tap on the Titan Campus AR.apk file from your device's download folder
   * Your device may ask for permissions to install applications from unknown sources. Please allow this permission to proceed.
   * Follow the on-screen instructions to complete the installation.
     
3. *Install the Application:*
   * After installation, open Titan Campus AR from your device’s app drawer.
   * The application is now ready for use.


4. *Confirm the Import:*
   * A dialog will appear showing all the files to be imported. By default, all files should be selected.
   * Verify that all the necessary files are checked. If you only need certain parts of the package, you can uncheck the items you don't want to import.
   * Click on the Import button to start the import process.
     
**Instructions for Importing Titan Campus AR Unity Package**
1. *Download the Unity Package:*
   * Locate the [Titan Campus AR.unitypackage](https://github.com/HRB-NO1/Titan-Campus-AR/blob/main/Titan%20Campus%20AR.unitypackage) file in your GitHub repository.
   * Click on the file and use the download option provided by GitHub to save the .unitypackage file to your local machine.

2. *Open Unity:*
   * Launch the Unity Editor.
   * Open an existing project where you want to import the package, or create a new Unity project.

3. *Import the Unity Package:*
   * Once you have your project open in Unity, go to the top menu bar and click on Assets.
   * From the dropdown menu, select Import Package > Custom Package....
   * Navigate to the location where you downloaded Titan Campus AR.unitypackage.
   * Select the file and click Open.

5. *Wait for Unity to Complete the Import:*
   * Unity will now import all the selected items from the package into your project. This process may take some time depending on the size of the package and the speed of your computer.
   
6. *Verify the Imported Assets:*
   * Once Unity has finished importing, you can verify the assets by navigating to the Assets folder in the Project window within Unity.
   * Look for a folder with the name Titan Campus AR or similar, which should contain all the imported assets.

7. *Ready to Use:*
   * The assets from the package are now ready to use in your project. You can drag and drop the assets into your scene or reference them in your scripts as needed.
   
**Instructions for Opening the Project from the Disk**
1. *Clone the Repository:*
   * Navigate to the GitHub page for the [titan-ar-tour](https://github.com/HRB-NO1/Titan-Campus-AR/tree/main/titan-ar-tour) project.
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
