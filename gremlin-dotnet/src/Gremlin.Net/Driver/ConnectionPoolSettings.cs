#region License

/*
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

#endregion

using System;
using Gremlin.Net.Driver.Exceptions;

namespace Gremlin.Net.Driver
{
    /// <summary>
    ///     Holds settings for the <see cref="ConnectionPool"/>.
    /// </summary>
    public class ConnectionPoolSettings
    {
        private int _poolSize = DefaultPoolSize;
        private int _maxInProcessPerConnection = DefaultMaxInProcessPerConnection;
        private int _getOpenConnectionRetries = DefaultGetOpenConnectionRetries;
        private const int DefaultPoolSize = 4;
        private const int DefaultMaxInProcessPerConnection = 32;
        private const int DefaultGetOpenConnectionRetries = 4;

        /// <summary>
        ///     Gets or sets the size of the connection pool.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The specified pool size is less than or equal to zero.</exception>
        /// <remarks>
        ///     The default value is 4.
        /// </remarks>
        public int PoolSize
        {
            get => _poolSize;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(PoolSize), $"{nameof(PoolSize)} must be > 0!");
                _poolSize = value;
            }
        }

        /// <summary>
        ///     Gets or sets the maximum number of in-flight requests that can occur on a connection.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The specified number is less than or equal to zero.</exception>
        /// <remarks>
        ///     The default value is 32. A <see cref="ConnectionPoolBusyException" /> is thrown if this limit is reached
        ///     on all connections when a new request comes in.
        /// </remarks>
        public int MaxInProcessPerConnection
        {
            get => _maxInProcessPerConnection;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(MaxInProcessPerConnection),
                        $"{nameof(MaxInProcessPerConnection)} must be > 0!");
                _maxInProcessPerConnection = value;
            }
        }

        /// <summary>
        ///     Gets or sets the number of retries to get an open connection from the pool to submit a request.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The number of retries specified is less than zero.</exception>
        /// <remarks>
        ///     The driver always tries to reconnect to a server in the background after it has noticed that a
        ///     connection is dead. This setting only specifies how often the driver will retry to get an open
        ///     connection from its pool when no open connection is available to submit a request.  
        ///     These retries give the driver time to establish new connections to the server that might have been
        ///     unavailable temporarily or that might have closed the connections, e.g., because they were idle for some
        ///     time.
        /// 
        ///     The default value is 4. A <see cref="ServerUnavailableException" /> is thrown if the server can still
        ///     not be reached after this many retry attempts.
        ///
        ///     Setting this to zero means that the exception is thrown immediately when no open connection is available
        ///     to submit a request. The driver will however still try to reconnect to the server in the background for
        ///     subsequent requests.
        /// </remarks>
        public int GetOpenConnectionRetries
        {
            get => _getOpenConnectionRetries;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(GetOpenConnectionRetries),
                        $"{GetOpenConnectionRetries} must be >= 0!");
                _getOpenConnectionRetries = value;
            }
        }
    }
}