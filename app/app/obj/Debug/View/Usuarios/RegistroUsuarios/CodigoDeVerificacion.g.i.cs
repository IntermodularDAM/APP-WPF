﻿#pragma checksum "..\..\..\..\..\View\Usuarios\RegistroUsuarios\CodigoDeVerificacion.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C12AA6A3FD831210EC429DB2DE7844BD24B43779FE70DF73C9BBABAE14CCE3C1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using app.View.Usuarios.RegistroUsuarios;


namespace app.View.Usuarios.RegistroUsuarios {
    
    
    /// <summary>
    /// CodigoDeVerificacion
    /// </summary>
    public partial class CodigoDeVerificacion : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 33 "..\..\..\..\..\View\Usuarios\RegistroUsuarios\CodigoDeVerificacion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCerrar;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\..\View\Usuarios\RegistroUsuarios\CodigoDeVerificacion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextEmail;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\..\View\Usuarios\RegistroUsuarios\CodigoDeVerificacion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCodigo;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\..\View\Usuarios\RegistroUsuarios\CodigoDeVerificacion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ErrorTextCodigo;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\..\View\Usuarios\RegistroUsuarios\CodigoDeVerificacion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEnviar;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/app;component/view/usuarios/registrousuarios/codigodeverificacion.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\Usuarios\RegistroUsuarios\CodigoDeVerificacion.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 13 "..\..\..\..\..\View\Usuarios\RegistroUsuarios\CodigoDeVerificacion.xaml"
            ((app.View.Usuarios.RegistroUsuarios.CodigoDeVerificacion)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnCerrar = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\..\..\View\Usuarios\RegistroUsuarios\CodigoDeVerificacion.xaml"
            this.btnCerrar.Click += new System.Windows.RoutedEventHandler(this.btnCerrar_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TextEmail = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.txtCodigo = ((System.Windows.Controls.TextBox)(target));
            
            #line 41 "..\..\..\..\..\View\Usuarios\RegistroUsuarios\CodigoDeVerificacion.xaml"
            this.txtCodigo.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtCodigo_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ErrorTextCodigo = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.btnEnviar = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\..\..\..\View\Usuarios\RegistroUsuarios\CodigoDeVerificacion.xaml"
            this.btnEnviar.Click += new System.Windows.RoutedEventHandler(this.btnEnviar_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

