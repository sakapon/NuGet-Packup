using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Keiho.Tools.NuGetPackup
{
    static class Program
    {
        const string TemplateFileName = "Package.nuspec.xml";
        const string NuSpecFileName = "Package.nuspec";

        const string PackageDirPath = @"..\packages";
        const string NuGetExeId = "NuGet.CommandLine";
        const string NuGetExeFileName = "NuGet.exe";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:ローカライズされるパラメーターとしてリテラルを渡さない")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods")]
        static int Main(string[] args)
        {
            // Find a project file.
            var projectFilePath = Directory.EnumerateFiles(Environment.CurrentDirectory, "*.csproj")
                .Concat(Directory.EnumerateFiles(Environment.CurrentDirectory, "*.vbproj"))
                .Concat(Directory.EnumerateFiles(Environment.CurrentDirectory, "*.fsproj"))
                .FirstOrDefault();
            if (projectFilePath == null)
            {
                Console.WriteLine("There is no project file.");
                return 10;
            }
            var projectNuSpecTemplateFilePath = Path.Combine(Environment.CurrentDirectory, TemplateFileName);
            var projectNuSpecFilePath = Path.Combine(Environment.CurrentDirectory, NuSpecFileName);

            // Load the project file as XML.
            var ns = XNamespace.Get("http://schemas.microsoft.com/developer/msbuild/2003");
            var projectXml = XDocument.Load(projectFilePath).Root;

            // Determine the path of target assembly file.
            var assemblyName = projectXml
                .Element(ns + "PropertyGroup")
                .Element(ns + "AssemblyName").Value;
            var outputType = projectXml
                .Element(ns + "PropertyGroup")
                .Element(ns + "OutputType").Value;
            var outputPath = projectXml
                .Elements(ns + "PropertyGroup")
                .Where(e => e.Attribute("Condition") != null)
                .First(e => e.Attribute("Condition").Value.Contains("Release"))
                .Element(ns + "OutputPath").Value;
            Func<string, string> toExtension = oType => oType == "Exe" ? ".exe" : ".dll";
            var assemblyPath = Path.Combine(Environment.CurrentDirectory, outputPath, assemblyName + toExtension(outputType));
            if (!File.Exists(assemblyPath))
            {
                Console.WriteLine("Target assembly file does not exist. : {0}", assemblyPath);
                return 10;
            }

            // Get attributes from the assembly.
            var targetAssembly = Assembly.LoadFile(assemblyPath);
            var fileVersion = targetAssembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
            var title = targetAssembly.GetCustomAttribute<AssemblyTitleAttribute>();
            var description = targetAssembly.GetCustomAttribute<AssemblyDescriptionAttribute>();
            var company = targetAssembly.GetCustomAttribute<AssemblyCompanyAttribute>();
            var copyright = targetAssembly.GetCustomAttribute<AssemblyCopyrightAttribute>();
            var metadata = targetAssembly.GetCustomAttributes<AssemblyMetadataAttribute>().ToDictionary(x => x.Key.ToLowerInvariant(), x => x.Value);

            // Create a .nuspec template file, if it does not exist.
            if (!File.Exists(projectNuSpecTemplateFilePath))
            {
                File.WriteAllText(projectNuSpecTemplateFilePath, Properties.Resources.PackageNuSpec);
                Console.WriteLine("Created file: {0}", TemplateFileName);
            }

            // Create a .nuspec file.
            var nuSpecContent = File.ReadAllText(projectNuSpecTemplateFilePath)
                .Replace("$id$", assemblyName)
                .Replace("$version$", fileVersion.IfNotNull(x => x.Version, ""))
                .Replace("$title$", title.IfNotNull(x => x.Title, ""))
                .Replace("$description$", description.IfNotNull(x => x.Description, ""))
                .Replace("$author$", company.IfNotNull(x => x.Company, ""))
                .Replace("$copyright$", copyright.IfNotNull(x => x.Copyright, ""))
                .Replace("$projectUrl$", metadata.GetValue("projecturl", ""))
                .Replace("$licenseUrl$", metadata.GetValue("licenseurl", ""))
                .Replace("$tags$", metadata.GetValue("tags", ""))
                .Replace("$releaseNotes$", metadata.GetValue("releasenotes", ""))
                ;
            File.WriteAllText(projectNuSpecFilePath, nuSpecContent);

            // Determine the path of NuGet.exe.
            if (!Directory.Exists(PackageDirPath))
            {
                Console.WriteLine("Ensure that {0} has been installed.", NuGetExeId);
                return 10;
            }
            var nuGetExePath = Directory.EnumerateDirectories(PackageDirPath, NuGetExeId + "*")
                .OrderByDescending(x => x)
                .SelectMany(x => Directory.EnumerateFiles(x, NuGetExeFileName, SearchOption.AllDirectories))
                .FirstOrDefault();
            if (nuGetExePath == null)
            {
                Console.WriteLine("Ensure that {0} has been installed.", NuGetExeId);
                return 10;
            }

            // Execute "nuget pack".
            var startInfo = new ProcessStartInfo(nuGetExePath, string.Format("pack {0}", NuSpecFileName))
            {
                UseShellExecute = false,
            };
            Process.Start(startInfo).WaitForExit();

            // Delete the temporary file.
            File.Delete(projectNuSpecFilePath);

            return 0;
        }

        public static TValue IfNotNull<T, TValue>(this T obj, Func<T, TValue> selector, TValue defaultValue = default(TValue))
        {
            if (selector == null) throw new ArgumentNullException("selector");

            return obj != null ? selector(obj) : defaultValue;
        }

        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default(TValue))
        {
            return dictionary.ContainsKey(key) ? dictionary[key] : defaultValue;
        }
    }
}
