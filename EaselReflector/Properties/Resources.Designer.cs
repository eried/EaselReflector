﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EaselReflector.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("EaselReflector.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .lnk.
        /// </summary>
        internal static string LinkExtension {
            get {
                return ResourceManager.GetString("LinkExtension", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;a class=\&quot;project-name\&quot; rel=\&quot;noreferrer\&quot; target=\&quot;_blank\&quot; href=\&quot;(?&lt;url&gt;/projects/.+?)\&quot;&gt;.*?&lt;span&gt;(?&lt;name&gt;.+?)&lt;/span&gt;.*?&lt;/a&gt;.
        /// </summary>
        internal static string RegexProjects {
            get {
                return ResourceManager.GetString("RegexProjects", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://easel.inventables.com.
        /// </summary>
        internal static string UrlHome {
            get {
                return ResourceManager.GetString("UrlHome", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://easel.inventables.com/projects?page={0}.
        /// </summary>
        internal static string UrlProjects {
            get {
                return ResourceManager.GetString("UrlProjects", resourceCulture);
            }
        }
    }
}
