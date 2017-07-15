// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CoreWf.Runtime;

namespace CoreWf
{
    // This wrapper is used to make our "new Expression" and "new Default" APIs
    // work correctly even if the expression set on the base class doesn't
    // match.  We'll log the error at cache metadata time.
    internal class ActivityWithResultWrapper<T> : CodeActivity<T>, Argument.IExpressionWrapper
    {
        private ActivityWithResult _expression;

        public ActivityWithResultWrapper(ActivityWithResult expression)
        {
            _expression = expression;
        }

        ActivityWithResult Argument.IExpressionWrapper.InnerExpression
        {
            get
            {
                return _expression;
            }
        }

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            // If we've gotten here then argument validation has already
            // logged a validation error.
        }

        protected override T Execute(CodeActivityContext context)
        {
            Fx.Assert("We'll never get here!");

            return default(T);
        }
    }
}
