# LemurRecognition
Unity + Magic Leap application that uses fine-tuned ResNet50 to recognize 5 different species of lemurs.

-Download UnityTemplate-0.24.2 from Magic Leap's website
-After opening the default scene, add to the scene Main Camera and Controller objects
-Make an object called ControllerInput and an object called UserInterfaceInput based on this tutorial:
-Make a canvas and add buttons and texts to it. All texts on the buttons should be TextMestProUGUI objects
-Make an empty object called LabelWebRequester and attach the LabelGetter C# script in this repository
-Open the LabelWebRequester object and drag and drop 4 buttons, each corresponding to one label
-Launch zero iteration on Magic Leap Lab application, connect Unity to zero iteration, and run the application on Unity. If the app runs correctly, the label on the top right should be the label with the highest score (most likely label).
