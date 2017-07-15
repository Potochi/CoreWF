// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using CoreWf.Tracking;

namespace Test.Common.TestObjects.Tracking
{
    public class TrackingParticipantWithException : InMemoryTrackingParticipant
    {
        protected override void Track(TrackingRecord record, TimeSpan timeout)
        {
            throw new Exception(TrackingConstants.ExceptionMessage);
        }
    }
}
