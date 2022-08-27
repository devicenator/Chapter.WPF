// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System.Globalization;
using System.Windows;
using System.Windows.Data;

// ReSharper disable once CheckNamespace

namespace Chapter.WPF;

/// <summary>
///     Brings the possibility to modify existing bindings. See <see cref="BindingAdapter" />.
/// </summary>
/// <example>
///     <code lang="XAML">
/// <![CDATA[
/// <TextBlock Text="{Binding Demo}" ToolTip="{Binding AnyTag}">
///     <Helpers:BindingAdapter.BindingExtensions>
///         <Helpers:BindingExtensionCollection>
///             <Helpers:BindingExtension Property="TextBlock.Text"
///                                       Converter="{Binding DemoConverter}"
///                                       ConverterParameter="{Binding DemoConverterParameter}" />
///             <Helpers:BindingExtension Property="TextBlock.ToolTip"
///                                       FallbackValue="{Binding BindingFallbackValue}" />
///         </Helpers:BindingExtensionCollection>
///     </Helpers:BindingAdapter.BindingExtensions>
/// </TextBlock>
/// ]]>
/// </code>
/// </example>
public class BindingExtension : FrameworkElement
{
    internal static readonly DependencyProperty OwnerProperty =
        DependencyProperty.Register("Owner", typeof(FrameworkElement), typeof(BindingExtension), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     Identifies the Property dependency property.
    /// </summary>
    public static readonly DependencyProperty PropertyProperty =
        DependencyProperty.Register("Property", typeof(DependencyProperty), typeof(BindingExtension), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     Identifies the UpdateSourceTrigger dependency property.
    /// </summary>
    public static readonly DependencyProperty UpdateSourceTriggerProperty =
        DependencyProperty.Register("UpdateSourceTrigger", typeof(UpdateSourceTrigger), typeof(BindingExtension), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     Identifies the ValidatesOnDataErrors dependency property.
    /// </summary>
    public static readonly DependencyProperty ValidatesOnDataErrorsProperty =
        DependencyProperty.Register("ValidatesOnDataErrors", typeof(bool), typeof(BindingExtension), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     Identifies the Mode dependency property.
    /// </summary>
    public static readonly DependencyProperty ModeProperty =
        DependencyProperty.Register("Mode", typeof(BindingMode), typeof(BindingExtension), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     Identifies the Path dependency property.
    /// </summary>
    public static readonly DependencyProperty PathProperty =
        DependencyProperty.Register("Path", typeof(PropertyPath), typeof(BindingExtension), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     Identifies the AsyncState dependency property.
    /// </summary>
    public static readonly DependencyProperty AsyncStateProperty =
        DependencyProperty.Register("AsyncState", typeof(object), typeof(BindingExtension), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     Identifies the BindingGroupName dependency property.
    /// </summary>
    public static readonly DependencyProperty BindingGroupNameProperty =
        DependencyProperty.Register("BindingGroupName", typeof(string), typeof(BindingExtension), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     Identifies the BindsDirectlyToSource dependency property.
    /// </summary>
    public static readonly DependencyProperty BindsDirectlyToSourceProperty =
        DependencyProperty.Register("BindsDirectlyToSource", typeof(bool), typeof(BindingExtension), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     Identifies the Converter dependency property.
    /// </summary>
    public static readonly DependencyProperty ConverterProperty =
        DependencyProperty.Register("Converter", typeof(IValueConverter), typeof(BindingExtension), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     Identifies the ConverterCulture dependency property.
    /// </summary>
    public static readonly DependencyProperty ConverterCultureProperty =
        DependencyProperty.Register("ConverterCulture", typeof(CultureInfo), typeof(BindingExtension), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     Identifies the ConverterParameter dependency property.
    /// </summary>
    public static readonly DependencyProperty ConverterParameterProperty =
        DependencyProperty.Register("ConverterParameter", typeof(object), typeof(BindingExtension), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     Identifies the ElementName dependency property.
    /// </summary>
    public static readonly DependencyProperty ElementNameProperty =
        DependencyProperty.Register("ElementName", typeof(string), typeof(BindingExtension), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     Identifies the FallbackValue dependency property.
    /// </summary>
    public static readonly DependencyProperty FallbackValueProperty =
        DependencyProperty.Register("FallbackValue", typeof(object), typeof(BindingExtension), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     Identifies the IsAsync dependency property.
    /// </summary>
    public static readonly DependencyProperty IsAsyncProperty =
        DependencyProperty.Register("IsAsync", typeof(bool), typeof(BindingExtension), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     Identifies the NotifyOnSourceUpdated dependency property.
    /// </summary>
    public static readonly DependencyProperty NotifyOnSourceUpdatedProperty =
        DependencyProperty.Register("NotifyOnSourceUpdated", typeof(bool), typeof(BindingExtension), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     Identifies the NotifyOnTargetUpdated dependency property.
    /// </summary>
    public static readonly DependencyProperty NotifyOnTargetUpdatedProperty =
        DependencyProperty.Register("NotifyOnTargetUpdated", typeof(bool), typeof(BindingExtension), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     Identifies the NotifyOnValidationError dependency property.
    /// </summary>
    public static readonly DependencyProperty NotifyOnValidationErrorProperty =
        DependencyProperty.Register("NotifyOnValidationError", typeof(bool), typeof(BindingExtension), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     Identifies the StringFormat dependency property.
    /// </summary>
    public static readonly DependencyProperty StringFormatProperty =
        DependencyProperty.Register("StringFormat", typeof(string), typeof(BindingExtension), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     Identifies the TargetNullValue dependency property.
    /// </summary>
    public static readonly DependencyProperty TargetNullValueProperty =
        DependencyProperty.Register("TargetNullValue", typeof(object), typeof(BindingExtension), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     Identifies the UpdateSourceExceptionFilter dependency property.
    /// </summary>
    public static readonly DependencyProperty UpdateSourceExceptionFilterProperty =
        DependencyProperty.Register("UpdateSourceExceptionFilter", typeof(UpdateSourceExceptionFilterCallback), typeof(BindingExtension), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     Identifies the ValidatesOnExceptions dependency property.
    /// </summary>
    public static readonly DependencyProperty ValidatesOnExceptionsProperty =
        DependencyProperty.Register("ValidatesOnExceptions", typeof(bool), typeof(BindingExtension), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     Identifies the XPath dependency property.
    /// </summary>
    public static readonly DependencyProperty XPathProperty =
        DependencyProperty.Register("XPath", typeof(string), typeof(BindingExtension), new PropertyMetadata(OnDataChanged));

    private readonly WorkingIndicator _isChangedInternally;

    /// <summary>
    ///     Initializes a new instance of the <see cref="BindingExtension" /> class.
    /// </summary>
    public BindingExtension()
    {
        using (_isChangedInternally = new WorkingIndicator())
        {
            new Binding().CopyInto(this);
        }
    }

    internal FrameworkElement Owner
    {
        get => (FrameworkElement)GetValue(OwnerProperty);
        set => SetValue(OwnerProperty, value);
    }

    /// <summary>
    ///     Gets or sets the property which binding have to be modified.
    /// </summary>
    public DependencyProperty Property
    {
        get => (DependencyProperty)GetValue(PropertyProperty);
        set => SetValue(PropertyProperty, value);
    }

    /// <summary>
    ///     Gets or sets the UpdateSourceTrigger in the modified binding.
    /// </summary>
    public UpdateSourceTrigger UpdateSourceTrigger
    {
        get => (UpdateSourceTrigger)GetValue(UpdateSourceTriggerProperty);
        set => SetValue(UpdateSourceTriggerProperty, value);
    }

    /// <summary>
    ///     Gets or sets the ValidatesOnDataErrors in the modified binding.
    /// </summary>
    public bool ValidatesOnDataErrors
    {
        get => (bool)GetValue(ValidatesOnDataErrorsProperty);
        set => SetValue(ValidatesOnDataErrorsProperty, value);
    }

    /// <summary>
    ///     Gets or sets the Mode in the modified binding.
    /// </summary>
    public BindingMode Mode
    {
        get => (BindingMode)GetValue(ModeProperty);
        set => SetValue(ModeProperty, value);
    }

    /// <summary>
    ///     Gets or sets the Path in the modified binding.
    /// </summary>
    public PropertyPath Path
    {
        get => (PropertyPath)GetValue(PathProperty);
        set => SetValue(PathProperty, value);
    }

    /// <summary>
    ///     Gets or sets the AsyncState in the modified binding.
    /// </summary>
    public object AsyncState
    {
        get => GetValue(AsyncStateProperty);
        set => SetValue(AsyncStateProperty, value);
    }

    /// <summary>
    ///     Gets or sets the BindingGroupName in the modified binding.
    /// </summary>
    public string BindingGroupName
    {
        get => (string)GetValue(BindingGroupNameProperty);
        set => SetValue(BindingGroupNameProperty, value);
    }

    /// <summary>
    ///     Gets or sets the BindsDirectlyToSource in the modified binding.
    /// </summary>
    public bool BindsDirectlyToSource
    {
        get => (bool)GetValue(BindsDirectlyToSourceProperty);
        set => SetValue(BindsDirectlyToSourceProperty, value);
    }

    /// <summary>
    ///     Gets or sets the Converter in the modified binding.
    /// </summary>
    public IValueConverter Converter
    {
        get => (IValueConverter)GetValue(ConverterProperty);
        set => SetValue(ConverterProperty, value);
    }

    /// <summary>
    ///     Gets or sets the ConverterCulture in the modified binding.
    /// </summary>
    public CultureInfo ConverterCulture
    {
        get => (CultureInfo)GetValue(ConverterCultureProperty);
        set => SetValue(ConverterCultureProperty, value);
    }

    /// <summary>
    ///     Gets or sets the ConverterParameter in the modified binding.
    /// </summary>
    public object ConverterParameter
    {
        get => GetValue(ConverterParameterProperty);
        set => SetValue(ConverterParameterProperty, value);
    }

    /// <summary>
    ///     Gets or sets the ElementName in the modified binding.
    /// </summary>
    public string ElementName
    {
        get => (string)GetValue(ElementNameProperty);
        set => SetValue(ElementNameProperty, value);
    }

    /// <summary>
    ///     Gets or sets the FallbackValue in the modified binding.
    /// </summary>
    public object FallbackValue
    {
        get => GetValue(FallbackValueProperty);
        set => SetValue(FallbackValueProperty, value);
    }

    /// <summary>
    ///     Gets or sets the IsAsync in the modified binding.
    /// </summary>
    public bool IsAsync
    {
        get => (bool)GetValue(IsAsyncProperty);
        set => SetValue(IsAsyncProperty, value);
    }

    /// <summary>
    ///     Gets or sets the NotifyOnSourceUpdated in the modified binding.
    /// </summary>
    public bool NotifyOnSourceUpdated
    {
        get => (bool)GetValue(NotifyOnSourceUpdatedProperty);
        set => SetValue(NotifyOnSourceUpdatedProperty, value);
    }

    /// <summary>
    ///     Gets or sets the NotifyOnTargetUpdated in the modified binding.
    /// </summary>
    public bool NotifyOnTargetUpdated
    {
        get => (bool)GetValue(NotifyOnTargetUpdatedProperty);
        set => SetValue(NotifyOnTargetUpdatedProperty, value);
    }

    /// <summary>
    ///     Gets or sets the NotifyOnValidationError in the modified binding.
    /// </summary>
    public bool NotifyOnValidationError
    {
        get => (bool)GetValue(NotifyOnValidationErrorProperty);
        set => SetValue(NotifyOnValidationErrorProperty, value);
    }

    /// <summary>
    ///     Gets or sets the StringFormat in the modified binding.
    /// </summary>
    public string StringFormat
    {
        get => (string)GetValue(StringFormatProperty);
        set => SetValue(StringFormatProperty, value);
    }

    /// <summary>
    ///     Gets or sets the TargetNullValue in the modified binding.
    /// </summary>
    public object TargetNullValue
    {
        get => GetValue(TargetNullValueProperty);
        set => SetValue(TargetNullValueProperty, value);
    }

    /// <summary>
    ///     Gets or sets the UpdateSourceExceptionFilter in the modified binding.
    /// </summary>
    public UpdateSourceExceptionFilterCallback UpdateSourceExceptionFilter
    {
        get => (UpdateSourceExceptionFilterCallback)GetValue(UpdateSourceExceptionFilterProperty);
        set => SetValue(UpdateSourceExceptionFilterProperty, value);
    }

    /// <summary>
    ///     Gets or sets the ValidatesOnExceptions in the modified binding.
    /// </summary>
    public bool ValidatesOnExceptions
    {
        get => (bool)GetValue(ValidatesOnExceptionsProperty);
        set => SetValue(ValidatesOnExceptionsProperty, value);
    }

    /// <summary>
    ///     Gets or sets the XPath in the modified binding.
    /// </summary>
    public string XPath
    {
        get => (string)GetValue(XPathProperty);
        set => SetValue(XPathProperty, value);
    }

    private static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var extension = (BindingExtension)d;
        extension.Update();
    }

    private void Update()
    {
        if (WorkingIndicator.IsActive(_isChangedInternally))
            return;

        if (Owner == null || Property == null)
            return;

        var expression = Owner.GetBindingExpression(Property);
        if (expression?.ParentBinding == null)
            return;

        var refreshedBinding = expression.ParentBinding.Clone();
        refreshedBinding.Take(this);

        Owner.SetBinding(Property, refreshedBinding);
    }
}