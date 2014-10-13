![wickedflame injectionmap](assets/wickedflame injectionmap - black.png)

# InjectionMap.Configuration
------------------------------
InjectionMap.Configuration is a small extension to InjectionMap that allows the definition of mappings in the application configuration file. 

# Usage
------------------------------

```csharp
public interface IContractOne { }

public class ObjectTypeOne : IContractOne { }
```
Define the mappings in the config file.
```csharp
<configuration>
  <configSections>
    <!-- Define the section -->
    <section name="injectionMap" type="InjectionMap.Configuration.InjectionMapSection, InjectionMap.Configuration" />
  </configSections>

  <injectionMap>
    <mappings>
      <!-- Map IContractOne to ObjectTypeOne -->
      <map contract="TestApp.IContractOne, TestApp" mappedType="TestApp.ObjectTypeOne, TestApp"/>
      <!-- Map ObjectTypeOne to self -->
      <map contract="TestApp.ObjectTypeOne, TestApp" toSelf="true"/>
    </mappings>
    <initializers>
      <!-- Register MapInitializers -->
      <init contract="TestApp.InjectionMapInitializer, TestApp"/>
    </initializers>
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
InjectionMap.Configuration can be installed from [NuGet](http://docs.nuget.org/docs/start-here/installing-nuget) through the package manager console:  

    PM > Install-Package InjectionMap.Configuration

## Bugs, issues and features
------------------------------
Bugs, issues or feature wishes can submitted on the [issues](https://github.com/InjectionMap/InjectionMap.Configuration/issues) page or feel free to fork the project and send a pull request.


InjectionMap is developed by [wickedflame](http://wicked-flame.blogspot.ch/) under the [Ms-PL License](License.txt).