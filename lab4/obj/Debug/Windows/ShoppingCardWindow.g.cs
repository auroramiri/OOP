﻿#pragma checksum "..\..\..\Windows\ShoppingCardWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6A47EF06076651F3CAF80EF64A8A3C72B3BA48A35DD91136745CC89FD1AF8F5D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Converters;
using Xceed.Wpf.Toolkit.Core;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit.Core.Input;
using Xceed.Wpf.Toolkit.Core.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Mag.Converters;
using Xceed.Wpf.Toolkit.Panels;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Commands;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.Zoombox;
using lab4.Converters;
using lab4.Windows;


namespace lab4.Windows {
    
    
    /// <summary>
    /// ShoppingCardWindow
    /// </summary>
    public partial class ShoppingCardWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 13 "..\..\..\Windows\ShoppingCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal lab4.Windows.ShoppingCardWindow shoppingCardWindow;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\Windows\ShoppingCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button backButton;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\Windows\ShoppingCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl shoppingList;
        
        #line default
        #line hidden
        
        
        #line 208 "..\..\..\Windows\ShoppingCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock totalPriceBlock;
        
        #line default
        #line hidden
        
        
        #line 225 "..\..\..\Windows\ShoppingCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock totalCountBlock;
        
        #line default
        #line hidden
        
        
        #line 279 "..\..\..\Windows\ShoppingCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton MastercardRadioButton;
        
        #line default
        #line hidden
        
        
        #line 285 "..\..\..\Windows\ShoppingCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton VisaRadioButton;
        
        #line default
        #line hidden
        
        
        #line 291 "..\..\..\Windows\ShoppingCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton CreditRadioButton;
        
        #line default
        #line hidden
        
        
        #line 301 "..\..\..\Windows\ShoppingCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.MaskedTextBox CardNumberTextBox;
        
        #line default
        #line hidden
        
        
        #line 322 "..\..\..\Windows\ShoppingCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.MaskedTextBox ExpireDateTextBox;
        
        #line default
        #line hidden
        
        
        #line 337 "..\..\..\Windows\ShoppingCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.MaskedTextBox CVVTextBox;
        
        #line default
        #line hidden
        
        
        #line 348 "..\..\..\Windows\ShoppingCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CheckoutButton;
        
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
            System.Uri resourceLocater = new System.Uri("/lab4;component/windows/shoppingcardwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\ShoppingCardWindow.xaml"
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
            this.shoppingCardWindow = ((lab4.Windows.ShoppingCardWindow)(target));
            return;
            case 2:
            this.backButton = ((System.Windows.Controls.Button)(target));
            
            #line 76 "..\..\..\Windows\ShoppingCardWindow.xaml"
            this.backButton.Click += new System.Windows.RoutedEventHandler(this.closeButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.shoppingList = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 7:
            this.totalPriceBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.totalCountBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.MastercardRadioButton = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 10:
            this.VisaRadioButton = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 11:
            this.CreditRadioButton = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 12:
            this.CardNumberTextBox = ((Xceed.Wpf.Toolkit.MaskedTextBox)(target));
            return;
            case 13:
            this.ExpireDateTextBox = ((Xceed.Wpf.Toolkit.MaskedTextBox)(target));
            return;
            case 14:
            this.CVVTextBox = ((Xceed.Wpf.Toolkit.MaskedTextBox)(target));
            return;
            case 15:
            this.CheckoutButton = ((System.Windows.Controls.Button)(target));
            return;
            case 16:
            
            #line 353 "..\..\..\Windows\ShoppingCardWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.CheckoutButton_Executed);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 4:
            
            #line 164 "..\..\..\Windows\ShoppingCardWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.AddOneButton_Executed);
            
            #line default
            #line hidden
            break;
            case 5:
            
            #line 175 "..\..\..\Windows\ShoppingCardWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.RemoveOneFromToCard_Executed);
            
            #line default
            #line hidden
            break;
            case 6:
            
            #line 186 "..\..\..\Windows\ShoppingCardWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.DeleteButton_Executed);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

