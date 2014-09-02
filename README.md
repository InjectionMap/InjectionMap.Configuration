![wickedflame injectionmap](assets/wickedflame injectionmap - black.png)

# InjectionMap.Configuration
------------------------------
InjectionMap.Configuration is a small extension to InjectionMap for defining mappings in the application configuration file. 

# Usage
------------------------------

```csharp
public interface IKeyOne { }

public class ObjectTypeOne : IKeyOne { }
```
Define the mappings in the config file.
```csharp
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <!-- Define the section -->
    <section name="injectionMap" type="InjectionMap.Configuration.InjectionMapSection, InjectionMap.Configuration" />
  </configSections>

  <injectionMap>
    <mappings>
      <!-- Map IKeyOne to ObjectTypeOne -->
      <map key="TestApp.IKeyOne, TestApp" for="TestApp.ObjectTypeOne, TestApp"/>
      <!-- Map ObjectTypeOne to self -->
      <map key="TestApp.ObjectTypeOne, TestApp" toSelf="true"/>
    </mappings>
  </injectionMap>
</configuration>
```
The Section has to be called "injectionMap" for InjectionMap to find it.
Import the namespace InjectionMap.Configuration and create a instance of InjectionMapper. To initialize the configuration just call the extensionmethod Initialize() on InjectionMapper. This call should only be made once per AppDomain, ideally in the application startup.
```csharp
using InjectionMap.Configuration;
...

using (var mapper = new InjectionMapper())
{
    mapper.Initialize();
}
```
## Installation
------------------------------
InjectionMap can be installed from [NuGet](http://docs.nuget.org/docs/start-here/installing-nuget) through the package manager console:  

    PM > Install-Package InjectionMap.Configuration

## Bugs, issues and features
------------------------------
Bugs, issues or feature wishes can submitted on the [issues](https://github.com/InjectionMap/InjectionMap.Configuration/issues) page or feel free to fork the project and send a pull request.


InjectionMap is developed by [wickedflame](http://wicked-flame.blogspot.ch/) under the [Ms-PL License](License.txt).