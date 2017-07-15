// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CoreWf.Validation;
using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace CoreWf
{
    public struct NativeActivityMetadata
    {
        private Activity _activity;
        private LocationReferenceEnvironment _environment;
        private bool _createEmptyBindings;

        internal NativeActivityMetadata(Activity activity, LocationReferenceEnvironment environment, bool createEmptyBindings)
        {
            _activity = activity;
            _environment = environment;
            _createEmptyBindings = createEmptyBindings;
        }

        internal bool CreateEmptyBindings
        {
            get
            {
                return _createEmptyBindings;
            }
        }

        public LocationReferenceEnvironment Environment
        {
            get
            {
                return _environment;
            }
        }

        public bool HasViolations
        {
            get
            {
                if (_activity == null)
                {
                    return false;
                }
                else
                {
                    return _activity.HasTempViolations;
                }
            }
        }

        public static bool operator ==(NativeActivityMetadata left, NativeActivityMetadata right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(NativeActivityMetadata left, NativeActivityMetadata right)
        {
            return !left.Equals(right);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is NativeActivityMetadata))
            {
                return false;
            }

            NativeActivityMetadata other = (NativeActivityMetadata)obj;
            return other._activity == _activity && other.Environment == this.Environment
                && other.CreateEmptyBindings == this.CreateEmptyBindings;
        }

        public override int GetHashCode()
        {
            if (_activity == null)
            {
                return 0;
            }
            else
            {
                return _activity.GetHashCode();
            }
        }

        public void Bind(Argument binding, RuntimeArgument argument)
        {
            ThrowIfDisposed();

            Argument.TryBind(binding, argument, _activity);
        }

        public void SetValidationErrorsCollection(Collection<ValidationError> validationErrors)
        {
            ThrowIfDisposed();

            ActivityUtilities.RemoveNulls(validationErrors);

            _activity.SetTempValidationErrorCollection(validationErrors);
        }

        public void AddValidationError(string validationErrorMessage)
        {
            AddValidationError(new ValidationError(validationErrorMessage));
        }

        public void AddValidationError(ValidationError validationError)
        {
            ThrowIfDisposed();

            if (validationError != null)
            {
                _activity.AddTempValidationError(validationError);
            }
        }

        public void SetArgumentsCollection(Collection<RuntimeArgument> arguments)
        {
            ThrowIfDisposed();

            ActivityUtilities.RemoveNulls(arguments);

            _activity.SetArgumentsCollection(arguments, _createEmptyBindings);
        }

        public void AddArgument(RuntimeArgument argument)
        {
            ThrowIfDisposed();

            if (argument != null)
            {
                _activity.AddArgument(argument, _createEmptyBindings);
            }
        }

        public void SetChildrenCollection(Collection<Activity> children)
        {
            ThrowIfDisposed();

            ActivityUtilities.RemoveNulls(children);

            _activity.SetChildrenCollection(children);
        }

        public void AddChild(Activity child)
        {
            AddChild(child, null);
        }

        public void AddChild(Activity child, object origin)
        {
            ThrowIfDisposed();
            ActivityUtilities.ValidateOrigin(origin, _activity);

            if (child != null)
            {
                _activity.AddChild(child);
                if (child.CacheId != _activity.CacheId)
                {
                    child.Origin = origin;
                }
            }
        }

        public void SetImplementationChildrenCollection(Collection<Activity> implementationChildren)
        {
            ThrowIfDisposed();

            ActivityUtilities.RemoveNulls(implementationChildren);

            _activity.SetImplementationChildrenCollection(implementationChildren);
        }

        public void AddImplementationChild(Activity implementationChild)
        {
            ThrowIfDisposed();

            if (implementationChild != null)
            {
                _activity.AddImplementationChild(implementationChild);
            }
        }

        //public void SetImportedChildrenCollection(Collection<Activity> importedChildren)
        //{
        //    ThrowIfDisposed();

        //    ActivityUtilities.RemoveNulls(importedChildren);

        //    this.activity.SetImportedChildrenCollection(importedChildren);
        //}

        public void AddImportedChild(Activity importedChild)
        {
            AddImportedChild(importedChild, null);
        }

        public void AddImportedChild(Activity importedChild, object origin)
        {
            ThrowIfDisposed();
            ActivityUtilities.ValidateOrigin(origin, _activity);

            if (importedChild != null)
            {
                _activity.AddImportedChild(importedChild);
                if (importedChild.CacheId != _activity.CacheId)
                {
                    importedChild.Origin = origin;
                }
            }
        }

        public void SetDelegatesCollection(Collection<ActivityDelegate> delegates)
        {
            ThrowIfDisposed();

            ActivityUtilities.RemoveNulls(delegates);

            _activity.SetDelegatesCollection(delegates);
        }

        public void AddDelegate(ActivityDelegate activityDelegate)
        {
            AddDelegate(activityDelegate, null);
        }

        public void AddDelegate(ActivityDelegate activityDelegate, object origin)
        {
            ThrowIfDisposed();
            ActivityUtilities.ValidateOrigin(origin, _activity);

            if (activityDelegate != null)
            {
                _activity.AddDelegate(activityDelegate);
                if (activityDelegate.Handler != null && activityDelegate.Handler.CacheId != _activity.CacheId)
                {
                    activityDelegate.Handler.Origin = origin;
                }
                // We don't currently have ActivityDelegate.Origin. If we ever add it, or if we ever
                // expose Origin publicly, we need to also set it here.
            }
        }

        public void SetImplementationDelegatesCollection(Collection<ActivityDelegate> implementationDelegates)
        {
            ThrowIfDisposed();

            ActivityUtilities.RemoveNulls(implementationDelegates);

            _activity.SetImplementationDelegatesCollection(implementationDelegates);
        }

        public void AddImplementationDelegate(ActivityDelegate implementationDelegate)
        {
            ThrowIfDisposed();

            if (implementationDelegate != null)
            {
                _activity.AddImplementationDelegate(implementationDelegate);
            }
        }

        public void SetImportedDelegatesCollection(Collection<ActivityDelegate> importedDelegates)
        {
            ThrowIfDisposed();

            ActivityUtilities.RemoveNulls(importedDelegates);

            _activity.SetImportedDelegatesCollection(importedDelegates);
        }

        public void AddImportedDelegate(ActivityDelegate importedDelegate)
        {
            AddImportedDelegate(importedDelegate, null);
        }

        public void AddImportedDelegate(ActivityDelegate importedDelegate, object origin)
        {
            ThrowIfDisposed();
            ActivityUtilities.ValidateOrigin(origin, _activity);

            if (importedDelegate != null)
            {
                _activity.AddImportedDelegate(importedDelegate);
                if (importedDelegate.Handler != null && importedDelegate.Handler.CacheId != _activity.CacheId)
                {
                    importedDelegate.Handler.Origin = origin;
                }
                // We don't currently have ActivityDelegate.Origin. If we ever add it, or if we ever
                // expose Origin publicly, we need to also set it here.
            }
        }

        public void SetVariablesCollection(Collection<Variable> variables)
        {
            ThrowIfDisposed();

            ActivityUtilities.RemoveNulls(variables);

            _activity.SetVariablesCollection(variables);
        }

        public void AddVariable(Variable variable)
        {
            AddVariable(variable, null);
        }

        public void AddVariable(Variable variable, object origin)
        {
            ThrowIfDisposed();
            ActivityUtilities.ValidateOrigin(origin, _activity);

            if (variable != null)
            {
                _activity.AddVariable(variable);
                if (variable.CacheId != _activity.CacheId)
                {
                    variable.Origin = origin;
                    if (variable.Default != null && variable.Default.CacheId != _activity.CacheId)
                    {
                        variable.Default.Origin = origin;
                    }
                }
            }
        }

        public void SetImplementationVariablesCollection(Collection<Variable> implementationVariables)
        {
            ThrowIfDisposed();

            ActivityUtilities.RemoveNulls(implementationVariables);

            _activity.SetImplementationVariablesCollection(implementationVariables);
        }

        public void AddImplementationVariable(Variable implementationVariable)
        {
            ThrowIfDisposed();

            if (implementationVariable != null)
            {
                _activity.AddImplementationVariable(implementationVariable);
            }
        }

        //public Collection<RuntimeArgument> GetArgumentsWithReflection()
        //{
        //    return Activity.ReflectedInformation.GetArguments(this.activity);
        //}

        //public Collection<Activity> GetChildrenWithReflection()
        //{
        //    return Activity.ReflectedInformation.GetChildren(this.activity);
        //}

        //public Collection<Variable> GetVariablesWithReflection()
        //{
        //    return Activity.ReflectedInformation.GetVariables(this.activity);
        //}

        //public Collection<ActivityDelegate> GetDelegatesWithReflection()
        //{
        //    return Activity.ReflectedInformation.GetDelegates(this.activity);
        //}

        public void AddDefaultExtensionProvider<T>(Func<T> extensionProvider)
            where T : class
        {
            if (extensionProvider == null)
            {
                throw CoreWf.Internals.FxTrace.Exception.ArgumentNull("extensionProvider");
            }
            _activity.AddDefaultExtensionProvider(extensionProvider);
        }

        public void RequireExtension<T>()
            where T : class
        {
            _activity.RequireExtension(typeof(T));
        }

        public void RequireExtension(Type extensionType)
        {
            if (extensionType == null)
            {
                throw CoreWf.Internals.FxTrace.Exception.ArgumentNull("extensionType");
            }
            if (extensionType.GetTypeInfo().IsValueType)
            {
                throw CoreWf.Internals.FxTrace.Exception.Argument("extensionType", SR.RequireExtensionOnlyAcceptsReferenceTypes(extensionType.FullName));
            }
            _activity.RequireExtension(extensionType);
        }

        internal void Dispose()
        {
            _activity = null;
        }

        private void ThrowIfDisposed()
        {
            if (_activity == null)
            {
                throw CoreWf.Internals.FxTrace.Exception.AsError(new ObjectDisposedException(ToString()));
            }
        }
    }
}
