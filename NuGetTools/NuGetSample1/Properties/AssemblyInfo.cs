using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// アセンブリに関する一般情報は以下の属性セットをとおして制御されます。
// アセンブリに関連付けられている情報を変更するには、
// これらの属性値を変更してください。
[assembly: AssemblyTitle("NuGet Sample 1")]
[assembly: AssemblyDescription(@"NuGet に公開されるライブラリのサンプルです。
This library is dummy.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Keiho Sakapon")]
[assembly: AssemblyProduct("Keiho Tools")]
[assembly: AssemblyCopyright("© 2013 Keiho Sakapon.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyMetadata("ProjectUrl", "http://sakapon.codeplex.com/")]
[assembly: AssemblyMetadata("LicenseUrl", "http://sakapon.codeplex.com/license")]
[assembly: AssemblyMetadata("Tags", "SampleTag1 SampleTag2")]
[assembly: AssemblyMetadata("ReleaseNotes", "Added Method1.")]

// ComVisible を false に設定すると、その型はこのアセンブリ内で COM コンポーネントから 
// 参照不可能になります。COM からこのアセンブリ内の型にアクセスする場合は、
// その型の ComVisible 属性を true に設定してください。
[assembly: ComVisible(false)]

// 次の GUID は、このプロジェクトが COM に公開される場合の、typelib の ID です
[assembly: Guid("0511fcff-eec2-48b5-9f81-9e8435dfc60d")]

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
[assembly: AssemblyVersion("1.1.0.0")]
[assembly: AssemblyFileVersion("1.1.0.0")]

[assembly: CLSCompliant(true)]
