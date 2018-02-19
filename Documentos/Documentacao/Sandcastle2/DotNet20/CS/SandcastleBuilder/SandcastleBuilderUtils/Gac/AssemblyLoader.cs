//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : AssemblyLoader.cs
// Author  : Eric Woodruff
// Updated : 01/20/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class that is used to load assemblies in a separate
// application domain in order to obtain a list of namespaces within them.
// It also contains a utility method used to get a list of assemblies in the
// GAC.
//
// This code may be used in compiled form in any way you desire.  This file
// may be redistributed unmodified by any means PROVIDING it is not sold for
// profit without the author's written consent, and providing that this notice
// and the author's name and all copyright notices remain intact.
//
// This code is provided "as is" with no warranty either express or implied.
// The author accepts no liability for any damage or loss of business that
// this product may cause.
//
// Version     Date     Who  Comments
// ============================================================================
// 1.2.0.0  08/04/2006  EFW  Created the code
//=============================================================================

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

using SandcastleBuilder.Utils;

namespace SandcastleBuilder.Utils.Gac
{
    /// <summary>
    /// This class is used to load assemblies in a separate application
    /// domain in order to obtain a list of namespaces within them.  It also
    /// contains a utility method used to get a list of assemblies in the GAC.
    /// </summary>
    /// <remarks>By loading them in a separate application domain, we can
    /// unload the application domain when done and not hold a reference
    /// to the assemblies.</remarks>
    public class AssemblyLoader : System.MarshalByRefObject
    {
        //=====================================================================
        // Private data members

        AppDomain appDomain;
        Dictionary<string, Assembly> loadedAssemblies;
        Collection<string> dependencies;

        // The one and only assembly loader
        private static AssemblyLoader loader;

        // This will contain the GAC assembly information.  It can be a little
        // time-consuming to enumerate the GAC so we'll keep the list around
        // after the first enumeration.
        private static Collection<string> gacList;

        //=====================================================================
        // Static properties and methods

        /// <summary>
        /// This static property is used to obtain a list containing the
        /// fully qualified names of all assemblies in the GAC.
        /// </summary>
        public static Collection<string> GacList
        {
            get
            {
                IAssemblyEnum ae;
                IAssemblyName an;
                StringBuilder sb;
                uint chars;

                if(gacList == null)
                {
                    gacList = new Collection<string>();

                    try
                    {
                        NativeMethods.CreateAssemblyEnum(out ae, IntPtr.Zero,
                            null, ASM_CACHE_FLAGS.ASM_CACHE_GAC, IntPtr.Zero);

                        while(ae.GetNextAssembly(IntPtr.Zero, out an, 0) == 0)
                        {
                            chars = 0;
                            an.GetDisplayName(null, ref chars,
                                ASM_DISPLAY_FLAGS.ALL);
                            sb = new StringBuilder((int)chars);
                            an.GetDisplayName(sb, ref chars,
                                ASM_DISPLAY_FLAGS.ALL);

                            gacList.Add(sb.ToString());
                        }
                    }
                    catch(Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                        throw new BuilderException(
                            "Unable to enumerate the GAC", ex);
                    }
                }

                return gacList;
            }
        }

        /// <summary>
        /// This is used to create the assembly loader instance.
        /// </summary>
        /// <param name="project">The project from which to get the
        /// necessary dependency information.</param>
        /// <returns>Returns the assembly loader instance</returns>
        public static AssemblyLoader CreateAssemblyLoader(
          SandcastleProject project)
        {
            string[] files;
            string dirName, fileSpec, sourcePath;
            int idx;

            if(loader == null)
            {
                AppDomain appDomain = AppDomain.CreateDomain(
                    "SHFBNamespaceInfo");

                loader = (AssemblyLoader)appDomain.CreateInstanceAndUnwrap(
                    typeof(AssemblyLoader).Assembly.FullName,
                    typeof(AssemblyLoader).FullName, false,
                    BindingFlags.NonPublic | BindingFlags.Instance, null,
                    new object[0], CultureInfo.InvariantCulture,
                    new object[0], AppDomain.CurrentDomain.Evidence);

                loader.AppDomain = appDomain;

                foreach(DocumentAssembly da in project.Assemblies)
                    if(!da.CommentsOnly)
                        loader.AddDependency(da.AssemblyPath);

                foreach(DependencyItem di in project.Dependencies)
                {
                    sourcePath = di.DependencyPath;

                    if(!sourcePath.StartsWith("GAC:"))
                        if(sourcePath.IndexOfAny(new char[] { '*', '?' }) == -1)
                            loader.AddDependency(sourcePath);
                        else
                        {
                            idx = sourcePath.LastIndexOf('\\');
                            dirName = sourcePath.Substring(0, idx);
                            fileSpec = sourcePath.Substring(idx + 1);

                            files = Directory.GetFiles(dirName, fileSpec);

                            foreach(string name in files)
                                if(name.EndsWith(".dll") || name.EndsWith(".exe"))
                                    loader.AddDependency(name);
                        }
                }
            }

            return loader;
        }

        /// <summary>
        /// This is used to release the assembly loader instance.
        /// </summary>
        public static void ReleaseAssemblyLoader()
        {
            if(loader != null && loader.AppDomain != null)
                AppDomain.Unload(loader.AppDomain);

            loader = null;
        }

        //=====================================================================
        // Properties

        /// <summary>
        /// Set or get the application domain for this instance
        /// </summary>
        private AppDomain AppDomain
        {
            get { return appDomain; }
            set { appDomain = value; }
        }

        //=====================================================================
        // Methods, etc

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <remarks>Use <see cref="CreateAssemblyLoader" /> to create
        /// an instance.  Use <see cref="ReleaseAssemblyLoader"/> when
        /// done with it.</remarks>
        private AssemblyLoader()
        {
            dependencies = new Collection<string>();
            loadedAssemblies = new Dictionary<string, Assembly>();

            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(
                CurrentDomain_AssemblyResolve);
        }

        /// <summary>
        /// This is used to get the location of the specified GAC assembly
        /// </summary>
        /// <param name="fullName">The assembly name in long form</param>
        /// <returns>Returns the full file path for the GAC assembly</returns>
        public string GetAssemblyLocation(string fullName)
        {
            foreach(Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                if(a.FullName == fullName)
                    return a.Location;

            // If not found, load it now
            Assembly asm = Assembly.Load(fullName);
            return asm.Location;
        }

        /// <summary>
        /// Add a dependency to the dependencies collection
        /// </summary>
        /// <param name="dependencyPath">The path to the dependency</param>
        /// <remarks>We must add the dependency within the instance as you
        /// cannot add them in the calling code as they won't be seen
        /// here in the application domain in which the instance is hosted.
        /// </remarks>
        public void AddDependency(string dependencyPath)
        {
            dependencies.Add(Path.GetFullPath(dependencyPath));
        }

        /// <summary>
        /// Load the named assembly file and get a list of all of its
        /// namespaces.
        /// </summary>
        /// <param name="assemblyFilename">The name of the assembly</param>
        /// <returns>Returns a string collection containing the namespace
        /// names.</returns>
        public Collection<string> GetNamespaces(string assemblyFilename)
        {
            Collection<string> namespaces = new Collection<string>();
            Assembly asm = null;
            string ns;

            // Do we need to load it now?
            if(!loadedAssemblies.TryGetValue(assemblyFilename, out asm))
            {
                AssemblyName asmName = AssemblyName.GetAssemblyName(
                    assemblyFilename);

                foreach(Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                    if(asmName.FullName == a.FullName)
                    {
                        asm = a;
                        break;
                    }

                if(asm == null)
                    asm = Assembly.LoadFile(assemblyFilename);

                if(asm == null)
                    throw new ArgumentException("Unable to load assembly: " +
                        assemblyFilename, "assemblyFilename");

                loadedAssemblies.Add(assemblyFilename, asm);
            }

            // Get the unique namespaces from the types in the assembly
            foreach(Type t in asm.GetTypes())
            {
                ns = (t.Namespace == null) ? String.Empty : t.Namespace;

                if(!namespaces.Contains(ns))
                    namespaces.Add(ns);
            }

            return namespaces;
        }

        /// <summary>
        /// This is handled to resolve dependent assemblies and load them
        /// when necessary.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="args">The event arguments</param>
        /// <returns>The loaded assembly</returns>
        private Assembly CurrentDomain_AssemblyResolve(object sender,
          ResolveEventArgs args)
        {
            Assembly asm = null;

            string[] nameInfo = args.Name.Split(new char[] { ',' });
            string resolveName = nameInfo[0];

            // See if it has already been loaded
            foreach(Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                if(args.Name == a.FullName)
                {
                    asm = a;
                    break;
                }

            // Check the dependencies list?
            if(asm == null)
                foreach(string filename in dependencies)
                    if(resolveName == Path.GetFileNameWithoutExtension(filename))
                    {
                        asm = Assembly.LoadFile(filename);
                        loadedAssemblies.Add(filename, asm);
                        break;
                    }

            if(asm == null)
                throw new BuilderException("Unable to resolve reference to " +
                    args.Name);

            return asm;
        }
    }
}
