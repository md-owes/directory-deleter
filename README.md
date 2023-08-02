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

To enable logging of issues in directory-deleter
* On Mac, you need to execute the following command in terminal
    ```
    launchctl setenv DD_EnableLogs 1
    ```
* On windows, you need to set system environment variable either manually or via below command
    ```
    setx DD_EnableLogs "1" /M
    ```

Now you will see a new file created at the following locations
* On Mac, file named directory-delete<date>.log will appear in /Users/<username>/Library
* On Windows, file named directory-delete<date>.log
    * if it is exe then location will be C:\Users\<username>\AppData\Roaming\<username>\c5a240f0-6866-4aa3-8d34-9c682b0cf217\Data
    * If it is msix then location will be C:\Users\<username>\AppData\Local\Packages\c5a240f0-6866-4aa3-8d34-9c682b0cf217_<packageid>\LocalState

Note: On Windows the log location will have a static GUID (c5a240f0-6866-4aa3-8d34-9c682b0cf217) in its path, this GUID is a hardcoded GUID for this app and can change in future.

After changing the value of environment variable, you need to close and open the app again for changes to take effect.

## Usage/Examples
Enter the required information 

<img src="/.github/img/FillDetails.png?raw=true" width=400px height=300px />

Save as a profile 

<img src="/.github/img/SaveProfileButton.png?raw=true" width=400px height=300px /> <img src="/.github/img/SaveProfileDialog.png?raw=true" width=400px height=300px />

Clear out filled details

<img src="/.github/img/ResetProfileButton.png?raw=true" width=400px height=300px />

Load saved profile 

<img src="/.github/img/LoadProfileButton.png?raw=true" width=400px height=300px /> <img src="/.github/img/LoadProfileDialog.png?raw=true" width=400px height=300px />


## Tech Stack

**Application:** Dotnet MAUI with C# language


## Authors

- [@mdowes](https://www.github.com/md-owes)


## Contributing

Contributions are always welcome! Shoot a [mail](mailto:mdowes@outlook.com) if you would like to become a contributor.


## License

[GPLv3](https://choosealicense.com/licenses/gpl-3.0/)

