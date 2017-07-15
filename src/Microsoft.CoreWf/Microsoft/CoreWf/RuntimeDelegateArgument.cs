// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CoreWf.Runtime;
using System;

namespace CoreWf
{
    // DelegateArgument is the XAML-based construct. RuntimeDelegateArgument is a binding construct to store the value
    [Fx.Tag.XamlVisible(false)]
    public sealed class RuntimeDelegateArgument
    {
        public RuntimeDelegateArgument(string name, Type type, ArgumentDirection direction, DelegateArgument boundArgument)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw CoreWf.Internals.FxTrace.Exception.ArgumentNullOrEmpty("name");
            }

            if (type == null)
            {
                throw CoreWf.Internals.FxTrace.Exception.ArgumentNull("type");
            }

            ArgumentDirectionHelper.Validate(direction, "direction");

            if (boundArgument != null)
            {
                // Validations that the bound argument matches are done
                // in CacheMetadata for ActivityDelegate.

                boundArgument.Bind(this);
            }

            this.Name = name;
            this.Type = type;
            this.Direction = direction;
            this.BoundArgument = boundArgument;
        }

        public string Name
        {
            get;
            private set;
        }

        //[SuppressMessage(FxCop.Category.Naming, FxCop.Rule.PropertyNamesShouldNotMatchGetMethods,
        //Justification = "Workflow normalizes on Type for Type properties")]
        public Type Type
        {
            get;
            private set;
        }

        public ArgumentDirection Direction
        {
            get;
            private set;
        }

        public DelegateArgument BoundArgument
        {
            get;
            private set;
        }
    }
}
