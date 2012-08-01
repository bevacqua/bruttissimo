using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using log4net.Config;

[assembly: AssemblyCompany("Bruttissimo")]
[assembly: AssemblyProduct("Bruttissimo")]
[assembly: AssemblyCopyright("Copyright © Bruttissimo 2011")]

[assembly: ComVisible(false)]

[assembly: AssemblyVersion("1.0.*")]

// NOTE: the log4net configuration must be at the specified relative path from the entry point project root
[assembly: XmlConfigurator(Watch = true, ConfigFile = "Xml/log4net.config" )]

// NOTE: allow Unit Tests to access internal classes and methods
#if DEBUG
[assembly: InternalsVisibleTo("Bruttissimo.Tests")]
[assembly: InternalsVisibleTo("Bruttissimo.Tests.Integration")]
[assembly: InternalsVisibleTo("Bruttissimo.Tests.Mocking")]
#endif