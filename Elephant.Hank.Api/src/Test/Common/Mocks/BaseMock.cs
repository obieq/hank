// ---------------------------------------------------------------------------------------------------
// <copyright file="BaseMock.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The BaseMock class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Test.Common.Mocks
{
    using Rhino.Mocks;

    /// <summary>
    /// The BaseMock class
    /// </summary>
    public class BaseMock
    {
        /// <summary>
        /// The mock repository.
        /// </summary>
        private static MockRepository mockRepository;

        /// <summary>
        /// Gets the mock repository.
        /// </summary>
        public static MockRepository MockRepository
        {
            get
            {
                return mockRepository ?? (mockRepository = new MockRepository());
            }
        }
    }
}