﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :2.0.50727.1433
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Arcane.Silverlight.Controls.Properties {
    using System;
    
    
    /// <summary>
    ///   Une classe de ressource fortement typée destinée, entre autres, à la consultation des chaînes localisées.
    /// </summary>
    // Cette classe a été générée automatiquement par la classe StronglyTypedResourceBuilder
    // à l'aide d'un outil, tel que ResGen ou Visual Studio.
    // Pour ajouter ou supprimer un membre, modifiez votre fichier .ResX, puis réexécutez ResGen
    // avec l'option /str ou régénérez votre projet VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Retourne l'instance ResourceManager mise en cache utilisée par cette classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Arcane.Silverlight.Controls.Properties.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Remplace la propriété CurrentUICulture du thread actuel pour toutes
        ///   les recherches de ressources à l'aide de cette classe de ressource fortement typée.
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
        ///   Recherche une chaîne localisée semblable à Only SelectionMode.Single is supported..
        /// </summary>
        internal static string TreeView_OnIsSelectionActiveChanged_ReadOnly {
            get {
                return ResourceManager.GetString("TreeView_OnIsSelectionActiveChanged_ReadOnly", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à FrameworkElement.Style property can be set only one time..
        /// </summary>
        internal static string Treeview_OnItemContainerStyleChanged_CanNotSetStyle {
            get {
                return ResourceManager.GetString("Treeview_OnItemContainerStyleChanged_CanNotSetStyle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à SelectedItems is a read-only DependencyProperty..
        /// </summary>
        internal static string Treeview_OnSelectedItemsChanged_ReadOnly {
            get {
                return ResourceManager.GetString("Treeview_OnSelectedItemsChanged_ReadOnly", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Can only change the SelectedItems collection in multiple selection mode. Use SelectedItem in single selection mode..
        /// </summary>
        internal static string Treeview_OnSelectedItemsCollectionChanged_WrongMode {
            get {
                return ResourceManager.GetString("Treeview_OnSelectedItemsCollectionChanged_WrongMode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Only SelectionMode.Single is supported..
        /// </summary>
        internal static string Treeview_OnSelectionModeChanged_OnlySingleSelection {
            get {
                return ResourceManager.GetString("Treeview_OnSelectionModeChanged_OnlySingleSelection", resourceCulture);
            }
        }
    }
}
