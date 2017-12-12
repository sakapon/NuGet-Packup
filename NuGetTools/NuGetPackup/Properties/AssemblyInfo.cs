using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// アセンブリに関する一般情報は以下の属性セットをとおして制御されます。
// アセンブリに関連付けられている情報を変更するには、
// これらの属性値を変更してください。
[assembly: AssemblyTitle("NuGet Packup")]
[assembly: AssemblyDescription("NuGet Packup is a tool to create a NuGet package for the project, with getting values from AssemblyInfo.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Keiho Sakapon")]
[assembly: AssemblyProduct("Keiho Tools")]
[assembly: AssemblyCopyright("© 2013 Keiho Sakapon.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyMetadata("ProjectUrl", "http://sakapon.codeplex.com/wikipage?title=NuGet%20Packup")]
[assembly: AssemblyMetadata("LicenseUrl", "http://sakapon.codeplex.com/license")]
[assembly: AssemblyMetadata("Tags", "NuGet Package")]
[assembly: AssemblyMetadata("ReleaseNotes", "Modified to use AssemblyName, not RootNamespace, for package ID.")]

// ComVisible を false に設定すると、その型はこのアセンブリ内で COM コンポーネントから 
// 参照不可能になります。COM からこのアセンブリ内の型にアクセスする場合は、
// その型の ComVisible 属性を true に設定してください。
[assembly: ComVisible(false)]

// 次の GUID は、このプロジェクトが COM に公開される場合の、typelib の ID です
[assembly: Guid("6b1953ac-7c7e-473c-ae45-b9246cbd564f")]

// アセンブリのバージョン情報は、以下の 4 つの値で構成されています:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// すべての値を指定するか、下のように '*' を使ってビルドおよびリビジョン番号を 
// 既定値にすることができます:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.11.0")]
[assembly: AssemblyFileVersion("1.0.11.0")]

[assembly: NeutralResourcesLanguage("ja-JP")]
