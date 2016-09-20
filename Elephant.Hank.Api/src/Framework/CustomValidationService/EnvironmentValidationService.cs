// ---------------------------------------------------------------------------------------------------
// <copyright file="EnvironmentValidationService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-09-06</date>
// <summary>
//     The EnvironmentValidationService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.CustomValidationService
{
    using System;

    using Elephant.Hank.Common.CustomValidationService;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Framework.TestDataServices;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The EnvironmentValidationService class.
    /// </summary>
    public class EnvironmentValidationService : IValidationService<TblEnvironmentDto>
    {
        /// <summary>
        /// The data base connecton service
        /// </summary>
        private readonly IDataBaseConnectionService dataBaseConnectonService;

        /// <summary>
        /// The API connecton service
        /// </summary>
        private readonly IApiConnectionService apiConnectonService;

        /// <summary>
        /// The API connecton service
        /// </summary>
        private readonly ISchedulerService schedulerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentValidationService" /> class.
        /// </summary>
        /// <param name="dataBaseConnectonService">The data base connecton service.</param>
        /// <param name="apiConnectonService">The API connecton service.</param>
        /// <param name="schedulerService">The scheduler service.</param>
        public EnvironmentValidationService(IDataBaseConnectionService dataBaseConnectonService, IApiConnectionService apiConnectonService, ISchedulerService schedulerService)
        {
            this.apiConnectonService = apiConnectonService;
            this.dataBaseConnectonService = dataBaseConnectonService;
            this.schedulerService = schedulerService;
        }

        /// <summary>
        /// Validates the delete.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <returns>Returns the validation result</returns>
        public ResultMessage<bool> ValidateDelete(TblEnvironmentDto environment)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            ResultMessage<bool> isValid = this.CheckValidationAgainstApiConnection(environment.Id);
            if (isValid.Item)
            {
                isValid = this.CheckValidationAgainstDBConnection(environment.Id);
                if (isValid.Item)
                {
                    isValid = this.CheckValidationAgainstScheduler(environment.Id);
                    if (isValid.Item)
                    {
                        result.Item = true;
                    }
                    else
                    {
                        result.Item = false;
                        result.Messages.AddRange(isValid.Messages);
                    }
                }
                else
                {
                    result.Item = false;
                    result.Messages.AddRange(isValid.Messages);
                }
            }
            else
            {
                result.Item = false;
                result.Messages.AddRange(isValid.Messages);
            }

            return result;
        }

        /// <summary>
        /// Checks the validation against API connection.
        /// </summary>
        /// <param name="environmentId">The environment identifier.</param>
        /// <returns>returns the validation result against the Api Connection check</returns>
        private ResultMessage<bool> CheckValidationAgainstApiConnection(long environmentId)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            var apiConnection = this.apiConnectonService.GetByEnvironmentId(environmentId);
            if (apiConnection.IsError)
            {
                result.Item = true;
            }
            else
            {
                result.Item = false;
                result.Messages.Add(new Message("Not able to delete as its reference exist inside api connection"));
            }

            return result;
        }

        /// <summary>
        /// Checks the validation against database connection.
        /// </summary>
        /// <param name="environmentId">The environment identifier.</param>
        /// <returns>returns the validation result against the DB Connection check</returns>
        private ResultMessage<bool> CheckValidationAgainstDBConnection(long environmentId)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            var dataBaseConnection = this.dataBaseConnectonService.GetByEnvironmentId(environmentId);
            if (dataBaseConnection.IsError)
            {
                result.Item = true;
            }
            else
            {
                result.Item = false;
                result.Messages.Add(new Message("Not able to delete as its reference exist inside database connection"));
            }

            return result;
        }

        /// <summary>
        /// Checks the validation against scheduler.
        /// </summary>
        /// <param name="environmentId">The environment identifier.</param>
        /// <returns>returns the validation result against the scheduler check</returns>
        private ResultMessage<bool> CheckValidationAgainstScheduler(long environmentId)
        {
            ResultMessage<bool> result = new ResultMessage<bool>();
            var dataBaseConnection = this.schedulerService.GetByUrlId(environmentId);
            if (dataBaseConnection.IsError)
            {
                result.Item = true;
            }
            else
            {
                result.Item = false;
                result.Messages.Add(new Message("Not able to delete as its reference exist inside scheduler"));
            }

            return result;
        }
    }
}
