﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bruttissimo.Common.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class User {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal User() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Bruttissimo.Common.Resources.User", typeof(User).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Las credenciales ingresadas son invalidas..
        /// </summary>
        public static string AuthenticationError {
            get {
                return ResourceManager.GetString("AuthenticationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Se produjo un error al identificarte con las credenciales solicitadas. Vuelve a intentar más tarde..
        /// </summary>
        public static string AuthenticationFaulted {
            get {
                return ResourceManager.GetString("AuthenticationFaulted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Se produjo un error accediendo a la base de datos..
        /// </summary>
        public static string DatabaseError {
            get {
                return ResourceManager.GetString("DatabaseError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Esta dirección de email ya se encuentra registrada..
        /// </summary>
        public static string DuplicateEmail {
            get {
                return ResourceManager.GetString("DuplicateEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Validación de email.
        /// </summary>
        public static string EmailRegistrationSubject {
            get {
                return ResourceManager.GetString("EmailRegistrationSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Oops! Algo salió muy mal!.
        /// </summary>
        public static string FatalException {
            get {
                return ResourceManager.GetString("FatalException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {&quot;Exceptions&quot;:[&quot;Oops! Algo salió muy mal!&quot;]}.
        /// </summary>
        public static string FatalExceptionJson {
            get {
                return ResourceManager.GetString("FatalExceptionJson", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El Job solicitado no parece existir en el servidor..
        /// </summary>
        public static string InvalidJobKey {
            get {
                return ResourceManager.GetString("InvalidJobKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to dd&apos; de &apos;MMMM, yyyy.
        /// </summary>
        public static string LongDateFormat {
            get {
                return ResourceManager.GetString("LongDateFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to dd&apos; de &apos;MMMM, yyyy hh:mm:ss tt.
        /// </summary>
        public static string LongDateTimeFormat {
            get {
                return ResourceManager.GetString("LongDateTimeFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El articulo se ha publicado con éxito!.
        /// </summary>
        public static string PostCreateSuccess {
            get {
                return ResourceManager.GetString("PostCreateSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Refrescar.
        /// </summary>
        public static string Refresh {
            get {
                return ResourceManager.GetString("Refresh", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nuevo!.
        /// </summary>
        public static string TwitterLink {
            get {
                return ResourceManager.GetString("TwitterLink", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Algo salió mal! Vuelve a intentar más tarde..
        /// </summary>
        public static string UnhandledAjaxException {
            get {
                return ResourceManager.GetString("UnhandledAjaxException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Algo salió mal durante la comunicación con el servidor..
        /// </summary>
        public static string UnhandledException {
            get {
                return ResourceManager.GetString("UnhandledException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {&quot;Exceptions&quot;:[&quot;Algo salió mal! Vuelve a intentar más tarde.&quot;]}.
        /// </summary>
        public static string UnhandledExceptionJson {
            get {
                return ResourceManager.GetString("UnhandledExceptionJson", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to anónimo.
        /// </summary>
        public static string UnregisteredUserDisplayName {
            get {
                return ResourceManager.GetString("UnregisteredUserDisplayName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El recurso solicitado no existe en el servidor..
        /// </summary>
        public static string WebResourceNotFound {
            get {
                return ResourceManager.GetString("WebResourceNotFound", resourceCulture);
            }
        }
    }
}
