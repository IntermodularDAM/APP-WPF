﻿#pragma checksum "..\..\..\..\View\Habitaciones\BuscadorHabitaciones.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F8B263B5C04A79436B349C34C98070D079368AD6D5AB0FCBCFDB382ECD5543B9"
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
using app.View.Habitaciones;


namespace app.View.Habitaciones {
    
    
    /// <summary>
    /// BuscadorHabitaciones
    /// </summary>
    public partial class BuscadorHabitaciones : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 40 "..\..\..\..\View\Habitaciones\BuscadorHabitaciones.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnVolver;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\View\Habitaciones\BuscadorHabitaciones.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_AddHabitacion;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\View\Habitaciones\BuscadorHabitaciones.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Volver;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\..\View\Habitaciones\BuscadorHabitaciones.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBoxHuespedes;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\..\View\Habitaciones\BuscadorHabitaciones.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker fecha_in;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\..\..\View\Habitaciones\BuscadorHabitaciones.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker fecha_out;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\..\View\Habitaciones\BuscadorHabitaciones.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox txtOp;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\..\..\View\Habitaciones\BuscadorHabitaciones.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox txtOp2;
        
        #line default
        #line hidden
        
        
        #line 131 "..\..\..\..\View\Habitaciones\BuscadorHabitaciones.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sliderPreu;
        
        #line default
        #line hidden
        
        
        #line 188 "..\..\..\..\View\Habitaciones\BuscadorHabitaciones.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel stackPanelResultados;
        
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
            System.Uri resourceLocater = new System.Uri("/app;component/view/habitaciones/buscadorhabitaciones.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\Habitaciones\BuscadorHabitaciones.xaml"
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
            this.btnVolver = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\..\View\Habitaciones\BuscadorHabitaciones.xaml"
            this.btnVolver.Click += new System.Windows.RoutedEventHandler(this.Click_btnVolver);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btn_AddHabitacion = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\..\..\View\Habitaciones\BuscadorHabitaciones.xaml"
            this.btn_AddHabitacion.Click += new System.Windows.RoutedEventHandler(this.btn_AddHabitacion_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btn_Volver = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\..\..\View\Habitaciones\BuscadorHabitaciones.xaml"
            this.btn_Volver.Click += new System.Windows.RoutedEventHandler(this.btn_Volver_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.comboBoxHuespedes = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.fecha_in = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 6:
            this.fecha_out = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 7:
            this.txtOp = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 8:
            this.txtOp2 = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 9:
            this.sliderPreu = ((System.Windows.Controls.Slider)(target));
            return;
            case 10:
            
            #line 154 "..\..\..\..\View\Habitaciones\BuscadorHabitaciones.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 164 "..\..\..\..\View\Habitaciones\BuscadorHabitaciones.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_Buscar_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.stackPanelResultados = ((System.Windows.Controls.WrapPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

