# Dicom Viewer Demo
This demo loads DICOM images and metadata using the Atalasoft.dotImage.Dicom assembly.  

For a demo that shows Dicom Leveling, plese try the Dicom Leveling Demo.

Requires evaluation or purchased licenses of DotImage Document Imaging or Photo Pro, as 
well as our Dicom Decoder addon.

This is the VB.NET version. We also offer a [C# version](https://github.com/AtalaSupport/DemoGallery_Desktop_DicomViewerDemo_CS_x64).


## Licensing
This application requires a license for DotImage Document Imaging.


## SDK Dependencies
This app was built based on 2026.2.0.0. It targets .NET Framework 4.6.2 and was created in Visual Studio 2022. However, it's fairly backward compatible as distributed. If you start adding references, you can run into issues if you're using an especially outdated version of DotImage. It should also open and run equally well in Visual Studio 2026 without undue modification.  


### Using NuGet for SDK Dependencies
We do publish our SDK components to NuGet. We have chosen to base the demo on local installed SDK because this leads to much smaller applications (NuGet packages add a lot of overhead due to the way they're packaged and deployed, and many of our demos -- including this one -- are often used to reproduce issues that need to be submitted to support. Apps that use NuGet are often significantly larger and run up against our maximum support case upload size)

Still, if you wish to use NuGet for the dependencies instead of relying on locally installed SDK, you can.

- Take note of each of the references we've included:
    - Atalasoft.DotImage.dll
    - Atalasoft.DotImage.Lib.dll
    - Atalasoft.DotImage.Raw.dll
    - Atalasoft.Shared.dll
- Remove those referneces
- Open the NuGet Package Manger from `Tools -> NuGet Package Manager -> Manage NuGet Packages for this Solution`
- Browse for and install Atalasoft.DotImage.WinControls.x64 to install the main dependencies.
- Browse for Atalasoft.DotImage.Raw.x64 to add the Raw support components.

## Cloning
We recommend the following to ensure you clone with the required submodule

Example: git for windows
```bash
git clone https://github.com/AtalaSupport/DemoGallery_Desktop_DicomViewerDemo_VB_x64.git DicomViewerDemo
```

If you've got DotImage 2026.2 installed and licensed, it should just build and run.  


## Related documentation
In addition to this README, the Atalasoft documentation set includes the following:  
- API Reference (.chm file) gives the complete Atalasoft WingScan server-side class library for offline use. The latest versions are linked on [Atalasoft's APIs & Developer Guides page](https://www.atalasoft.com/Support/APIs-Dev-Guides).
- In addition, you can also refer to the following Atalasoft resources:
    - [Atalasoft Support](http://www.atalasoft.com/support/)
    - [Atalasoft Knowledgebase](http://www.atalasoft.com/kb2)


## Getting Help for Atalasoft products
Atalasoft regularly updates our support [Knowledgebase](http://www.atalasoft.com/kb2) with the latest information about our products. To access some resources, you must have a valid Support Agreement with an authorized Atalasoft Reseller/Partner or with Atalasoft directly. Use the tools that Atalasoft provides for researching and identifying issues. 

Customers with an active evaluation, or those with active support / maintenance may [create a support case](https://www.atalasoft.com/Support/my-portal/Cases/Create-Case) 24/7, or call in to support ([+1 949 236-6510](tel:19492366510) ) during our normal support hours (Monday - Friday 8:00am to 5:00PM Eastern (New York) time).  

Customers who are unable to create a case or call in may [email our Sales Team](email:sales@atalasoft.com).  

