<h2 style="text-align: center"> igd-re-arms-vendor-data-simulator </h2>

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
        <li><a href="#prerequisites">Prerequisites</a></li>
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

<!-- LICENSE -->
## License

Developed for internal use.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTACT -->
## Contact

Email: [o365g_digitalsustainability_itsehbg@ingka.com](o365g_digitalsustainability_itsehbg@ingka.com)

Project Link: [GitHub link](https://github.com/ingka-group-digital/igd-re-arms-vendor-data-simulator)

<p align="right">(<a href="#readme-top">back to top</a>)</p>
