﻿#pragma checksum "..\..\..\..\View\Reservas\InformacionReserva.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5F3B002E41709260C31301E685A227B512ADA7798B81F878A4F85D73323546FB"
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
using app.View.Reservas;


namespace app.View.Reservas {
    
    
    /// <summary>
    /// InformacionReserva
    /// </summary>
    public partial class InformacionReserva : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 44 "..\..\..\..\View\Reservas\InformacionReserva.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtNombre;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\View\Reservas\InformacionReserva.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtUsuario;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\View\Reservas\InformacionReserva.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtHabitacion;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\View\Reservas\InformacionReserva.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtFechaEntrada;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\View\Reservas\InformacionReserva.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtFechaSalida;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\..\View\Reservas\InformacionReserva.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtEstadoReserva;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\View\Reservas\InformacionReserva.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCerrar;
        
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
            System.Uri resourceLocater = new System.Uri("/app;component/view/reservas/informacionreserva.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\Reservas\InformacionReserva.xaml"
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
            this.txtNombre = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.txtUsuario = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.txtHabitacion = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.txtFechaEntrada = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.txtFechaSalida = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.txtEstadoReserva = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.btnCerrar = ((System.Windows.Controls.Button)(target));
            
            #line 61 "..\..\..\..\View\Reservas\InformacionReserva.xaml"
            this.btnCerrar.Click += new System.Windows.RoutedEventHandler(this.btnCerrar_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

