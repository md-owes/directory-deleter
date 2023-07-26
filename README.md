# directory-deleter

This tool is developed to assist mainly developers and testers who run into frequent problem of deleting temporary artifacts. Using this tool, you can create profiles of frequently deleted locations and folders to delete. 


## PreRequisites

The following pre-requisites have to be installed or configured before running this tool
* Ensure you have dotnet 7 SDK and above (latest stable dotnet sdk is preferable)

To work on this tool you need to
* Ensure you have .Net MAUI workload installed
    * run the below command which will install maui if not installed already
        ```
        dotnet workload install maui
        ```
    * If using Visual Studio Code then the C# Dev Kit extension is required which will add the build and run pipelines


## Installation

For MacOS, use the package file named directory-deleter-v{VERSION}.pkg, for Windows server use the exe directory-deleter-v{VERSION}.exe and for Windows 10 & 11 use the msix file directory-deleter-v{VERSION}.msix 

To install directory-deleter on Windows 10 & 11, you need to install the public key certificate first. The certificate is named directory-deleter.cer and it should be installed in Trusted People as mentioned in [Microsoft documentation](https://learn.microsoft.com/en-us/dotnet/maui/windows/deployment/publish-cli#installing-the-app) for store apps


## Usage/Examples
<style type="text/css">
    img {
        width: 600px;
        height:450px;
    }
</style>
Enter the required information 

![Step1](/.github/img/FillDetails.png?raw=true "Fill required details") 

Save as a profile 

![Step2.1](/.github/img/SaveProfileButton.png?raw=true "Save Profile Button")
![Step2.2](/.github/img/SaveProfileDialog.png?raw=true "Save Profile")

Clear out filled details

![Step3](/.github/img/ResetProfileButton.png?raw=true "Reset Profile")

Load saved profile 

![Step4.1](/.github/img/LoadProfileButton.png?raw=true "Load Profile Button")
![Step4.2](/.github/img/LoadProfileDialog.png?raw=true "Load Profile")


## Tech Stack

**Application:** Dotnet MAUI with C# language


## Authors

- [@mdowes](https://www.github.com/md-owes)


## Contributing

Contributions are always welcome! Shoot a [mail](mailto:mdowes@outlook.com) if you would like to become a contributor.


## License

[GPLv3](https://choosealicense.com/licenses/gpl-3.0/)

