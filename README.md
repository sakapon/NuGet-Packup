## NuGet Packup
NuGet Packup is a tool to create a NuGet package for the project, with getting values from AssemblyInfo.

[NuGet Packup](http://nuget.org/packages/Keiho.Tools.NuGetPackup/) は、アセンブリの情報 (主に AssemblyInfo.cs) から必要な値を取得して、  
NuGet パッケージを作成するためのツールです。

### インストール方法
Visual Studio でプロジェクトを右クリックして [Nuget パッケージの管理] から NuGet Packup を検索するか、  
またはパッケージ マネージャー コンソールで
```
Install-Package Keiho.Tools.NuGetPackup
```
を実行すればインストールできます。

NuGet Packup をインストールすると、[NuGet.CommandLine](http://nuget.org/packages/NuGet.CommandLine/) も同時にインストールされます。  
インストールが完了すると、次のファイルがプロジェクトに追加されます。
* NuGetPackup.exe
* Package.nuspec.xml

### 値の指定方法
Package.nuspec.xml は、.nuspec ファイルのテンプレートとして使用されます。  
Package.nuspec.xml にある $id$, $version$ などのプレースホルダーは、  
NuGet パッケージを作成するために必要なパラメーターを表します。

各プレースホルダーに代入される値は、次の表の場所から取得されます。太字は必須項目です。  
AssemblyInfo.cs に最初から記述されている AssemblyFileVersion 属性などのほかにも、  
AssemblyMetadata 属性を利用して値を指定しておきます。

| プレースホルダー | 値の取得先 |
| **$id$** | プロジェクトのプロパティのアセンブリ名 |
| **$version$** | AssemblyFileVersion 属性 |
| $title$ | AssemblyTitle 属性 |
| **$description$** | AssemblyDescription 属性 |
| **$author$** | AssemblyCompany 属性 |
| $copyright$ | AssemblyCopyright 属性 |
| **$projectUrl$** | AssemblyMetadata 属性、キー ProjectUrl |
| **$licenseUrl$** | AssemblyMetadata 属性、キー LicenseUrl |
| $tags$ | AssemblyMetadata 属性、キー Tags |
| $releaseNotes$ | AssemblyMetadata 属性、キー ReleaseNotes |

![](https://github.com/sakapon/NuGet-Packup/blob/master/Images/NuGetPackup-assembly.png)

必要であれば、[.nuspec ファイルの仕様](http://docs.nuget.org/docs/reference/nuspec-reference)に従って Package.nuspec.xml を編集します。  
例えば、
* <frameworkAssemblies> : .NET Framework アセンブリへの参照
* <dependencies> : パッケージへの参照
* <files> : パッケージに含めるファイル

などがよく使われると思います。  
既定では、bin\Release にあるファイルをライブラリとして追加します。

![](https://github.com/sakapon/NuGet-Packup/blob/master/Images/NuGetPackup-nuspec.png)

### 実行方法
NuGetPackup.exe を実行すれば、NuGet パッケージが作成されます。  
ただし、先にプロジェクトの Release ビルドを実施しておく必要があります。

#### 動作環境
* .NET Framework 4.5 以降

#### 参照
* [NuGet Packup](http://nuget.org/packages/Keiho.Tools.NuGetPackup/)
* [Nuspec Reference](http://docs.nuget.org/docs/reference/nuspec-reference)
* [Using NuGet Packup](Using-NuGet-Packup) : 典型的な利用プロセス
* [NuGet パッケージを作成して公開する](http://sakapon.wordpress.com/2013/07/16/nugetpackage/)

#### 補記
拡張子が .nuspec であるファイルを NuGet パッケージに含めることができないため、  
ファイル名を Package.nuspec.xml としています。
