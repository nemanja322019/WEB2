<a id="readme-top"></a>

<!-- PROJECT LOGO -->
<br>
<h2 align="center"> igd-re-arms-vendor-data-simulator </h2>
<br>

<!-- TABLE OF CONTENTS -->
<details open>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="##about-the-project">About The Project</a>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li>
          <a href="#prerequisites">Prerequisites</a>
          <ul>
            <li><a href="#setting-up-java-fx-in-intellij">Setting up Java FX in Intellij</a></li>
            <li><a href="#scenebuilder-tool-integration-with-intellij">SceneBuilder tool integration with Intellij</a></li>
          </ul>
        </li>
        <li>
          <a href="#run-vendor-data-simulator">Run Vendor Data Simulator</a>
          <ul>
            <li><a href="#running-from-intellij">Running from Intellij</a></li>
            <li><a href="#building-an-executable-jar-file">Building an executable jar file</a></li>
          </ul>
        </li>
      </ul>
    </li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

Source code for software application used in Energy Monitoring Project for generating fake measurements data to be used in e2e testing.

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- GETTING STARTED -->
## Getting Started

### Prerequisites

The project is using Java FX for setting up a basic GUI used for parameter configuration.

#### Setting up Java FX in Intellij

The Java FX library needs to be configured in Intellij. Download the Java FX SDK from the following link: https://gluonhq.com/products/javafx/

And now in Intellij go to "Project Structure"->"Project Settings"->"Libraries"->"New Project Library"
and provide the path to where the Java FX lib folder is located on the machine. Example:

    C:\Program Files\Java\javafx-sdk-17.0.9\lib

#### SceneBuilder tool integration with Intellij

The panel containing all the fields from the GUI is created using the generator-panel-view.fxml file. SceneBuilder tool can be used to easily edit this file. In order to integrate SceneBuilder with Intellij you need to follow these steps:

    1. Download and install the latest version of Scene Builder https://gluonhq.com/products/scene-builder/

    2. In the Settings dialog select Languages & Frameworks | JavaFX.

    3. Click the Browse button in the Path to SceneBuilder field.

    4. In the dialog that opens, select the Scene Builder application (executable file) on your computer and click OK.

    5. Apply the changes and close the dialog.

At this stage the .fxml file can be opened in SceneBuilder by right-clicking on the file in IntelliJ and selecting "Open in SceneBuilder"

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Run Vendor Data Simulator

Build the project using the Maven command:

    mvn clean package

#### Running from Intellij

In Intellij create an Application configuration to run the code and add the JavaFX module path and modules to the VM arguments:

    --module-path "C:\Program Files\Java\javafx-sdk-17.0.9\lib" --add-modules javafx.fxml,javafx.base,javafx.controls,javafx.graphics,javafx.media

Now run the newly created Application configuration.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

#### Building an executable jar file

1. Go to File->Project Structure->Artifacts->Add->JAR->From module with dependencies
2. Choose the igd-re-arms-vendor-data-simulator Module
3. Choose the MainApplication.class as Main Class
4. Choose the /src/ as the directory for the META-INF/MANIFEST.MF

`Ex: D:\00_Dev\10_IKEA_Workspace\30_Projects\ingka-group-digital-realestate\igd-re-arms-vendor-data-simulator\src\`

5. In the Output Layout tab on the right panel click the Add Copy Of -> File and choose all the .dll files under the Java FX bin folder

`Ex: All dll files from C:\Program Files\Java\javafx-sdk-17.0.9\bin\`

6. Click Apply and then OK
7. Go to Build->Build Artifacts
8. Choose the igd-re-arms-vendor-data-simulator:jar->Build
9. Go inside the /out/ directory and double click the resulted jar file to start the app

`Ex: ..\out\artifacts\igd_re_arms_vendor_data_simulator_jar`

The executable jar is also stored on confluence: https://confluence.build.ingka.ikea.com/display/HRRE/Measurements+data+generator

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- LICENSE -->
## License

Developed for internal use.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTACT -->
## Contact

Email: [o365g_digitalsustainability_itsehbg@ingka.com](o365g_digitalsustainability_itsehbg@ingka.com)

Project Link: [GitHub link](https://github.com/ingka-group-digital/igd-re-arms-vendor-data-simulator)

<p align="right">(<a href="#readme-top">back to top</a>)</p>
