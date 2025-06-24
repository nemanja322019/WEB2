
<a id="readme-top"></a>

<!-- PROJECT LOGO -->
<br>
<h2 align="center">igd-re-arms-pes-function</h2>
<br>



<!-- TABLE OF CONTENTS -->
<details open>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#run">Run project</a></li>
      </ul>
    </li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

Source code for a cloud function. It provides scheduled exports of measurement data from BigQuery to Google Cloud Storage in AVRO format, organized by date and vendor. It simulates the behavior of a Permanent Event Storage (PES) system.

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- GETTING STARTED -->
## Getting Started

### Prerequisites

flask

google-cloud-bigquery

### Run PES function

The function exposes a REST endpoint (/export) that can be triggered to export data for a specified time window.
Accepts POST requests with either:

- A custom time window
```json
  {
    "when_read_from": "YYYY-MM-DDTHH:MM:SS",
    "when_read_to": "YYYY-MM-DDTHH:MM:SS"
  }
```
- A period string in format yyyyMM
```json
  {
    "period": "202503"
  }
```
- No parameters (defaults to previous month)

The folder structure for storing the data in the bucket is:

`<bucket-name>/year=<YYYY>/month=<MM>/day=<DD>/vendor=<vendor>`

Example:

`dev-em-backup-bucket/year=2025/month=03/day=23/vendor=testvendor`

This code will get deployed in GCP as a cloud function using github actions.
The configuration used in the deployment is written in json files under the `workflows/config` folder.


<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- LICENSE -->
## License

Developed for internal use only.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

Email: [o365g_digitalsustainability_itsehbg@ingka.com](o365g_digitalsustainability_itsehbg@ingka.com)

Project Link: [GitHub link](https://github.com/ingka-group-digital/igd-re-arms-pes-function)

<p align="right">(<a href="#readme-top">back to top</a>)</p>
