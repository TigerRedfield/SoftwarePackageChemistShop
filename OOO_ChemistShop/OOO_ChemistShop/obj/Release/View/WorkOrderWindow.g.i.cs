﻿#pragma checksum "..\..\..\View\WorkOrderWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8090A52A8D63A650FF7579D177D0C46A0BC630E8C9FC0D158988D92F0B5F9968"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using OOO_ChemistShop.View;
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


namespace OOO_ChemistShop.View {
    
    
    /// <summary>
    /// WorkOrderWindow
    /// </summary>
    public partial class WorkOrderWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 30 "..\..\..\View\WorkOrderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonExit;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\View\WorkOrderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbCount;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\View\WorkOrderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RbSortAll;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\View\WorkOrderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RbSortHigh;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\View\WorkOrderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RbSortLow;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\View\WorkOrderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbFilterStatus;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\View\WorkOrderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listViewOrders;
        
        #line default
        #line hidden
        
        
        #line 128 "..\..\..\View\WorkOrderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listViewOrder;
        
        #line default
        #line hidden
        
        
        #line 154 "..\..\..\View\WorkOrderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dateDelivery;
        
        #line default
        #line hidden
        
        
        #line 163 "..\..\..\View\WorkOrderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbStatus;
        
        #line default
        #line hidden
        
        
        #line 165 "..\..\..\View\WorkOrderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button butEditOrder;
        
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
            System.Uri resourceLocater = new System.Uri("/OOO_ChemistShop;component/view/workorderwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\WorkOrderWindow.xaml"
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
            
            #line 8 "..\..\..\View\WorkOrderWindow.xaml"
            ((OOO_ChemistShop.View.WorkOrderWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ButtonExit = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\View\WorkOrderWindow.xaml"
            this.ButtonExit.Click += new System.Windows.RoutedEventHandler(this.ButtonExit_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tbCount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.RbSortAll = ((System.Windows.Controls.RadioButton)(target));
            
            #line 76 "..\..\..\View\WorkOrderWindow.xaml"
            this.RbSortAll.Checked += new System.Windows.RoutedEventHandler(this.RbSortAll_Checked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.RbSortHigh = ((System.Windows.Controls.RadioButton)(target));
            
            #line 79 "..\..\..\View\WorkOrderWindow.xaml"
            this.RbSortHigh.Checked += new System.Windows.RoutedEventHandler(this.RbSortHigh_Checked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.RbSortLow = ((System.Windows.Controls.RadioButton)(target));
            
            #line 80 "..\..\..\View\WorkOrderWindow.xaml"
            this.RbSortLow.Checked += new System.Windows.RoutedEventHandler(this.RbSortLow_Checked);
            
            #line default
            #line hidden
            return;
            case 7:
            this.cbFilterStatus = ((System.Windows.Controls.ComboBox)(target));
            
            #line 92 "..\..\..\View\WorkOrderWindow.xaml"
            this.cbFilterStatus.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbFilterStatus_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.listViewOrders = ((System.Windows.Controls.ListView)(target));
            
            #line 100 "..\..\..\View\WorkOrderWindow.xaml"
            this.listViewOrders.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.listViewOrders_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.listViewOrder = ((System.Windows.Controls.ListView)(target));
            return;
            case 10:
            this.dateDelivery = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 11:
            this.cbStatus = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 12:
            this.butEditOrder = ((System.Windows.Controls.Button)(target));
            
            #line 165 "..\..\..\View\WorkOrderWindow.xaml"
            this.butEditOrder.Click += new System.Windows.RoutedEventHandler(this.butEditOrder_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

