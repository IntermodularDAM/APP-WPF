﻿#pragma checksum "..\..\..\..\..\View\Usuarios\Pre_Registros\Pre_Registro.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "21918377AF0F93B3F398D947B4B22A9D6B2AEDACA197A3A954AF25D6B6764511"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
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
using app.View.Usuarios.Pre_Registros;
using app.ViewModel.Usuarios;


namespace app.View.Usuarios.Pre_Registros {
    
    
    /// <summary>
    /// Pre_Registro
    /// </summary>
    public partial class Pre_Registro : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 46 "..\..\..\..\..\View\Usuarios\Pre_Registros\Pre_Registro.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse imgPerfil;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\..\View\Usuarios\Pre_Registros\Pre_Registro.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtTitulo;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\..\View\Usuarios\Pre_Registros\Pre_Registro.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtEmail;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\..\..\View\Usuarios\Pre_Registros\Pre_Registro.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtEmailConfirmar;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\..\..\View\Usuarios\Pre_Registros\Pre_Registro.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox txtRol;
        
        #line default
        #line hidden
        
        
        #line 128 "..\..\..\..\..\View\Usuarios\Pre_Registros\Pre_Registro.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btPre_Registro;
        
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
            System.Uri resourceLocater = new System.Uri("/app;component/view/usuarios/pre_registros/pre_registro.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\Usuarios\Pre_Registros\Pre_Registro.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            
            #line 38 "..\..\..\..\..\View\Usuarios\Pre_Registros\Pre_Registro.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Image_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.imgPerfil = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 3:
            this.txtTitulo = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.txtEmail = ((System.Windows.Controls.TextBox)(target));
            
            #line 60 "..\..\..\..\..\View\Usuarios\Pre_Registros\Pre_Registro.xaml"
            this.txtEmail.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtEmail_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txtEmailConfirmar = ((System.Windows.Controls.TextBox)(target));
            
            #line 81 "..\..\..\..\..\View\Usuarios\Pre_Registros\Pre_Registro.xaml"
            this.txtEmailConfirmar.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtEmailConfirmar_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txtRol = ((System.Windows.Controls.ComboBox)(target));
            
            #line 103 "..\..\..\..\..\View\Usuarios\Pre_Registros\Pre_Registro.xaml"
            this.txtRol.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.txtRol_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btPre_Registro = ((System.Windows.Controls.Button)(target));
            
            #line 128 "..\..\..\..\..\View\Usuarios\Pre_Registros\Pre_Registro.xaml"
            this.btPre_Registro.Click += new System.Windows.RoutedEventHandler(this.btPre_Registro_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

