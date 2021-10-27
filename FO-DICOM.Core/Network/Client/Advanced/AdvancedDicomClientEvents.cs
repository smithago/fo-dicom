﻿// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace FellowOakDicom.Network.Client.Advanced
{
    /// <summary>
    /// This is an empty marker interface so it is possible to create collections of the various events that occur in our DICOM network communication
    /// </summary>
    public interface IAdvancedDicomClientEvent
    {
        
    }
    
    /// <summary>
    /// When the TCP connection with the SCP is closed
    /// </summary>
    public class ConnectionClosedEvent : IAdvancedDicomClientEvent
    {
        /// <summary>
        /// When the connection closed without an error
        /// </summary>
        public static readonly ConnectionClosedEvent WithoutException = new ConnectionClosedEvent();
        
        /// <summary>
        /// When the connection closed with an error
        /// </summary>
        /// <param name="exception">The error that occured while trying to read from or write to the connection</param>
        /// <returns></returns>
        public static ConnectionClosedEvent WithException(Exception exception) => new ConnectionClosedEvent(exception);
        
        /// <summary>
        /// (Optional) the exception that occured while trying to read from or write to the connection
        /// </summary>
        public Exception Exception { get; }

        private ConnectionClosedEvent()
        {
            
        }

        private ConnectionClosedEvent(Exception exception)
        {
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }
    }
    
    /// <summary>
    /// When the DICOM association is suddenly aborted
    /// </summary>
    public class DicomAbortedEvent : IAdvancedDicomClientEvent
    {
        /// <summary>
        /// Who initiated the ABORT
        /// </summary>
        public DicomAbortSource Source { get; }
        
        /// <summary>
        /// Why the ABORT occurred
        /// </summary>
        public DicomAbortReason Reason { get; }

        /// <summary>
        /// Initializes a new DicomAbortedEvent 
        /// </summary>
        public DicomAbortedEvent(DicomAbortSource source, DicomAbortReason reason)
        {
            Source = source;
            Reason = reason;
        }
    }
    
    /// <summary>
    /// When the DICOM association is accepted
    /// </summary>
    public class DicomAssociationAcceptedEvent : IAdvancedDicomClientEvent
    {
        public DicomAssociation Association { get; }

        /// <summary>
        /// Initializes a new DicomAssociationAcceptedEvent
        /// </summary>
        public DicomAssociationAcceptedEvent(DicomAssociation association)
        {
            Association = association ?? throw new ArgumentNullException(nameof(association));
        }
    }
    
    /// <summary>
    /// When the DICOM association is rejected
    /// </summary>
    public class DicomAssociationRejectedEvent : IAdvancedDicomClientEvent
    {
        /// <summary>
        /// Whether the rejection is permanent or only temporary
        /// </summary>
        public DicomRejectResult Result { get; }
        
        /// <summary>
        /// Who rejected the association
        /// </summary>
        public DicomRejectSource Source { get; }
        
        /// <summary>
        /// Why the association was rejected
        /// </summary>
        public DicomRejectReason Reason { get; }

        /// <summary>
        /// Initializes a new DicomAssociationRejectedEvent
        /// </summary>
        public DicomAssociationRejectedEvent(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            Result = result;
            Source = source;
            Reason = reason;
        }
    }

    /// <summary>
    /// When the association is released
    /// </summary>
    public class DicomAssociationReleasedEvent : IAdvancedDicomClientEvent
    {
        /// <summary>
        /// An instance of DicomAssociationReleasedEvent, which is a singleton since this event does not have any parameters
        /// </summary>
        public static readonly DicomAssociationReleasedEvent Instance = new DicomAssociationReleasedEvent();

        private DicomAssociationReleasedEvent()
        {
            
        }
    }

    /// <summary>
    /// When a DICOM request is completed and no further responses are expected
    /// </summary>
    public class RequestCompletedEvent : IAdvancedDicomClientEvent
    {
        /// <summary>
        /// The original request
        /// </summary>
        public DicomRequest Request { get; }
        
        /// <summary>
        /// The final response
        /// </summary>
        public DicomResponse Response { get; }

        /// <summary>
        /// Initializes a new RequestCompletedEvent
        /// </summary>
        public RequestCompletedEvent(DicomRequest request, DicomResponse response)
        {
            Request = request ?? throw new ArgumentNullException(nameof(request));
            Response = response ?? throw new ArgumentNullException(nameof(response));
        }
    }
    
    /// <summary>
    /// When a DICOM request has been sent to the SCP and is now pending one or more responses 
    /// </summary>
    public class RequestPendingEvent : IAdvancedDicomClientEvent
    {
        /// <summary>
        /// The original request
        /// </summary>
        public DicomRequest Request { get; }
        
        /// <summary>
        /// The response that was received from the SCP
        /// </summary>
        public DicomResponse Response { get; }

        /// <summary>
        /// Initializes a new RequestPendingEvent 
        /// </summary>
        public RequestPendingEvent(DicomRequest request, DicomResponse response)
        {
            Request = request ?? throw new ArgumentNullException(nameof(request));
            Response = response ?? throw new ArgumentNullException(nameof(response));
        }
    }
    
    /// <summary>
    /// When a DICOM request times out
    /// </summary>
    public class RequestTimedOutEvent : IAdvancedDicomClientEvent
    {
        /// <summary>
        /// The original request
        /// </summary>
        public DicomRequest Request { get; }
        
        /// <summary>
        /// The timeout that elapsed before receiving a response
        /// </summary>
        public TimeSpan Timeout { get; }

        /// <summary>
        /// Initializes a new RequestTimedOutEvent 
        /// </summary>
        public RequestTimedOutEvent(DicomRequest request, TimeSpan timeout)
        {
            Request = request ?? throw new ArgumentNullException(nameof(request));
            Timeout = timeout;
        }
    }

    /// <summary>
    /// When the internal DicomService queue is empty, and a call to SendNextMessage will be required to send more requests
    /// </summary>
    public class SendQueueEmptyEvent : IAdvancedDicomClientEvent
    {
        /// <summary>
        /// An instance of SendQueueEmptyEvent, which is a singleton since this event does not have any parameters
        /// </summary>
        public static readonly SendQueueEmptyEvent Instance = new SendQueueEmptyEvent();

        private SendQueueEmptyEvent() {}
    }
}